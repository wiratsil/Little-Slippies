using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Main_Multi : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1";

    public static Main_Multi instance;
    public byte maxPlayersPerRoom = 4;
    public int readyCount = 5;

    public enum MultiMode { None, JoinRoom, QuickMatch, CreateRoom }
    public MultiMode multiMode;
    [Space]
    public GameObject gameReady;
    public Transform slotTrans;
    public GameObject slotPlayer;
    public List<GameObject> slotPlayerList;
    public Button readyButton;
    public TextMeshProUGUI readyTxt;
    [Space]
    public GameObject gameJoinRoom;
    public string roomName;
    public TMP_InputField roomNameSet;
    [Space]
    public GameObject gameRoom;
    public Button roomStart;
    public TextMeshProUGUI roomNameTxt;

    private void Awake()
    {
        if (Main_Multi.instance != null && Main_Multi.instance != this)
        {
            Destroy(transform.gameObject);
            return;
        }
        instance = this;
        //DontDestroyOnLoad(transform.gameObject);


    }

    // Start is called before the first frame update
    void Start()
    {
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.AutomaticallySyncScene = true;

        if (!PhotonNetwork.IsConnected)
        {

            LogFeedback("Connecting...", "Blue");

            // #Critical, we must first and foremost connect to Photon Online Server.
            PhotonNetwork.GameVersion = this.gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LogFeedback (string txt, string color = "Black")
    {
        Debug.Log("<Color=" + color + ">" + txt + "</Color>");
    }

    public void Z_QuickMatch()
    {
        multiMode = MultiMode.QuickMatch;

        // we check if we are connected or not, we join if we are , else we initiate the connection to the server.
        if (PhotonNetwork.IsConnected)
        {
            LogFeedback("Joining Room...","Blue");
            // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {

            LogFeedback("Connecting...","Blue");

            // #Critical, we must first and foremost connect to Photon Online Server.
            PhotonNetwork.GameVersion = this.gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void Z_CreateRoom()
    {
        multiMode = MultiMode.CreateRoom;
        LogFeedback("Create Room...", "Blue");

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = false;
        roomOptions.MaxPlayers = this.maxPlayersPerRoom;

        roomName = Random.Range(0,9999).ToString("D4");
        LogFeedback("Room Name Is : " + roomName, "Blue");

        roomNameTxt.text = "Room : " + roomName;
        PhotonNetwork.CreateRoom(roomName.ToString(), roomOptions);
    }

    public void Z_OpenJoinRoom()
    {
        multiMode = MultiMode.JoinRoom;
        gameJoinRoom.SetActive(true);
    }

    public void Z_CloseJoinRoom()
    {
        gameJoinRoom.SetActive(false);
    }

    public void Z_JoinRoom ()
    {
        LogFeedback("Join Room...", "Blue");
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = false;
        roomOptions.MaxPlayers = this.maxPlayersPerRoom;
        PhotonNetwork.JoinOrCreateRoom(roomNameSet.text, roomOptions, TypedLobby.Default);

        gameJoinRoom.SetActive(false);
        gameRoom.SetActive(true);
    }
    

    public override void OnCreatedRoom()
    {
        LogFeedback("OnCreatedRoom...", "Blue");
        base.OnCreatedRoom();

        if (multiMode != MultiMode.QuickMatch)
        {
            gameRoom.SetActive(true);
            roomStart.gameObject.SetActive(true);
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        LogFeedback("OnJoinRandomFailed: Next -> Create a new Room","Blue");

        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = this.maxPlayersPerRoom });
    }


    /// <summary>
    /// Called after the connection to the master is established and authenticated
    /// </summary>
    public override void OnConnectedToMaster()
    {
        // we don't want to do anything if we are not attempting to join a room. 
        // this case where isConnecting is false is typically when you lost or quit the game, when this level is loaded, OnConnectedToMaster will be called, in that case
        // we don't want to do anything.
        if (PhotonNetwork.IsConnected)
        {
            LogFeedback("OnConnectedToMaster","Blue");
            //Debug.Log("OnConnectedToMaster/*: Next -> try to Join Random Room");
            //Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room.\n Calling: PhotonNetwork.JoinRandomRoom(); Operation will fail if no room found");

            // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()
            //PhotonNetwork.JoinRandomRoom();
        }
    }
    
    public override void OnJoinedRoom()
    {
        LogFeedback("OnJoinedRoom with " + PhotonNetwork.CurrentRoom.PlayerCount + " Player(s)","Blue");

        roomNameTxt.text = "Room : " + PhotonNetwork.CurrentRoom.Name ;

        CheckReady();
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        LogFeedback("OnPlayerEnteredRoom : " + PhotonNetwork.CurrentRoom.PlayerCount, "Blue");
        base.OnPlayerEnteredRoom(newPlayer);

        CheckReady();
    }

    public void Ready()
    {
        Hashtable setRoomProperties = new Hashtable();
        setRoomProperties.Add("RTP", true);
        PhotonNetwork.LocalPlayer.SetCustomProperties(setRoomProperties);
        readyButton.gameObject.SetActive(false);
        readyTxt.text = "Waiting for other players";
    }

    public void UnReady()
    {
        Hashtable setRoomProperties = new Hashtable();
        setRoomProperties.Add("RTP", false);
        PhotonNetwork.LocalPlayer.SetCustomProperties(setRoomProperties);
        readyButton.gameObject.SetActive(true);
        readyTxt.text = "Successfully paired";
    }

    public void Z_StartGame ()
    {
        photonView.RPC("MainStartGameRPC", RpcTarget.All);
    }

    [PunRPC]
    public IEnumerator MainStartGameRPC()
    {
        gameReady.SetActive(true);

        slotPlayerList = new List<GameObject>();
        foreach (var player in PhotonNetwork.PlayerList)
        {
            GameObject clone = Instantiate(slotPlayer, slotTrans);
            slotPlayerList.Add(clone);
            clone.SetActive(true);
        }

        StartCoroutine(ReadyCountdown());
        yield return 0; // if you allow 1 frame to pass, the object's OnDestroy() method gets called and cleans up references.
    }

    public void CheckReady()
    {
        UnReady();
        if (PhotonNetwork.CurrentRoom.PlayerCount == maxPlayersPerRoom && multiMode == MultiMode.QuickMatch)
        {
            Z_StartGame();
        }
    }

    IEnumerator ReadyCountdown()
    {
        float time = Time.time;
        while(Time.time < time + readyCount)
        {
            foreach (var players in PhotonNetwork.PlayerList)
            {
                if(players.CustomProperties.Count > 0 && (bool)players.CustomProperties["RTP"])
                {
                    //LogFeedback( players.ActorNumber + " Is Ready", "Red");
                    slotPlayerList[players.ActorNumber - 1].GetComponent<Image>().color = Color.white;
                }
            }
            yield return new WaitForSeconds(1);

            if (PhotonNetwork.IsMasterClient)
            {
                if (CheckPlayerReady())
                {
                    PhotonNetwork.LoadLevel("Multi");
                }
            }
            LogFeedback("Count down : " + ((int)((time + readyCount) - Time.time )),"Red");
        }
    }

    public bool CheckPlayerReady()
    {
        foreach (var players in PhotonNetwork.PlayerList)
        {
            if (players.CustomProperties.Count > 0 && !(bool)players.CustomProperties["RTP"])
            {
                //LogFeedback("Failed Ready");
                return false;
            }
        }
        //LogFeedback("Sucsess Ready");
        return true;
    }

    public void Back()
    {

    }
}
