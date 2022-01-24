using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_EBullet : Obstacle
{
    public Rigidbody2D rigid;

    public override void Trigger(Collider2D collision)
    {
        base.Trigger(collision);
        if(collision.tag == "Player")
            rigid.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
