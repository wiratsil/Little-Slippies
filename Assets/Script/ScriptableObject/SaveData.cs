using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;



[CreateAssetMenu(fileName = "SaveData", menuName = "ScriptableObjects/Save Data", order = 1)]
public class SaveData : ScriptableObject
{
    [System.Serializable]
    public class SaveChapter
    {
        public int chapterNum;
        public float time;
        public int star;
    }
    public SaveChapter[] saveChapter;

    [System.Serializable]
    public class SoundData
    {
        public bool BGM;
        public bool SFX;
    }
    public SoundData soundData;

    void OnEnable()
    {


    }


}
