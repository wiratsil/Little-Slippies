using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : Obstacle
{
    public float speed;
    public float lifeTime = 15;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void SetAnimation(string ani)
    {
        anim.SetTrigger(ani);
    }

    public override void Movement()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);

        if (lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;

            if(lifeTime < 0)
                StartCoroutine(Active());
        }
    }
    
}
