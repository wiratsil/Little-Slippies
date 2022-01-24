using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
public partial class Character : MonoBehaviour
{

    private void Movement()
    {
        //Camera following
        if (transform.position.x < GameManager_Story.instance.distanceMap)
            Camera.main.transform.position = new Vector3(transform.position.x + 5, 0, -10);
        //Move up
        if (moveUp)
        {
            rigid.velocity = (Vector2.right * CurrentSpeed * Time.deltaTime) + (Vector2.up * CurrentControl * Time.deltaTime);
        }//Move down
        else if (moveDown)
        {
            rigid.velocity = (Vector2.right * CurrentSpeed * Time.deltaTime) + (Vector2.down * CurrentControl * Time.deltaTime);
        }
        else
        {
            rigid.velocity = (Vector2.right * CurrentSpeed * Time.deltaTime);
        }
        //จำกัดขอบเขต
        rigid.position = new Vector2(rigid.position.x, Mathf.Clamp(rigid.position.y, yMin, yMax));

        if (posPlay >= transform.position.x && !takeEffect)
        {
            timingPos += Time.deltaTime;

            if (timingPos > 3)
            {
                //transform.position = new Vector3(posPlay -5 , 0 , 0);
                rigid.AddForce(Vector2.left * 1000);
                if (timingPos > 4)
                    timingPos = 0;
            }
        }
        else
            posPlay = transform.position.x;

        Boost();
        Stun();
        Crash();
        Knock();
        Shock();
        ReduceVision();
        Ultimate();
    }

    public void SetAnimation(AnimationList ani)
    {
        anima = ani;
        anim.SetTrigger(anima.ToString());
    }

