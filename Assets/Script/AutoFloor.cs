using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
public class AutoFloor : MonoBehaviour
{
    public Character character;
    public float lenghtRespawn;
    public float lenghtFloor;
    // Start is called before the first frame update
    void Start()
    {

    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((character.transform.position.x - transform.position.x) > lenghtRespawn)
        {
            transform.position = new Vector3(character.transform.position.x + lenghtFloor, transform.position.y, 0);
        }
    }
}
