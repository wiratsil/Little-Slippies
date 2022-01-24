using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CooldownButton : MonoBehaviour// EventTrigger
{
    public Button button;
    public Image fill;
    public float duration;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    public virtual void Z_StartCooldown()
    {
        button.interactable = false;
        fill.fillAmount = 0;
        StartCoroutine(Cooldown());
    }
    public virtual void Z_StartCooldown(Image fi)
    {

    }

    public virtual void FinishCooldown()
    {
        button.interactable = true;

    }

    IEnumerator Cooldown()
    {
        float timer = 0;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            fill.fillAmount = Mathf.Lerp(0, 1, timer / duration);
            yield return null;

        }
        FinishCooldown();
    }
}
