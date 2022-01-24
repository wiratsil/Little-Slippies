using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ControllerJoystck_Story : ControllerJoystck
{
   
    private void Awake()
    {
        character = GameObject.FindObjectOfType<Character>();
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
        base.Controller();
    }


}
