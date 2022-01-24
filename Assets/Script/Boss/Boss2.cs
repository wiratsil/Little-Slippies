using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : Boss
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
        StartCoroutine(DoDamage(a));
    }

    public IEnumerator DoDamage(string a)
    {
        yield return new WaitForSecondsRealtime(1);
    }
}
