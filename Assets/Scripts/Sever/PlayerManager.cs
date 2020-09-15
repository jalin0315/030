using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    public GameObject playerNamePrefab;
    public Transform gridLayout;

    public GameObject playerRankPrefab;

    public GameObject playerRankGps, gpsParent;

    public List<GameObject> Gps = new List<GameObject>();

    public static List<Marbles> Marbles = new List<Marbles>();
    public List<Marbles> marbles = new List<Marbles>();

    public List<GameObject> rank = new List<GameObject>();
    private void Awake()
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)    //定位版
        {
            GameObject gps = Instantiate(playerRankGps, gpsParent.transform);
            gps.name = "00" + (i + 1);
            Gps.Add(gps);
        }
    }
    public void Start()
    {
        foreach (Player p in PhotonNetwork.PlayerList)    //賭注版
        {
            GameObject pl = Instantiate(playerNamePrefab, gridLayout.position, Quaternion.identity);
            pl.transform.GetChild(0).GetComponent<Text>().text = p.NickName;
            pl.transform.SetParent(gridLayout); 
            pl.transform.localScale = new Vector3(1, 1, 1);
            // Add a button for each player in the room.
            // You can use p.NickName to access the player's nick name.
        }

        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)    //資訊版
        {
            GameObject pl = Instantiate(playerRankPrefab, Gps[i].transform.position, Quaternion.identity);                  
            pl.transform.GetChild(0).GetComponent<Text>().text = PhotonNetwork.PlayerList[i].NickName;
            pl.name = PhotonNetwork.PlayerList[i].NickName;
            pl.transform.SetParent(Gps[i].transform);
            pl.transform.localScale = new Vector3(1, 1, 1);
            rank.Add(pl);
        }
    }
    private void Update()
    {
        marbles = Marbles;
        
        for (int i = 0; i < Marbles.Count; i++)
        {
            if (Marbles[i].rank != 0 && Marbles[i].name == rank[i].name) 
            {
                rank[i].transform.SetParent(Gps[Marbles[i].rank - 1].transform);
                rank[i].transform.position = Gps[Marbles[i].rank - 1].transform.position;
            }
        }
    }
}