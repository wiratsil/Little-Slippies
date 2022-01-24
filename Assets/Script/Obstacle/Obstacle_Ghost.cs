using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Ghost : Obstacle
{
    public Character character;
    public float speed;
    public float speedPong;
    public Rigidbody2D rigid;
    public float pingpongPos;

    float lastX;
    Vector3 a;
    Vector3 b;

    private void Start()
    {
        a = new Vector3(transform.position.x - pingpongPos, transform.position.y, transform.position.z);
        b = new Vector3(transform.position.x + pingpongPos, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        if (character == null)
        {
            float pingPong = Mathf.PingPong(Time.time * speedPong, 1);
            transform.position = Vector3.Lerp(a, b, pingPong);
        }


        if (transform.position.x >= lastX)
        {
            lastX = transform.position.x;
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else
        {
            lastX = transform.position.x;
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }

    public override void Trigger(Collider2D collision)
    {
        if ( obstac && (collision.tag == "Player"|| collision.tag == "PlayerBullet"))
        {
            character = collision.GetComponent<Character>();
            StartCoroutine(Active());
        }
    }
    

    public override IEnumerator Active()
    {
        CircleCollider2D circleCollider2D = GetComponent<CircleCollider2D>();
        circleCollider2D.enabled = false;
        float timer = 0;
        while (timer < 1)
        {
            yield return null;
            timer += Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position,character.transform.position , speed * Time.deltaTime);
        }

        if (anim != null)
            anim.SetTrigger("Active");
        
        circleCollider2D.enabled = true;
        effect = EffectType.Stun;

        yield return new WaitForSeconds(0.5f);
        effect = EffectType.None;
        circleCollider2D.enabled = false;
        obstac = false;
        yield return new WaitForSeconds(0.8f);
        Destroy();
    }

    public override void Movement()
    {

    }

    public override void Destroy()
    {
        DestroyImmediate(this.gameObject);
    }
}
