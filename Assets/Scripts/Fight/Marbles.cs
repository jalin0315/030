using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Marbles : MonoBehaviourPunCallbacks
{
    public Vector3 vv;
    int i;
    public int ID,rankint,lap,bet;
    Rigidbody ry;

    //public static List<GameObject> Rank = new List<GameObject>();
    //public List<GameObject> rank = new List<GameObject>();

    private void Start()
    {
        GameManager.marbles.Add(gameObject);
        ry = GetComponent<Rigidbody>();
    }
    private void Awake()
    {
        vv = transform.position;
    }
    private void Update()
    {
        //rank = Rank;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(transform.right * -400, ForceMode.Impulse);
        }
        //if (rankint != 0)
        //{
        //    foreach (var r in Rank)
        //    {
        //        if (r.name == photonView.Owner.NickName) 
        //        {
        //            r.transform.SetParent(PlayerManager.Gps[rankint - 1].transform);轉彎數量
        //            r.transform.position = PlayerManager.Gps[rankint - 1].transform.position;
        //        }
        //    }
        //}
        gameObject.name = photonView.Owner.NickName;
        if (lap == RankManager._NumberOfTurns + 1 && !Rank.Marbles.Contains(gameObject.GetComponent<Marbles>()) && PhotonNetwork.PlayerList.Length > 1)  
        {
            if (!Rank.isStart)
            {
                Rank.isStart = true;
            }
            Rank.Marbles.Add(gameObject.GetComponent<Marbles>());
            ry.velocity = Vector3.zero;
            gameObject.GetComponent<ConstantForce>().force = new Vector3(0, 10, 0);
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
