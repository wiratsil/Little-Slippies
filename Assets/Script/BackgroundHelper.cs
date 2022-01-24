using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

public class BackgroundHelper : MonoBehaviour
{
    public float speed = 1;
    float pos = 0;
    public bool repeat;
    [SerializeField]
    public Character character;

    [SerializeField]
    private RawImage rawImage;
    [SerializeField]
    private RectTransform rect;

    [SerializeField]
    private Image[] imageList;
    private int index;

    // Start is called before the first frame update
    void Start()
    {

        rawImage = GetComponent<RawImage>();
        rect = GetComponent<RectTransform>();

        if (repeat)
            rawImage.texture.wrapMode = TextureWrapMode.Repeat;
        else
            rawImage.texture.wrapMode = TextureWrapMode.Clamp;

        //rawImage.texture = imageList[index].mainTexture;
        pos = rawImage.uvRect.x;
    }


    private void OnEnable()
    {

    }

    private void Update()
    {
        if (character == null)
        {
            pos += Time.deltaTime * speed / 1000 ;
            if (pos >= 1.0F)
                pos = -1.0F;

            rawImage.uvRect = new Rect(pos, 0, 1, 1);
            return;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (character == null)
            return;
            // pos += (player.CurrentSpeed / 1000 * Time.deltaTime) / speed ;
            pos += character.rigid.velocity.x / speed / 1000; 
        if (pos > 1.0F)
            pos = -1.0F;
        
        rawImage.uvRect = new Rect(pos, 0, 1, 1);
    }
}