    public void DoEffect(EffectType effectType)
    {
        if (godMode)
            return;

        switch (effectType)
        {
            case EffectType.Knock:
                DoKnock();
                break;
            case EffectType.Shock:
                DoShock();
                break;
            case EffectType.Stun:
                DoStun();
                break;
            case EffectType.Crash:
                DoCrash(obstacle.transform.position);
                break;
            case EffectType.Decrease:
                DoBoost(0.5f, 2, false);
                break;
            case EffectType.ReduceVision:
                DoReduce();
                break;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (godMode)
            return;

        if (collision.tag == "Obstacle")
        {
            obstacle = collision.GetComponent<Obstacle>();

            if (!obstacle.obstac)
                return;
            
            switch (obstacle.effect)
            {
                case EffectType.Knock:
                    DoKnock(obstacle.durationEffect);
                    break;
                case EffectType.Shock:
                    DoShock(obstacle.durationEffect);
                    break;
                case EffectType.Stun:
                    DoStun(obstacle.durationEffect);
                    break;
                case EffectType.Crash:
                    DoCrash(obstacle.transform.position);
                    break;
                case EffectType.Decrease:
                    DoBoost(0.5f, 2, false);
                    break;
                case EffectType.ReduceVision:
                    DoReduce(obstacle.durationEffect);
                    break;

            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (godMode)
            return;

        if (collision.collider.tag == "Obstacle")
        {
            obstacle = collision.collider.GetComponent<Obstacle>();

            switch (obstacle.effect)
            {
                case EffectType.Knock:
                    DoKnock(obstacle.durationEffect);
                    break;
                case EffectType.Stun:
                    DoStun(obstacle.durationEffect);
                    break;
                case EffectType.Crash:
                    DoCrash(obstacle.transform.position);
                    break;
                case EffectType.Decrease:
                    DoBoost(0.5f, 0.5f, false);
                    break;
                case EffectType.ReduceVision:
                    DoReduce(obstacle.durationEffect);
                    break;

            }
        }
    }

    private void GetReduceVision()
    {
        CurrentAnimation = (int)AnimationList.Crash;
    }

    private void GetGuard()
    {
        CurrentAnimation = (int)AnimationList.UseItem;
    }


    protected void CheckAnimation(AnimationList ani)
    {
        if (CurrentAnimation == (int)ani)
        {
            CurrentAnimation = (int)AnimationList.Move;
        }
    }

    private IEnumerator Breke()
    {
        while (breke && CurrentSpeed > 100)
        {
            CurrentSpeed -= Time.deltaTime * 300;
            yield return null;
        }
        yield return new WaitUntil(() => !breke);
        CurrentSpeed = m_BaseSpeed;
    }

    private void DoBoost(float multiSpeed = 2.5f, float durationSpeed = 3f, bool power = true, float accelerationTime = 0.3f)
    {
        if (!booster.working)
        {
            booster.startTime = Time.time;
            if (power)
                CurrentAnimation = (int)AnimationList.Boost;
            else
                CurrentAnimation = (int)AnimationList.Crash;
        }
        booster.working = true;
        booster.speed = multiSpeed;
        booster.duration = durationSpeed;
        booster.acceleration = accelerationTime;
        takeEffect = true;
    }
    private void Boost()
    {
        if (booster.working)
        {
            if (CurrentSpeed != (m_BaseSpeed * booster.speed))
            {
                float t = (Time.time - booster.startTime) / booster.acceleration;
                CurrentSpeed = Mathf.SmoothStep(m_BaseSpeed, m_BaseSpeed * booster.speed, t);
            }

            booster.duration -= Time.deltaTime;


            //if (CurrentAnimation != (int)3)
            //{
            //    booster.duration = 0;
            //}

            if (booster.duration <= 0)
            {
                booster.working = false;
                booster.duration = 0;
                CurrentSpeed = m_BaseSpeed;
                CheckAnimation(AnimationList.Boost);
                takeEffect = false;
            }
        }
    }

    private void DoStun(float duration = 1.5f)
    {
        CurrentAnimation = (int)AnimationList.Stun;
        stuning.duration += duration;
        stuning.working = true;
        takeEffect = true;
    }
    private void Stun()
    {
        if (stuning.working)
        {
            CurrentSpeed = 100;
            CurrentControl = 0;

            stuning.duration -= Time.deltaTime;

            if (stuning.duration <= 0)
            {
                stuning.working = false;
                stuning.duration = 0;
                CurrentSpeed = m_BaseSpeed;
                CurrentControl = m_BaseControl;
                CheckAnimation(AnimationList.Stun);
                takeEffect = false;
            }
        }
    }

    private void DoCrash(Vector3 target = default(Vector3), float duration = 0.5f)
    {
        CurrentAnimation = (int)AnimationList.Crash;
        crashing.working = true;
        crashing.duration = duration;
        crashing.startTime = Time.time;
        crashing.direction = (transform.position - target).normalized;

        CurrentSpeed = CurrentSpeed / 1.4f;
        CurrentControl = CurrentControl / 1.4f;
        takeEffect = true;

    }
    private void Crash()
    {
        if (crashing.working)
        {
            CurrentSpeed = CurrentSpeed / 1.4f;
            CurrentControl = CurrentControl / 1.4f;

            crashing.duration -= Time.deltaTime;

            if (crashing.duration <= 0)
            {
                crashing.working = false;
                crashing.duration = 0;
                CurrentSpeed = m_BaseSpeed;
                CurrentControl = m_BaseControl;
                CheckAnimation(AnimationList.Crash);
                takeEffect = false;
            }
        }
    }

    private void DoKnock(float duration = 2f)
    {
        CurrentAnimation = (int)AnimationList.Knock;
        knocking.duration = duration;
        knocking.working = true;
        takeEffect = true;
    }
    private void Knock()
    {
        if (knocking.working)
        {
            CurrentSpeed = -200;
            CurrentControl = 0;

            knocking.duration -= Time.deltaTime;

            if (knocking.duration <= 0)
            {
                knocking.working = false;
                knocking.duration = 0;
                CurrentSpeed = m_BaseSpeed;
                CurrentControl = m_BaseControl;
                CheckAnimation(AnimationList.Knock);
                takeEffect = false;
            }
        }
    }


    private void DoShock(float duration = 2f)
    {
        CurrentAnimation = (int)AnimationList.Shock;
        shocking.duration = duration;
        shocking.working = true;
        takeEffect = true;
    }
    private void Shock()
    {
        if (shocking.working)
        {
            CurrentSpeed = 100;
            CurrentControl = 0;

            shocking.duration -= Time.deltaTime;

            if (shocking.duration <= 0)
            {
                shocking.working = false;
                shocking.duration = 0;
                CurrentSpeed = m_BaseSpeed;
                CurrentControl = m_BaseControl;
                CheckAnimation(AnimationList.Shock);
                takeEffect = false;
            }
        }
    }

    private void DoReduce(float duration = 4f)
    {
        reduceVision.working = true;
        reduceVision.duration = duration;
        GameManager_Story.instance.reduceVision.enabled = true;
        CheckAnimation(AnimationList.Crash);
        takeEffect = true;
    }
    private void ReduceVision()
    {
        if (reduceVision.working)
        {
            reduceVision.duration -= Time.deltaTime;

            if (reduceVision.duration <= 0)
            {
                reduceVision.working = false;
                reduceVision.duration = 0;
                GameManager_Story.instance.reduceVision.enabled = false;
                takeEffect = false;
            }
        }
    }

    protected virtual void DoUlti(float duration = 15f)
    {
        CurrentAnimation = (int)AnimationList.Ulti;
        ulti.duration += duration;
        ulti.working = true;
        takeEffect = true;
    }
    protected virtual void Ultimate()
    {
        if (ulti.working)
        {
            ulti.duration -= Time.deltaTime;

            if (ulti.duration <= 0)
            {
                ulti.working = false;
                ulti.duration = 0;
                CurrentSpeed = m_BaseSpeed;
                CurrentControl = m_BaseControl;
                CheckAnimation(AnimationList.Ulti);
                takeEffect = false;
            }
        }
    }


    protected virtual void DoFire()
    {
        CurrentAnimation = (int)AnimationList.UseItem;
        StartCoroutine(Fire());
    }
    protected virtual IEnumerator Fire()
    {
        yield return new WaitForSeconds(0.3f);
        GameObject clone = Instantiate(bullet, transform.position + Vector3.right * 2, transform.rotation);
    }
}
