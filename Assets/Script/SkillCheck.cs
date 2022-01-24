using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;


public class SkillCheckEvent : UnityEvent
{
    public static SkillCheckEvent @event = new SkillCheckEvent();
    public static SkillCheckEvent @check = new SkillCheckEvent();
}
public class SkillCheck : MonoBehaviour
{
    public RectTransform outline;
    public Animator animOutline;

    public RectTransform needle;
    public float speedNeedle;
    public bool roNeedle;

    public float min;
    public float max;

    public int countCheck;
    public int count;

    public CooldownButton cooldownButton;

    public enum BoostPhase
    {
        None,
        Pre,
        Start,
        Checking,
        Waiting,
        End
    }

    public BoostPhase boostPhase;

    private bool working; 
    // Start is called before the first frame update
    void Start()
    {
        SkillCheckEvent.check.AddListener(Check);
    }

    private void Update()
    {

        switch (boostPhase)
        {
            case BoostPhase.Pre:
                
                working = true;
                StartCoroutine(PreCheck());
                boostPhase = BoostPhase.Waiting;
                int randomRo = UnityEngine.Random.Range(0, 360);
               // gameObject.transform.localEulerAngles = new Vector3(0, 0, randomRo);
                needle.transform.localEulerAngles = new Vector3(0, 0, 350);
                count++;
                break;
            case BoostPhase.Start:
                boostPhase = BoostPhase.Checking;
                roNeedle = true;
                break;
            case BoostPhase.Checking:

                if(roNeedle)
                    needle.Rotate(Vector3.back * speedNeedle * Time.deltaTime);

                if (needle.rotation.eulerAngles.z > 350)
                {
                    StartCoroutine(DoMiss());
                }

                break;
            case BoostPhase.End:
                working = false;
                boostPhase = BoostPhase.None;
                count = 1;
                cooldownButton.Z_StartCooldown();
                break;
        }
    }

    void FixedUpdate()
    {

    }
    
    public void Check()
    {
        if (GameManager_Story.instance.phase != PhaseGame.Playing)
            return;

        roNeedle = false;

        if (!working && boostPhase == BoostPhase.None)
        {
            SkillCheckEvent.@event.Invoke();
            boostPhase = BoostPhase.Pre;
        }
        if (needle.eulerAngles.z >= min && needle.eulerAngles.z <= max && boostPhase == BoostPhase.Checking)
        {
            StartCoroutine(DoHit());
        }
        else if (boostPhase == BoostPhase.Checking)
        {
            StartCoroutine(DoMiss());
        }
        
    }
    

    private IEnumerator DoHit()
    {
        SkillCheckEvent.@event.Invoke();

        needle.gameObject.SetActive(false);
        animOutline.SetTrigger("Hit");

        yield return new WaitForSeconds(0.5f);

        outline.gameObject.SetActive(false);

        yield return new WaitForEndOfFrame();

        if(countCheck < count)
            boostPhase = BoostPhase.End;
        else
            boostPhase = BoostPhase.Pre;
    }

    private IEnumerator DoMiss()
    {
        needle.gameObject.SetActive(false);
        animOutline.SetTrigger("Miss");

        yield return new WaitForSeconds(0.5f);

        outline.gameObject.SetActive(false);

        boostPhase = BoostPhase.End;
    }

    private IEnumerator PreCheck()
    {
        yield return new WaitForSeconds(0.5f);

        needle.gameObject.SetActive(true);
        outline.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        boostPhase = BoostPhase.Start;
    }

    public void Z_CooldownBtn(CooldownButton cooldownButton)
    {
        this.cooldownButton = cooldownButton;
    }
}
