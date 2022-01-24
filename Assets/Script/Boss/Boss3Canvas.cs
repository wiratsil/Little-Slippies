using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss3Event : UnityEvent<string,float>
{
    public static Boss3Event skill = new Boss3Event();
}
public class Boss3Canvas : MonoBehaviour
{
    public Animator animator;
    public float pos;
    bool side;
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        Boss3Event.skill.AddListener(DoSkill);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    private void Update()
    {
        var dist = (transform.position - Camera.main.transform.position).z;
        if (side)
        {
            var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
            transform.position = new Vector3(leftBorder, pos, 0);
        }
        else
        {
            var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
            transform.position = new Vector3(rightBorder, pos, 0);

        }
    }

    public void DoSkill(string ani, float po)
    {
        pos = po;
        if (ani == "At1")
        {
            side = false;
            StartCoroutine(Fire());
        }
        else
        {
            side = true;
        }
        animator.SetTrigger(ani);
    }

    IEnumerator Fire()
    {
        Rigidbody2D clone = null;
        yield return new WaitForSeconds(1.2f);
        clone = Instantiate(bullet, transform.position + Vector3.left * 5, Quaternion.identity).GetComponent<Rigidbody2D>();
        clone.AddForce(Vector2.left * 1000);

        yield return new WaitForSeconds(1f);
        clone = Instantiate(bullet, transform.position + Vector3.left * 5, Quaternion.identity).GetComponent<Rigidbody2D>();
        clone.AddForce(Vector2.left * 1000);
    }
}
