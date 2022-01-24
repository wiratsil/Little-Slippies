using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StoryTrigger : UnityEvent<StoryData>
{
    public static StoryTrigger trigger = new StoryTrigger();
} 

public class Storytelling : MonoBehaviour
{
    public StoryData storyData;

    public Image imageRef;
    public Text textRef;

    private void Start()
    {
        //StoryTrigger.trigger.AddListener(TriggerPoint);
    }

    public void TriggerPoint(StoryPoint storyPoint)
    {


    }

    IEnumerator ShowStory(float time, StoryPoint storyPoint)
    {
        yield return new WaitForSeconds(time);
    }
}
