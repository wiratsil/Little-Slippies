using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ControllerJoystck_Multi : ControllerJoystck
{

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Controller();
    }

    public override void Controller()
    {
        if (PhotonNetwork.IsConnected && character == null)
        {
            Character[] characters = GameObject.FindObjectsOfType<Character>();
            foreach (Character c in characters)
            {
                if (c.GetComponent<PhotonView>().IsMine)
                {
                    character = c;
                    return;
                }
            }
        }
        base.Controller();
    }


}
