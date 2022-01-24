using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class ControllerJoystck : MonoBehaviour
{
    [SerializeField]
    public VariableJoystick variableJoystick;
    [SerializeField]
    public Character character;

    public float midZone = 0.5f;

    public bool pressUp;
    public bool pressDown;

    void Start()
    {
        character = GameObject.FindObjectOfType<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        Controller();
    }

    public virtual void Controller ()
    {
        //if (variableJoystick.Vertical > midZone)
        //{
        //    Press_Up(true);
        //}
        //else if (variableJoystick.Vertical < - midZone)
        //{
        //    Press_Down(true);
        //}
        //else
        //{
        //    Press_Up(false);
        //    Press_Down(false);
        //}

        if (pressUp)
        {
            Press_Up(pressUp);
        }
        else if (pressDown)
        {
            Press_Down(pressDown);
        }
        else
        {
            Press_Up(false);
            Press_Down(false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Press_Boost();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Press_Ultimate();
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Press_Up(true);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Press_Down(true);
        }
    }


    public virtual void Press_Up(bool press)
    {
        if (GameManager_Story.instance.phase != PhaseGame.Playing)
            return;
        character.P_MoveUp(press);
    }
    public virtual void Press_UpBool(bool press)
    {
        pressUp = press;
    }

    public virtual void Press_Down(bool press)
    {
        if (GameManager_Story.instance.phase != PhaseGame.Playing)
            return;
        character.P_MoveDown(press);
    }
    public virtual void Press_DownBool(bool press)
    {
        pressDown = press;
    }


    public virtual void Press_Boost()
    {
        if (GameManager_Story.instance.phase != PhaseGame.Playing)
            return;
        character.P_Boost();
    }

    public virtual void Press_Breke(bool press)
    {
        if (GameManager_Story.instance.phase != PhaseGame.Playing)
            return;
        character.P_Breke(press);
    }

    public virtual void Press_UseItem()
    {
        if (GameManager_Story.instance.phase != PhaseGame.Playing)
            return;
        character.P_Item();
    }

    public virtual void Press_Ultimate()
    {
        if (GameManager_Story.instance.phase != PhaseGame.Playing)
            return;
        character.P_Ulti();
    }

    public virtual void Press_Fire()
    {
        if (GameManager_Story.instance.phase != PhaseGame.Playing)
            return;
        character.P_Fire();
    }

}
