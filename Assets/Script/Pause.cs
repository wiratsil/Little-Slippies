using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Pause : MonoBehaviour
{

    public Text spText;

    public Text conText;
    
    public Character character;

    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.FindObjectOfType<Character>();
        spText.text = (character.m_BaseSpeed / 100).ToString();
        conText.text = (character.m_BaseControl / 100).ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSp(bool bo)
    {
        if (bo && character.m_BaseSpeed < 1000)
        {
            character.m_BaseSpeed += 50;
        }
        if (!bo && character.m_BaseSpeed > 0)
        {
            character.m_BaseSpeed -= 50;
        }

        spText.text = (character.m_BaseSpeed / 100).ToString();
    }

    public void ChangeCon(bool bo)
    {
        if (bo && character.m_BaseControl < 1000)
        {
            character.m_BaseControl += 50;
        }
        if (!bo && character.m_BaseControl > 0)
        {
            character.m_BaseControl -= 50;
        }

        conText.text = (character.m_BaseControl / 100).ToString();
    }

    public void Exit()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void ExitToMain()
    {
        SceneManager.LoadScene("Main");
    }
    public void z_LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
