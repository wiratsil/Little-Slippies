using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManagerChapter2 : StoryManager
{
    public Chapter chapter2_1;
    public Chapter chapter2_2;

    //public c chapter1_2;

    private void Awake()
    {
       // SetData1();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        if (GameManager_Story.instance.phase == PhaseGame.BeforePlay)
        {
            SetData1();
        }
        else if (GameManager_Story.instance.phase == PhaseGame.AfterPlay)
        {
            SetData2();
        }
        StartCoroutine(ShowStory(data.chapterData));

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(FinishStory());
        }
    }
    
    public override IEnumerator FinishStory()
    {
        if (GameManager_Story.instance.phase == PhaseGame.BeforePlay)
        {
            GameManager_Story.instance.SetGamePhase(PhaseGame.Start);
        }
        else if (GameManager_Story.instance.phase == PhaseGame.AfterPlay)
        {
            GameManager_Story.instance.Unlock();
        }
        return base.FinishStory();
    }

    public void SetData1()
    {
        data.chapterData = new List<ChapterData>();
        
        for (int i = 0; i < chapter2_1.dataArray.Count; i++)
        {
            ChapterData temp = new ChapterData();
            temp.Scene = chapter2_1.dataArray[i].Scene;
            temp.Character1 = chapter2_1.dataArray[i].Character1;
            temp.Character2 = chapter2_1.dataArray[i].Character2;
            temp.Highlight = chapter2_1.dataArray[i].Highlight;
            temp.Dialogue = chapter2_1.dataArray[i].Dialogue;
            data.chapterData.Add(temp);
        }
        PlayerPrefs.SetInt("Chapter1_1", 1);
    }


    public void SetData2()
    {
        data.chapterData = new List<ChapterData>();
        for (int i = 0; i < chapter2_2.dataArray.Count; i++)
        {
            ChapterData temp = new ChapterData();
            temp.Scene = chapter2_2.dataArray[i].Scene;
            temp.Character1 = chapter2_2.dataArray[i].Character1;
            temp.Character2 = chapter2_2.dataArray[i].Character2;
            temp.Highlight = chapter2_2.dataArray[i].Highlight;
            temp.Dialogue = chapter2_2.dataArray[i].Dialogue;
            data.chapterData.Add(temp);
        }
        PlayerPrefs.SetInt("Chapter1_2", 1);
    }
}
