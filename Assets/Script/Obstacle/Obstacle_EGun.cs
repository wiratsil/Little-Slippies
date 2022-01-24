using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_EGun : Obstacle
{
    Character player;
    public Obstacle bullet;
    public float force;
    public bool invert;
    public bool fire;
    private void Start()
    {

    }

    public override void Trigger(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.GetComponent<Character>();
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = null;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (player != null && !player.godMode)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("stand") && !fire)
            {
                anim.SetTrigger("Shoot");
                StartCoroutine(Shooting());
                fire = true;
            }
        }
    }

    IEnumerator Shooting()
    {
        yield return new WaitForSeconds(0.5f);

        if (player != null && fire)
        {
            Vector3 direct;
            Vector3 spawTran = Vector3.zero ;
            if (invert)
            {
                direct = (player.transform.position - transform.position).normalized + new Vector3(0, (float)Random.Range(-1, 0));
                spawTran = transform.position + new Vector3(-1f, -1);
            }
            else
            {
                direct = (player.transform.position - transform.position).normalized + new Vector3(0, (float)Random.Range(0, 1));
                spawTran = transform.position + new Vector3(-1f, 1);  
            }
            Rigidbody2D clone = Instantiate(bullet, spawTran, Quaternion.identity, gameObject.transform).GetComponent<Rigidbody2D>();
            clone.AddForce(direct * force);
            fire = false;
        }

    }

    private void Update()
    {

    }
    
}
