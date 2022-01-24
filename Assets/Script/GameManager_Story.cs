using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;

using TMPro;

public class GameManagerEvent : UnityEvent<PhaseGame>
{
    public static GameManagerEvent phaseGame = new GameManagerEvent();
}

public class GameManager_Story : GameManager
{
    public static GameManager_Story instance;

    public GameObject obj;
    public Character player;
    public float distanceMap;
    [SerializeField]
    private float timeLimit;
    [SerializeField]
    private int timeCountdoown;
    [SerializeField]
    private int minute;
    [SerializeField]
    private int sec;
    [SerializeField]
    private int milisec;

    [SerializeField]
    private Text timer;
    [SerializeField]
    private Button newGame;
    [SerializeField]
    private Text result;

    [SerializeField]
    private Slider miniMap;

    private Text spText;

    public GameObject pauseObj;

    public float countingStart = 3.9f;

    public GameObject counting;
    [SerializeField]
    private Image ultiPose;

    public Image reduceVision;

    [System.Serializable]
    public class UnlockReward
    {
        public GameObject unlock;
        public Image reward;
        public TextMeshProUGUI text;
        public Sprite spriteReward;
    }
    public UnlockReward unlockReward;

    [Space]
    [SerializeField]
    private PhaseGame _phase;
    public PhaseGame phase { get { return _phase; } }

    public int chapterStory;
    public float maxTimeLimit;

    public AudioClip goalSOund;
    public AudioClip inGameSound;

    private void Awake()
    {

        if (GameManager_Story.instance != null && GameManager_Story.instance != this)
        {
            Destroy(transform.gameObject);
            return;
        }
        instance = this;

        player = GameObject.FindObjectOfType<Character>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetGamePhase(PhaseGame.BeforePlay);
        //    InvokeRepeating("SpawnOb",1,0.7f);
        maxTimeLimit = timeLimit;

        PlayerPrefs.SetInt("PScene", 1);
    }

    // Update is called once per frame
    void Update()
    {
        GameManager();
    }

    public void SetGamePhase(PhaseGame phaseGame)
    {
        if (phase == phaseGame)
            return;

        _phase = phaseGame;

        GameManagerEvent.phaseGame.Invoke(_phase);

        switch (phase)
        {
            case PhaseGame.None:
                break;

            case PhaseGame.BeforePlay:
                break;

            case PhaseGame.Start:
                StartGame();
                break;

            case PhaseGame.Playing:
                break;

            case PhaseGame.AfterPlay:
                break;

            case PhaseGame.End:
                SceneManager.LoadScene("Main");
                break;
        }

    }

    public void GameManager ()
    {
        if (phase == PhaseGame.Playing)
        {
            MiniMap();
            Timing();
        }
    }
    
    public void Timing()
    {
        if (player.transform.position.x > distanceMap)
        {
            result.text = "Goal";
            player.CurrentControl = 0;

            SetGamePhase(PhaseGame.AfterPlay);
            int startScore = 0;
            if ((timeCountdoown * 100) / maxTimeLimit < 10)
            {
                startScore = 1;
            }
            else if((timeCountdoown * 100) / maxTimeLimit <= 15)
            {
                startScore = 2;
            }
            else if ((timeCountdoown * 100) / maxTimeLimit >= 20)
            {
                startScore = 3;
            }
            PlayerPrefs.SetInt("ChapterScore" + chapterStory, startScore);
            PlayerPrefs.Save();
            SaveSystem.Instance.saveData.saveChapter[chapterStory].star = startScore;

            SoundEvent.play.Invoke(goalSOund);

            return;
        }
        if (timeCountdoown <= 0.00000)
        {
            minute = 0;
            sec = 0;
            milisec = 0;
            result.text = "Game over";
            newGame.gameObject.SetActive(true);
            player.rigid.constraints = RigidbodyConstraints2D.FreezeAll; 
            timer.text = string.Format("{0:D2}:{1:D2}:{2:D2}", minute, sec, milisec);
            SetGamePhase(PhaseGame.None);
            return;
        }

        timeCountdoown = (int)(timeLimit - Time.time);
        minute = (int)(timeLimit - Time.time) / 60;
        sec = (int)(timeLimit - Time.time) % 60;
        milisec = (int)((timeLimit - Time.time) * 100) % 100;
        timer.text = string.Format("{0:D2}:{1:D2}:{2:D2}", minute, sec, milisec);
        //player = GameObject.FindGameObjectWithTag("Player");
    }

    public void MiniMap()
    {
        miniMap.value = ((player.transform.position.x * 100) / distanceMap) / 100;
    }
    public void SpawnOb()
    {
        float xx = Random.RandomRange(player.transform.position.x + 40, player.transform.position.x + 100);
        float yy = Random.RandomRange(-4.5f, 4.5f);
        float s = player.transform.position.x / 1000 < 0.5 ? 0.5f : (player.transform.position.x / distanceMap);
        Vector3 pos = new Vector3(xx, -4, 0);
        GameObject clone = Instantiate(obj, pos, Quaternion.identity);
        int ran = Random.RandomRange(2, 7);
        clone.GetComponent<Obstacle>().effect = (EffectType)ran;
        clone.transform.localScale = new Vector3(s, s, s);
        clone.SetActive(true);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddTime()
    {
       // timeLimit += 60;
    }


    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseObj.SetActive(true);
    }

    public void StartGame()
    {
        StartCoroutine(_StartGame());
        StoryFloatEvent.activeStory.Invoke();
        SoundEvent.playBGM.Invoke(inGameSound);
    }

    public IEnumerator _StartGame()
    {
        counting.SetActive(true);

        while (countingStart > 0)
        {
            countingStart -= Time.deltaTime;
            yield return null;
        }

        timeLimit = timeLimit + Time.time;
        SetGamePhase(PhaseGame.Playing);
        countingStart = 4;
        counting.SetActive(false);

    }

    public void UltiPose(Sprite sprite)
    {
        StartCoroutine(_UltiPose(sprite));
    }

    private IEnumerator _UltiPose(Sprite sprite)
    {
        float timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime;
            ultiPose.sprite = sprite;
            ultiPose.color = Color.Lerp(Color.clear , new Color (1,1,1,0.7f), timer*2 );
            ultiPose.rectTransform.localScale = Vector3.Lerp(new Vector3(0,0,0), new Vector3 (1,1,1), timer * 4);
            ultiPose.rectTransform.localPosition = Vector2.Lerp(new Vector2(-1500, 0), new Vector2(0  , 0), timer * 4);
            yield return null;
        }

        ultiPose.color = Color.clear;
    }

    public void Unlock()
    {
        StartCoroutine(_Unlock());
    }

    private IEnumerator _Unlock()
    {
        unlockReward.unlock.SetActive(true);
        unlockReward.reward.sprite = unlockReward.spriteReward;
        yield return new WaitForSecondsRealtime(3);
        bool action = false;
        while (!action)
        {
            if (Input.anyKey)
                action = true;

            yield return null;
        }

        unlockReward.unlock.SetActive(false);
        SetGamePhase(PhaseGame.End);
    }
    

}
