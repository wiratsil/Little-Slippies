using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Animator anim;
    public bool obstac;
    public EffectType effect;
    public float durationEffect;
    [Space]
    public bool floating;
    public float floatingSpeed;
    public float floatPingpong;
    private Vector3 floatA;
    private Vector3 floatB;
    private Vector3 floatRA;
    private Vector3 floatRB;

    private void Start()
    {
        //if(obstac)
        //    RandomEffect();
        if(floating)
            transform.localEulerAngles = new Vector3(0, 0, Random.Range(-180, 180));

        floatA = new Vector3(transform.position.x, transform.position.y - floatPingpong, transform.position.z);
        floatB = new Vector3(transform.position.x, transform.position.y + floatPingpong, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Trigger(collision);
    }

    public virtual void Trigger(Collider2D collision)
    {
        if ( obstac && (collision.tag == "Player"|| collision.tag == "PlayerBullet"))
        {
            if(anim != null)
                anim.SetTrigger("Active");
            StartCoroutine(Active());
        }
    }

    private void Update()
    {
        Floating();
    }


    public virtual IEnumerator Active()
    {
        yield return null;
        effect = EffectType.None;
        yield return new WaitForSeconds(1.5f);

        Destroy();
    }

    public virtual void Movement()
    {

    }

    public virtual void Destroy()
    {
        DestroyImmediate(this.gameObject);
    }

    void RandomEffect()
    {
        int ran = Random.RandomRange(2, 7);
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        if (ran == 2)
            sprite.color = Color.red;
        else if (ran == 3)
            sprite.color = Color.yellow;
        else if (ran == 4)
            sprite.color = Color.blue;
        else if (ran == 5)
            sprite.color = Color.green;
        else if (ran == 6)
            sprite.color = Color.red + Color.blue;
        else if (ran == 7)
            sprite.color = Color.red + Color.yellow;

        effect = (EffectType)ran;
    }
    
    public void Floating()
    {
        if (!floating)
            return;

        float pingPong = Mathf.PingPong(Time.time * floatingSpeed, 1);
        transform.position = Vector3.Lerp(floatA, floatB, pingPong);

    }
}
