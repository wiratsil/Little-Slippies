using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SaveEvent : UnityEvent<bool>
{
    public static SaveEvent SetSoundSFX = new SaveEvent();
    public static SaveEvent SetSoundBGM = new SaveEvent();
}

public class SaveSystem : MonoBehaviour
{
    public SaveData saveData;

    public static SaveSystem Instance { get { return _instance; } }
    private static SaveSystem _instance;
    // Start is called before the first frame update
    void Awake ()
    {
        SaveEvent.SetSoundSFX.AddListener(SaveSoundSFX);
        SaveEvent.SetSoundBGM.AddListener(SaveSoundBGM);

        if (_instance != null && _instance != this)
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

    public void SaveSoundSFX(bool bo)
    {
        saveData.soundData.SFX = bo;
    }
    public void SaveSoundBGM(bool bo)
    {
        saveData.soundData.BGM = bo;
    }
}
