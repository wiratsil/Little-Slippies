using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Anda : Character
{
    float startTime;
    float acceleration;

    protected override void DoUlti(float duration = 9999f)
    {
        base.DoUlti(duration);

        startTime = Time.time;
        acceleration = 3;
    }

    protected override void Ultimate()
    {
        base.Ultimate();
        if (!ulti.working)
            return;

        if (CurrentSpeed != (m_BaseSpeed * 3))
        {
            float t = (Time.time - startTime) / acceleration;
            CurrentSpeed = Mathf.SmoothStep(m_BaseSpeed, m_BaseSpeed * 3, t);
        }
        if (CurrentAnimation != (int)4)
        {
            ulti.duration = 0;
        }
    }
}
