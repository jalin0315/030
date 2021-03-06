﻿using System.Collections;
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

    public static List<GameObject> Gps = new List<GameObject>();
    public List<GameObject> gps = new List<GameObject>();

    //public List<GameObject> rank = new List<GameObject>();
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
            pl.name = p.NickName;
            Money.Gambing_.Add(pl);
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

            RankManager._RankText.Add(pl);

            //rank.Add(pl);
            //Marbles.Rank.Add(pl);
        }
    }
    private void Update()
    {
        gps = Gps;
    }
}