using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public Image bg;
    public Sprite[] bgList;
    public Image bar;
    // Start is called before the first frame update
    void Start()
    {
        bg.sprite = bgList[Random.Range(0, bgList.Length)];
        StartCoroutine(LoadYourAsyncScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadYourAsyncScene()
    {
        yield return null;

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(PlayerPrefs.GetString("Loading"));
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        Debug.Log("Pro :" + asyncOperation.progress);
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            //Output the current progress
            bar.fillAmount = asyncOperation.progress;
            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                //Change the Text to show the Scene is ready
                //Wait to you press the space key to activate the Scene
                yield return null;
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
