using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main_Single : MonoBehaviour
{
    public GameObject[] chapter;
    public GameObject[] canvas;
    public GameObject main;

    public Toggle BGM;
    public Toggle SFX;
    // Start is called before the first frame update
    void Start()
    {

        StartSound();

        Application.targetFrameRate = 300;
        //if (!PlayerPrefs.HasKey("ChapterScore0" ))
        //    PlayerPrefs.SetInt("ChapterScore0", 0);

        UpdateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void z_LoadScene(string scene)
    {
        PlayerPrefs.SetString("Loading", scene);
        SceneManager.LoadScene("Loading");
    }

    public void UpdateLevel()
    {
        for (int i = 0; i < chapter.Length; i++)
        {
            chapter[i].GetComponent<Button>().interactable = true;

            if (SaveSystem.Instance.saveData.saveChapter[i].star > 0)
            {
                for (int h = 0; h < SaveSystem.Instance.saveData.saveChapter[i].star; h++)
                {
                    chapter[i].transform.GetChild(h).GetChild(0).GetComponent<Image>().enabled = true;

                }
            }
            else
            {
                chapter[i].GetComponent<Button>().interactable = true;
                break;
            }
            //if (PlayerPrefs.HasKey("ChapterScore" + i))
            //{
            //    Debug.Log("Has key : ChapterScore "+ i +" = "+ PlayerPrefs.GetInt("ChapterScore" + i));

            //    chapter[i].GetComponent<Button>().interactable = true;

            //    if (PlayerPrefs.GetInt("ChapterScore" + i) == 0)
            //        continue;

            //    for (int h = 0; h < PlayerPrefs.GetInt("ChapterScore" + i); h++)
            //    {
            //        chapter[i].transform.GetChild(h).GetChild(0).GetComponent<Image>().enabled = true;

            //    }
            //}
            //else
            //{
            //    chapter[i].GetComponent<Button>().interactable = true;
            //    break;
            //}
        }
    }

    public void Selection(GameObject obj)
    {
        main.SetActive(false);
        obj.SetActive(true);
    }

    public void Back()
    {
        for (int i = 0; i < canvas.Length; i++)
        {
            canvas[i].SetActive(false);
            main.SetActive(true);
        }
    }
    public void SetSoundBGM(bool bo)
    {
        if (!BGM.interactable)
            return;
        SaveSystem.Instance.saveData.soundData.BGM = !SaveSystem.Instance.saveData.soundData.BGM;
        SoundManager.Instance.audioBGM.mute = !SaveSystem.Instance.saveData.soundData.BGM;
    }

    public void SetSoundSFX(bool bo)
    {
        if (!SFX.interactable)
            return;
        SaveSystem.Instance.saveData.soundData.SFX = !SaveSystem.Instance.saveData.soundData.SFX;
        SoundManager.Instance.audioSFX.mute = !SaveSystem.Instance.saveData.soundData.SFX;
    }

    public void StartSound()
    {
        BGM.isOn = SaveSystem.Instance.saveData.soundData.BGM;
        SoundManager.Instance.audioBGM.mute = !SaveSystem.Instance.saveData.soundData.BGM;
        BGM.interactable = true;

        SFX.isOn = SaveSystem.Instance.saveData.soundData.SFX;
        SoundManager.Instance.audioSFX.mute = !SaveSystem.Instance.saveData.soundData.SFX;
        SFX.interactable = true;

    }
}
