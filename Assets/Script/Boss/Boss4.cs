using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4 : Boss
{
    public bool following;
    public float offsetFollow;
    public float speed;

    public GameObject bullet;
    public float force;
    Character player;
    bool followRight;

    private void Awake()
    {
        player = GameObject.FindObjectOfType<Character>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartSetup();
    }

    private void Update()
    {

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        BossFollow();
        FollowRight();
    }
    
    public override void SetAnimBoss(string a)
    {
        speed = 1;
        StopAllCoroutines();

        if (a == "MoveOn")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 10, transform.position.z);
            anim.SetTrigger("Move");
        }
        if (a == "At1")
        {
            int ran = Random.Range(0, 2);
            if (ran == 0)
            {
                StartCoroutine(Attack1_1());
            }
            else
            {
                StartCoroutine(Attack1_2());
            }

        }
        else if (a == "At2")
        {
            StartCoroutine(Attack2());
        }
        else if (a == "At3")
        {
            StartCoroutine(Attack3());
        }
    }

    public override void BossFollow()
    {
        if(following)
            transform.position = Vector3.Lerp(transform.position, new Vector3(character.transform.position.x + 18, character.transform.position.y - 1.5f, 0), Time.deltaTime);
    }

    public IEnumerator DoDamage(string ani, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Boss3Event.skill.Invoke(ani,character.transform.position.y);
    }

    public IEnumerator Attack1_1()
    {
        following = false;

        var dist = (transform.position - Camera.main.transform.position).z;
        float timer = 0;

        anim.SetTrigger("DoL");
        while(timer < 1.5f)
        {
            yield return null;
            timer += Time.deltaTime;
            var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(0.9f, 0, dist)).x;
            transform.position = Vector3.Lerp(transform.position, new Vector3(rightBorder, 0, 0), Time.deltaTime * 100);
        }

        while ((transform.position.x - Camera.main.ViewportToWorldPoint(new Vector3(0.1f, 0, dist)).x) > 0.1)
        {
            yield return null;
            speed += Time.deltaTime * speed;
            var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0.1f, 0, dist)).x;
            transform.position = Vector3.Lerp(transform.position, new Vector3(leftBorder, 0, 0), Time.deltaTime * speed);
        }
        
        anim.SetTrigger("DoR");
        timer = 0;
        while (timer < 1.5f)
        {
            yield return null;
            timer += Time.deltaTime;
            var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0.05f, 0, dist)).x;
            transform.position = Vector3.Lerp(transform.position, new Vector3(leftBorder, 0, 0), Time.deltaTime * 100);
        }

        while (Mathf.Abs((transform.position.x - Camera.main.ViewportToWorldPoint(new Vector3(0.9f, 0, dist)).x)) > 0.3)
        {
            yield return null;
            speed += Time.deltaTime * speed;
            var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(0.9f, 0, dist)).x;
            transform.position = Vector3.Lerp(transform.position, new Vector3(rightBorder, 0, 0), Time.deltaTime * speed);
        }
        anim.SetTrigger("Move");

        following = true;
    }

    public IEnumerator Attack1_2()
    {
        following = false;

        var dist = (transform.position - Camera.main.transform.position).z;
        float timer = 0;

        anim.SetTrigger("DoL");
        while (timer < 1.5f)
        {
            yield return null;
            timer += Time.deltaTime;
            var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(0.9f, 0, dist)).x;
            var rightBorderY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.9f, dist)).y;

            float xxx = Mathf.Lerp(transform.position.x, rightBorder, Time.deltaTime * 50);
            float yyy = Mathf.Lerp(transform.position.y, rightBorderY, Time.deltaTime * 2);
            transform.position = new Vector3(xxx, yyy, 0);
        }

        while ((transform.position.x - Camera.main.ViewportToWorldPoint(new Vector3(0.1f, 0, dist)).x) > 0.1)
        {
            yield return null;
            speed += Time.deltaTime * speed;
            var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0.1f, 0, dist)).x;
            var leftBorderY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.1f, dist)).y;
            transform.position = Vector3.Lerp(transform.position, new Vector3(leftBorder, leftBorderY, 0), Time.deltaTime * speed);
        }

        anim.SetTrigger("DoR");
        timer = 0;
        while (timer < 1.5f)
        {
            yield return null;
            timer += Time.deltaTime;
            var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0.05f, 0, dist)).x;
            var leftBorderY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.9f, dist)).y;
            
            float xxx = Mathf.Lerp(transform.position.x, leftBorder, Time.deltaTime * 50);
            float yyy = Mathf.Lerp(transform.position.y, leftBorderY, Time.deltaTime * 2);
            transform.position = new Vector3(xxx, yyy, 0);
            
        }

        while (Mathf.Abs((transform.position.x - Camera.main.ViewportToWorldPoint(new Vector3(0.9f, 0, dist)).x)) > 0.3)
        {
            yield return null;
            speed += Time.deltaTime * speed;
            var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(0.9f, 0, dist)).x;
            var rightBorderY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.1f, dist)).y;
            transform.position = Vector3.Lerp(transform.position, new Vector3(rightBorder, rightBorderY, 0), Time.deltaTime * speed);
        }
        anim.SetTrigger("Move");

        following = true;
    }

    public void FollowRight()
    {
        if (followRight)
        {
            var dist = (transform.position - Camera.main.transform.position).z;
            var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(0.9f, 0, dist)).x;
            transform.position = Vector3.Lerp(transform.position, new Vector3(rightBorder, 0, 0), Time.deltaTime * 100);
        }
    }

    public IEnumerator Attack2()
    {
        anim.SetTrigger("At2");

        following = false;
        followRight = true;
        
        yield return new WaitForSeconds(2.5f);
        Fire();
        yield return new WaitForSeconds(1);
        Fire();
        yield return new WaitForSeconds(1);
        Fire();
        yield return new WaitForSeconds(1);

        anim.SetTrigger("Move");
        following = true;
        followRight = false;
    }

    public void Fire()
    {
        Vector3 direct;
        Vector3 spawTran = Vector3.zero;

        direct = (player.transform.position - transform.position).normalized ;
        spawTran = transform.position + new Vector3(-1f, 0);
        Rigidbody2D clone = Instantiate(bullet, spawTran, Quaternion.identity).GetComponent<Rigidbody2D>();
        clone.AddForce(direct * force);
    }


    public IEnumerator Attack3()
    {
        yield return null;

        anim.SetTrigger("Ulti");

    }
}
