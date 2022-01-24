using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossAnimEvent : UnityEvent<string>
{
    public static BossAnimEvent animEvent = new BossAnimEvent();
}

public class BossPointer : MonoBehaviour
{
    public string bossAimString;
    public Chapter chapter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && bossAimString == "Story")
        {
            StoryEventI.open.Invoke(chapter);

            GameManager_Story.instance.SetGamePhase(PhaseGame.Story);
            return;
        }
        if(collision.tag == "Player")
            BossAnimEvent.animEvent.Invoke(bossAimString);
    }
}
