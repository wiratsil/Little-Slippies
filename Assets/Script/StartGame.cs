using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public CanvasGroup canvas;
    public Text text;
    public Button play;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        if (PlayerPrefs.HasKey("PScene") && PlayerPrefs.GetInt("PScene") != 0)
        {
            Z_Start();
            play.onClick.Invoke();
            PlayerPrefs.SetInt("PScene", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        text.color = Color.Lerp(Color.black, Color.white, Mathf.PingPong(Time.time, 1));
    }

    public void Z_Start()
    {
        canvas.alpha = 0;
        canvas.blocksRaycasts = false;
    }

}
