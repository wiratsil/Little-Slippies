using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_PlasticBag : Obstacle
{

    public Transform target;

    private void Start()
    {
        //if(obstac)
        //    RandomEffect();
    }

    private void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.time) + Vector3.up * 0.5f;
        }
    }

    public override void Trigger(Collider2D collision)
    {
        base.Trigger(collision);
        target = collision.transform;
    }

    public override IEnumerator Active()
    {
        yield return new WaitForSecondsRealtime(durationEffect + 2f);
        Destroy();
    }
}
