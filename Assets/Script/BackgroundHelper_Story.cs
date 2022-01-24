using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundHelper_Story : BackgroundHelper
{
    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.FindObjectOfType<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
