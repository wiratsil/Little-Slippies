using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;


public class StoryEvent : UnityEvent
{
    public static StoryEvent open = new StoryEvent();
    public static StoryEvent close = new StoryEvent();
}
public class StoryEventI : UnityEvent<Chapter>
{
    public static StoryEventI open = new StoryEventI();
}

public class StoryManager : MonoBehaviour
{
    public StoryData data;

    [Space]
    public Image mainBackground;
    public Image character1;
    public Image character2;
    public TextMeshProUGUI txt;
    [Space]
    public List<PhaseGame> phaseGame;
    [Space]
    public List<Chapter> chapters;
    public List<AudioClip> soundChapter;

    private CanvasGroup canvasGroup;
    

    private void Awake()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        StoryEvent.open.AddListener(Open);
        StoryEventI.open.AddListener(Open);
        StoryEvent.close.AddListener(Close);
        GameManagerEvent.phaseGame.AddListener(GetPhaseGame);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetPhaseGame(PhaseGame phase)
    {
        if (phase != PhaseGame.AfterPlay)
        {
            foreach (PhaseGame p in phaseGame)
            {
                if (p == phase)
                {
                    Open();
                    break;
                }
            }
        }
        else if (phase == PhaseGame.AfterPlay)
        {
            StartCoroutine(StartStoryDelay(5));
        }
    }

    public IEnumerator StartStoryDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Open();
    }

    public void SetData()
    {
        data.chapterData = new List<ChapterData>();
        data.chapterData.AddRange(chapters[0].dataArray);
        chapters.RemoveAt(0);
        phaseGame.RemoveAt(0);
        SoundEvent.playBGM.Invoke(soundChapter[0]);
        soundChapter.RemoveAt(0);
    }
    public void SetData(Chapter chapter)
    {
        data.chapterData = new List<ChapterData>();
        data.chapterData.AddRange(chapter.dataArray);
    }

    public void Z_PressSkip()
    {
        data.nextPart = true;
    }
    public void Z_PressSkipEnd()
    {
        StopAllCoroutines();
        StartCoroutine(FinishStory());
    }

    public void Open(Chapter chapter)
    {
        SetData(chapter);
        StartCoroutine(ShowStory(data.chapterData));
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public virtual IEnumerator ShowStory(List<ChapterData> chapterDatas)
    {
        for (int i = 0; i < data.chapterData.Count; i++)
        {
            txt.text = "";

            StartCoroutine(ChangeCharacter(
                    data.GetCharacter(data.chapterData[i].Character1),
                    data.GetCharacter(data.chapterData[i].Character2),
                    data.chapterData[i].Highlight,
                    data.GetScene(data.chapterData[i].Scene)));

            data.durationTime = 0.05f;
            
            for (int j = 0; j < data.chapterData[i].Dialogue.Length; j++)
            {
                txt.text += data.chapterData[i].Dialogue[j];
                yield return new WaitForSecondsRealtime(0.01f);
                if (data.nextPart)
                {
                    txt.text = data.chapterData[i].Dialogue;
                    j = data.chapterData[i].Dialogue.Length;
                    data.nextPart = false;
                }
            }
            float timer = 0;
            while (!data.nextPart)
            {
                yield return null;

                timer += Time.deltaTime;
                if(timer > 10)
                    data.nextPart = true;
            }
            data.nextPart = false;
        }
        StartCoroutine(FinishStory());
    }

    public virtual IEnumerator FinishStory()
    {
        if (GameManager_Story.instance.phase == PhaseGame.BeforePlay && phaseGame.Contains(GameManager_Story.instance.phase))
        {
            Open();
        }
        else if(GameManager_Story.instance.phase == PhaseGame.BeforePlay)
        {
            GameManager_Story.instance.SetGamePhase(PhaseGame.Start);
            Close();
        }
        else if (GameManager_Story.instance.phase == PhaseGame.Story)
        {
            GameManager_Story.instance.SetGamePhase(PhaseGame.Playing);
            Close();
        }
        if (GameManager_Story.instance.phase == PhaseGame.AfterPlay)
        {
            GameManager_Story.instance.Unlock();
            Close();
        }
        yield return null;
    }


    public IEnumerator ChangeCharacter(Sprite cha1 = null, Sprite cha2 = null, int highlight = 0, Sprite scene = null)
    {
        yield return null;

        mainBackground.sprite = scene;

        if (cha1 != null)
        {
            character1.sprite = cha1;
            character1.color = Color.white;
        }

        if (cha2 != null)
        {
            character2.sprite = cha2;
            character2.color = Color.white;
        }

        switch (highlight)
        {
            case 0:
                mainBackground.color = Color.white;
                character1.color = Color.white;
                character2.color = Color.white;
                break;
            case 1:
                mainBackground.color = Color.gray;
                character1.color = Color.white;
                character2.color = Color.gray;
                break;
            case 2:
                mainBackground.color = Color.gray;
                character1.color = Color.gray;
                character2.color = Color.white;
                break;
        }
        if(cha1 == null)
            character1.color = Color.clear;
        if(cha2 == null)
            character2.color = Color.clear;
        
    }

    public void Open()
    {
        SetData();
        StartCoroutine(ShowStory(data.chapterData));
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }
    public void Close()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }

}
