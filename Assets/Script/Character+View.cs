using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Character : MonoBehaviour
{
    public void P_MoveUp(bool bo)
    {
        moveUp = bo;
    }

    public void P_MoveDown(bool bo)
    {
        moveDown = bo;
    }

    public void P_Boost()
    {
        SkillCheckEvent.check.Invoke();
    }

    public void P_Breke(bool bo)
    {
        breke = bo;

        if (breke)
            StartCoroutine(Breke());
    }

    public void P_Ulti()
    {
        DoUlti();
        GameManager_Story.instance.UltiPose(ultiPose);
    }

    public void P_Item()
    {

    }

    public void P_Fire()
    {
        DoFire();
    }
}
