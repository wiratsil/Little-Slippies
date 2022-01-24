using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;

[CreateAssetMenu(fileName = "Chapter", menuName = "ScriptableObjects/Chapter Data", order = 1)]
public class Chapter : ScriptableObject
{
    [Space]
    //[ReadOnly]
    public string sheetPath = "Assets/Excel/Little Slippies Story.xlsx";

    public List<ChapterData> dataArray;

    void OnEnable()
    {


    }
    
   
}
