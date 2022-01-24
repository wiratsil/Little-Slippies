using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Tangtang : Character
{
    float startTime;
    float acceleration;

    protected override void DoUlti(float duration = 9999f)
    {
        base.DoUlti(7);
        godMode = true;
    }

    protected override void Ultimate()
    {
        base.Ultimate();

        if (ulti.duration < 0.5)
        {
            anim.SetTrigger("FUlti");
            godMode = false;
        }
    }
}
