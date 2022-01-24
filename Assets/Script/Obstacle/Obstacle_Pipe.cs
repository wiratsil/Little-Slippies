using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Pipe : Obstacle
{
    Rigidbody2D rigid;
    Character character;
    int intId;
    public float force;

    private void Start()
    {

    }
    
    public override void Trigger(Collider2D collision)
    {
        rigid = collision.GetComponent<Rigidbody2D>();
        intId = collision.GetInstanceID();
        if (collision.tag == "Player")
        {
            character = collision.GetComponent<Character>();
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetInstanceID() == intId)
        {
            rigid = null;
            intId = 0;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (character != null && character.godMode)
            return;

        if (rigid != null)
        {
            rigid.AddForce(Vector2.up * force);
        }
    }


    private void Update()
    {

    }
    
}
