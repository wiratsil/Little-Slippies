using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Photon.Pun;
using Photon.Realtime;

public class PlayerGameNetwork : MonoBehaviour
{
    public static PlayerGameNetwork Instance;
    public string Name { get; private set; }
    
    private PhotonView photonView;

    public bool ready;

    // Update is called once per frame
    private void Awake()
    {
        DontDestroyOnLoad(this);

        Instance = this;

        Name = "Player #" + Random.Range(0, 9999);

        photonView = gameObject.GetComponent<PhotonView>();
        photonView.name = Name;
    }

    public void Ready()
    {
        photonView.RPC("ReadyToPlay", RpcTarget.All);
    }


    [PunRPC]
    public IEnumerator ReadyToPlay()
    {
        ready = true;
        //PhotonNetwork.Instantiate("Bomb", new Vector3(Random.Range(-3, 3), 0, 0), Quaternion.identity, 0);
        // transform.localScale = new Vector3(transform.localScale.x + 1, transform.localScale.y + 1, transform.localScale.z);
        yield return 0; // if you allow 1 frame to pass, the object's OnDestroy() method gets called and cleans up references.
    }
}