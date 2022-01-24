using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBGM : MonoBehaviour
{
    public AudioClip audioClip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        SoundEvent.playBGM.Invoke(audioClip);
    }
}
