using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Crab : Obstacle
{

    public Transform target;
    public float speed;
    
    public float pingpongPos;

    Vector3 a;
    Vector3 b;

    private void Start()
    {
        //if(obstac)
        //    RandomEffect();
        a = new Vector3(transform.position.x - pingpongPos, transform.position.y, transform.position.z);
        b = new Vector3(transform.position.x + pingpongPos, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        if (target == null)
        {
            float pingPong = Mathf.PingPong(Time.time * speed, 1);
            transform.position = Vector3.Lerp(a, b, pingPong);
        }
        
    }

    public override void Trigger(Collider2D collision)
    {
        base.Trigger(collision);
        target = null;
    }

    public override IEnumerator Active()
    {
        yield return new WaitForSecondsRealtime(durationEffect + 2f);
        //Destroy();
    }

    public override void Destroy()
    {

    }
}
