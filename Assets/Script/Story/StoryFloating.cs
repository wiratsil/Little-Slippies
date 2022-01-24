using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class StoryFloatEvent : UnityEvent
{
    public static StoryFloatEvent activeStory = new StoryFloatEvent();
}

public class StoryFloating : MonoBehaviour
{
    public StoryData data;
    [Space]
    private CanvasGroup canvasGroup;
    public float idleTime;
    public float closeTime;
    [Space]
    public Image mainBackground;
    public Image character1;
    public Image character2;
    public TextMeshProUGUI txt;

    private void Awake()
    {
        StoryFloatEvent.activeStory.AddListener(StartStory);
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Z_PressSkip()
    {
        data.nextPart = true;
    }

    public void StartStory()
    {
        StartCoroutine(ShowStory(data.chapterData));
    }

    public virtual IEnumerator ShowStory(List<ChapterData> chapterDatas)
    {
        canvasGroup.alpha = 1;
        for (int i = 0; i < data.chapterData.Count; i++)
        {
            txt.text = "";

            StartCoroutine(ChangeCharacter(
                    data.GetCharacter(data.chapterData[i].Character1),
                    data.GetCharacter(data.chapterData[i].Character2),
                    data.chapterData[i].Highlight,
                    data.GetScene(data.chapterData[i].Scene)));

            data.durationTime = 0.05f;

            if (data.nextPart && txt.text.Length == data.chapterData[i].Dialogue.Length)
            {
                data.nextPart = false;
                continue;
            }
            else if (data.nextPart)
            {
                txt.text = data.chapterData[i].Dialogue;
            }
            else
            {
                for (int j = 0; j < data.chapterData[i].Dialogue.Length; j++)
                {
                    txt.text += data.chapterData[i].Dialogue[j];
                    yield return new WaitForSecondsRealtime(0.01f);
                }
            }


            data.nextPart = false;
            float timer = 0;
            while (!data.nextPart)
            {
                timer += Time.deltaTime;
                if (timer > idleTime)
                {
                    canvasGroup.alpha = 0;
                }
                if (timer > idleTime + closeTime)
                {
                    data.nextPart = true;
                    canvasGroup.alpha = 1;
                }

                yield return null;
            }
            data.nextPart = false;
        }
        StartCoroutine(FinishStory());
    }

    public virtual IEnumerator FinishStory()
    {
        canvasGroup.alpha = 0;
        yield return null;
    }


    public IEnumerator ChangeCharacter(Sprite cha1 = null, Sprite cha2 = null, int highlight = 0, Sprite scene = null)
    {
        yield return null;

        //data.mainBackground.sprite = scene;

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
                //data.mainBackground.color = Color.white;
                character1.color = Color.white;
                character2.color = Color.white;
                break;
            case 1:
                //data.mainBackground.color = Color.gray;
                character1.color = Color.white;
                character2.color = Color.gray;
                break;
            case 2:
                //data.mainBackground.color = Color.gray;
                character1.color = Color.gray;
                character2.color = Color.white;
                break;
        }
        if (cha1 == null)
            character1.transform.parent.gameObject.SetActive(false);
        else
            character1.transform.parent.gameObject.SetActive(true);

        if (cha2 == null)
            character2.transform.parent.gameObject.SetActive(false);
        else
            character2.transform.parent.gameObject.SetActive(true);



    }

}
