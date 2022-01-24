using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryFloatChapter2 : StoryFloating
{
    public Chapter chapter;
    // Start is called before the first frame update
    void Start()
    {
        SetData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetData()
    {
        data.chapterData = new List<ChapterData>();
        for (int i = 0; i < chapter.dataArray.Count; i++)
        {
            ChapterData temp = new ChapterData();
            temp.Scene = chapter.dataArray[i].Scene;
            temp.Character1 = chapter.dataArray[i].Character1;
            temp.Character2 = chapter.dataArray[i].Character2;
            temp.Highlight = chapter.dataArray[i].Highlight;
            temp.Dialogue = chapter.dataArray[i].Dialogue;
            data.chapterData.Add(temp);
        }
    }

}
