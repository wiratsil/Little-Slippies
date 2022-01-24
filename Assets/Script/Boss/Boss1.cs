using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : Boss
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
        base.SetAnimBoss(a);
        StartCoroutine(DoDamage(a));
    }

    public IEnumerator DoDamage(string a)
    {
        yield return new WaitForSecondsRealtime(1);
        if (a == "At1" || a == "At2" || a == "MoveF")
        {
            character.DoEffect(EffectType.Shock);
        }
    }
}
