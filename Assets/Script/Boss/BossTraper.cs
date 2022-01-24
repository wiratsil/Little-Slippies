using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTraper : MonoBehaviour
{
    public BossBullet bossBullet;
    public List<Transform> point;
    public List<GameObject> cloneList;
    // Start is called before the first frame update
    void Start()
    {
        point = new List<Transform>();
        cloneList = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
            point.Add(transform.GetChild(i));

        for (int i = 0; i < point.Count; i++)
        {
            BossBullet clone = Instantiate(bossBullet, point[i].position, point[i].rotation) ;
            clone.gameObject.SetActive(false);
            cloneList.Add(clone.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < cloneList.Count; i++)
        {
            cloneList[i].SetActive(true);
        }
        gameObject.SetActive(false);
    }
}
