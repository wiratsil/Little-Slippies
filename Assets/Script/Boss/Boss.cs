using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    
    public Animator anim;
    public Character character;

    // Start is called before the first frame update
    void Start()
    {
        StartSetup();
    }

    public virtual void StartSetup()
    {
        character = GameObject.FindObjectOfType<Character>();
        BossAnimEvent.animEvent.AddListener(SetAnimBoss);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        BossFollow();
    }

    public virtual void BossFollow()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(character.transform.position.x + 12.5f, character.transform.position.y - 1.5f, 0), Time.deltaTime);
    }

    public virtual void SetAnimBoss(string a)
    {
        anim.SetTrigger(a);
    }
}
