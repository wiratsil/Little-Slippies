using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3 : Boss
{
    // Start is called before the first frame update
    void Start()
    {
        StartSetup();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        BossFollow();
    }
    
    public override void SetAnimBoss(string a)
    {
        if (a == "MoveOn")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 10, transform.position.z);
        }
        base.SetAnimBoss(a);
        if (a == "At1")
        {
            StartCoroutine(DoDamage(a,1f));

        }
        else if (a == "At2")
        {
            StartCoroutine(DoDamage(a,1.25f));
        }
    }

    public override void BossFollow()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(character.transform.position.x + 18, character.transform.position.y - 1.5f, 0), Time.deltaTime);
    }

    public IEnumerator DoDamage(string ani, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Boss3Event.skill.Invoke(ani,character.transform.position.y);
    }
}
