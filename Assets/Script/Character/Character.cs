using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public partial class Character : MonoBehaviour
{
    [SerializeField]
    protected Animator anim;
    [SerializeField]
    private AnimationList anima;
    [SerializeField]
    private Sprite ultiPose;
    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    public Rigidbody2D rigid;

    [SerializeField]
    public float m_BaseSpeed ;
    [SerializeField]
    public float m_BaseDefend ;
    [SerializeField]
    public float m_BaseControl ;

    [SerializeField]
    private float m_Speed;
    public float CurrentSpeed
    {
        get { return m_Speed; }
        set { m_Speed = value; }
    }

    [SerializeField]
    private float m_Defned;
    public float CurrentDefend
    {
        get { return m_Defned; }
        set { m_Defned = value; }
    }

    [SerializeField]
    private float m_Control;
    public float CurrentControl
    {
        get { return m_Control; }
        set { m_Control = value; }
    }

    private float m_Timer;


    private int m_CurrentAnimation;
    protected int CurrentAnimation
    {
        get { return m_CurrentAnimation; }
        set
        {
            m_CurrentAnimation = value;
            SetAnimation((AnimationList)m_CurrentAnimation);
        }
    }

    [SerializeField]
    float yMin = -4.4f;
    [SerializeField]
    float yMax = 4.4f;


    private bool moveUp;
    private bool moveDown;
    private bool breke;
    public bool godMode;

    [SerializeField]
    private Manner manner;

    [SerializeField]
    private Obstacle obstacle;


    [System.Serializable]
    private class Booster
    {
        public bool working;
        public float speed;
        public float duration;
        public float acceleration;
        public float startTime;
    }
    [SerializeField]
    private Booster booster = new Booster();

    [System.Serializable]
    private class Stuning
    {
        public bool working;
        public float duration;
    }
    [SerializeField]
    private Stuning stuning = new Stuning();

    [System.Serializable]
    private class Crashing
    {
        public bool working;
        public float duration;
        public float acceleration;
        public float startTime;
        public Vector2 direction;
    }
    [SerializeField]
    private Crashing crashing = new Crashing();

    [System.Serializable]
    private class Knocking
    {
        public bool working;
        public float duration;
    }
    [SerializeField]
    private Knocking knocking = new Knocking();

    [System.Serializable]
    private class Shocking
    {
        public bool working;
        public float duration;
    }
    [SerializeField]
    private Shocking shocking = new Shocking();

    [System.Serializable]
    private class Reduce
    {
        public bool working;
        public float duration;
    }
    [SerializeField]
    private Reduce reduceVision = new Reduce();

    [System.Serializable]
    protected class Ulti
    {
        public bool working;
        public float duration;
    }
    [SerializeField]
    protected Ulti ulti = new Ulti();
    protected float posPlay;
    protected float timingPos;
    [SerializeField]
    protected bool takeEffect;

    private void Start()
    {
        CurrentAnimation = (int)AnimationList.Move;
        SkillCheckEvent.@event.AddListener(() => DoBoost());

        CurrentSpeed = m_BaseSpeed;
        CurrentDefend = m_BaseDefend;
        CurrentControl = m_BaseControl;
    }

    private void Update()
    {

    }
    
    private void FixedUpdate()
    {
        if (GameManager_Story.instance.phase == PhaseGame.Playing || GameManager_Story.instance.phase == PhaseGame.AfterPlay)
            Movement();
        else
        {
            rigid.velocity = Vector2.zero ;
        }
    }
}
