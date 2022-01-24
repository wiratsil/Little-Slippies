using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public virtual void Movement()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (obstacle != null && obstacle.obstac)
        {
            
        }
        else if (obstacle != null && !obstacle.obstac)
        {

        }
        if(collision.tag != "Player")
            StartCoroutine(ActiveDestroy());
    }


    public virtual IEnumerator ActiveDestroy()
    {
        anim.SetTrigger("Active");
        yield return new WaitForSeconds(1);

        Destroy();
    }

    public virtual void Destroy()
    {
        DestroyImmediate(this.gameObject);
    }
}
