using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class DrawLots : MonoBehaviourPunCallbacks, IPunObservable
{
    public List<GameObject> map = new List<GameObject>();
    public GameObject slot;
    public float QQ;
    public static int i;
    public bool isOpen,isRandom;
    public GameObject goGo;
    Hashtable maP = new Hashtable();
    void Start()
    {
        isOpen = true;
        QQ = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen)
        {
            QQ -= 1 * Time.deltaTime;
            if (QQ > 0)
            {
                for (int i = 0; i < map.Count; i++)
                {
                    map[i].transform.Rotate(-i + 2 * QQ, i + 2 * QQ, -i + 2 * QQ);
                }
            }
        }


        if (QQ < 0 && !isRandom) 
        {
            isOpen = false;
            if (PhotonNetwork.IsMasterClient)
            {
                i = Random.Range(0, map.Count);
                maP.Add("Random", i);
                PhotonNetwork.LocalPlayer.SetCustomProperties(maP, null);
                isRandom = true;
                QQ = 5;
            }
            else if (!PhotonNetwork.IsMasterClient) 
            {
                foreach (Player player in PhotonNetwork.PlayerList)
                {
                    if (player.IsMasterClient)  
                    {
                        i = (int)player.CustomProperties["Random"];
                        isRandom = true;
                        QQ = 5;
                    }
                }
            }
        }
        if (isRandom)
        {
            map[i].transform.rotation = new Quaternion(0, 0, 0, 0);
            map[i].transform.SetParent(slot.transform);
            map[i].transform.position = Vector3.MoveTowards(map[i].transform.position, slot.transform.position, 10);
            if (map[i].transform.position == slot.transform.position)
            {
                goGo.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
    [PunRPC]
    public void Map()
    {
        
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
}
