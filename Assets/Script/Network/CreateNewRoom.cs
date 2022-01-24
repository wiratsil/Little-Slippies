﻿using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class CreateNewRoom : MonoBehaviourPun
{
    [SerializeField]
    private Text _roomName;
    private Text RoomName
    {
        get { return _roomName; }
    }

    public void OnClick_CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 4 };

        if (PhotonNetwork.CreateRoom(RoomName.text, roomOptions, TypedLobby.Default))
        {
            print("Create room successfully");
        }
        else
        {
            print("Create room failed");
        }
    }

    private void OnPhotonCreateRoomFailed(object[] codeAndMessage)
    {
        print("Create Room Failed: " + codeAndMessage[1]);

    }

    private void OnCreatedRoom(short returnCode, string message)
    {
        print("Room Created Successfully");
    }
}