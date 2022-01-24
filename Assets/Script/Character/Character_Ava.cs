using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Ava : Character
{

    protected override void DoUlti(float duration = 9999f)
    {
        base.DoUlti(5);
        CurrentSpeed = 100;
        CurrentControl = 50;
        godMode = true;
    }

    protected override void Ultimate()
    {
        base.Ultimate();

        if (!ulti.working)
            godMode = false;
    }
    
}
