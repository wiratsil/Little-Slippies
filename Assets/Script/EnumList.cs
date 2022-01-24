using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationList
{
    None,
    Idle,
    Move,
    Boost,
    Ulti,
    Crash,
    Knock,
    Stun,
    Shock,
    UseItem
}

public enum Manner
{
    None,
    Idle,
    Move,
    Boost,
    Knock,
    Stun,
    Crash,
    Shock
}

public enum EffectType
{
    None,
    Increase,
    Decrease,
    Crash,
    Knock,
    Shock,
    Stun,
    ReduceVision,
    Guard
}

public enum ItemType
{
    SingleAttack,
    TeamAttack,
    SinglePowerUp,
    TeamPowerUp,
    Trap
}

public enum PhaseGame
{
    None,
    BeforePlay,
    Start,
    Playing,
    AfterPlay,
    Story,
    End
}

public enum Name
{
    Anda,
    Ava
}

public class EnumList : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
