using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Marbles : MonoBehaviourPunCallbacks
{
    public Vector3 vv;
    int i;
    public int ID,rankint;
    Rigidbody ry;

    public static List<GameObject> Rank = new List<GameObject>();
    public List<GameObject> rank = new List<GameObject>();
    private void Awake()
    {
        vv = transform.position;
        ry = GetComponent<Rigidbody>();


    }
    private void Update()
    {
        rank = Rank;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(transform.right * -400, ForceMode.Impulse);
        }
        if (rankint != 0)
        {
            foreach (var r in Rank)
            {
                if (r.name == photonView.Owner.NickName) 
                {
                    r.transform.SetParent(PlayerManager.Gps[rankint - 1].transform);
                    r.transform.position = PlayerManager.Gps[rankint - 1].transform.position;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            i += 1;
            transform.position = vv;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Debug.Log(i);
        }
        if (other.gameObject.CompareTag("Over"))
        {
            Instantiate(gameObject, new Vector3(vv.x, vv.y, vv.z), transform.rotation);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GG"))
        {
            transform.position = new Vector3(vv.x, vv.y + 10, vv.z);
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<ConstantForce>().enabled = false ;
        }

        if (collision.gameObject.CompareTag("Finish"))    //第一個到 計算排名
        {
            i += 1;
            transform.position = vv;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Debug.Log(i);
        }
    }
}
