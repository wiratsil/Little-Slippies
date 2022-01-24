using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Tentacle : Obstacle
{
    public override IEnumerator Active()
    {
        yield return null;
        //return base.Active();
    }
}
