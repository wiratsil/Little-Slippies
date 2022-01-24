using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoundEvent : UnityEvent<AudioClip>
{
    public static SoundEvent play = new SoundEvent();
    public static SoundEvent playBGM = new SoundEvent();
}
public class SoundManager : MonoBehaviour
{
    public AudioSource audioSFX;
    public AudioSource audioBGM;


    public static SoundManager Instance { get { return _instance; } }
    private static SoundManager _instance;

    // Start is called before the first frame update
    void Awake()
    {
        SoundEvent.play.AddListener(PlaySound);
        SoundEvent.playBGM.AddListener(PlayBackgroundSound);

        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;//Avoid doing anything else
        }
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(AudioClip audioClip)
    {
        audioSFX.PlayOneShot(audioClip);
    }

    public void PlayBackgroundSound(AudioClip audioClip)
    {
        audioBGM.clip = audioClip;
        audioBGM.Play();
    }
}
