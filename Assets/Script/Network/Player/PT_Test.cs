using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class PT_Test : MonoBehaviour
{
    public bool ready;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PhotonView photonView = gameObject.GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            //Move Front/Back
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(transform.up * Time.deltaTime * 2.45f, Space.World);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(-transform.up * Time.deltaTime * 2.45f, Space.World);
            }

            //Rotate Left/Right
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(-transform.right * Time.deltaTime * 2.45f, Space.World);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(transform.right * Time.deltaTime * 2.45f, Space.World);
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
              //  PhotonNetwork.Instantiate("Bomb", new Vector3(Random.Range(-3, 3), 0, 0), Quaternion.identity, 0);
                photonView.RPC("TestRpc", RpcTarget.All);
            }
        }

    }


    [PunRPC]
    public IEnumerator TestRpc()
    {
        ready = !ready;
        //PhotonNetwork.Instantiate("Bomb", new Vector3(Random.Range(-3, 3), 0, 0), Quaternion.identity, 0);
        // transform.localScale = new Vector3(transform.localScale.x + 1, transform.localScale.y + 1, transform.localScale.z);
        yield return 0; // if you allow 1 frame to pass, the object's OnDestroy() method gets called and cleans up references.
    }

}
