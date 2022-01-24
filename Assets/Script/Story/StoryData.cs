using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class StoryData : MonoBehaviour
{
    public BackgroundList backgroundList;
    [System.Serializable]
    public class BackgroundList
    {
        public Sprite[] scene;
    }
    [Space]
    public CharacterList characterList;
    [System.Serializable]
    public class CharacterList
    {
        public Sprite[] character;
    }
    [Space]
    public List<ChapterData> chapterData = new List<ChapterData>();
    [Space]
    public float durationTime = 0.05f;
    public bool nextPart;


    public Sprite GetScene(int index)
    {
        if (index == 0)
            return null;

        Sprite sprite = null;
        sprite = backgroundList.scene[index - 1];
        return sprite;
    }

    public Sprite GetCharacter(int index)
    {
        if (index == 0)
            return null;

        Sprite sprite = null;
        sprite = characterList.character[index - 1];
        return sprite;
    }
}
