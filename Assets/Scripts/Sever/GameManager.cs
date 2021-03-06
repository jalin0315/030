﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public Marbles[] ball;
    public Transform[] position;
    public int ID, gps;

    public string u;

    public static List<GameObject> marbles = new List<GameObject>();
    public List<GameObject> mrbs = new List<GameObject>();

    GameObject ms;
    public MovementSystem mv;
    private void Awake()
    {
        ID = GameObject.Find("S&L").GetComponent<L_>().loadShowAndFight;
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if (PhotonNetwork.PlayerList[i] == PhotonNetwork.LocalPlayer)
            {
                gps = i;
            }
        }
        u = "00" + ID;
        GameObject mars = PhotonNetwork.Instantiate(u, position[gps].position, Quaternion.identity, 0);
        mars.AddComponent<MovementSystem>();
        mars.GetComponent<MovementSystem>().Text = mv.Text;
        mars.GetComponent<MovementSystem>()._Rigidbody = mars.GetComponent<Rigidbody>();
        CameraSystem._LocalPlayer = mars.transform;
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
    private void Start()
    {
        //if (ID != 0)
        //{
        //    foreach (var item in ball)
        //    {
        //        if (item.ID == ID)
        //        {
        //            foreach (var player in PhotonNetwork.PlayerList)
        //            {
        //                if (player.NickName == PhotonNetwork.LocalPlayer.NickName)
        //                {
        //                    u = "00" + item.ID;
        //                    GameObject marbles = PhotonNetwork.Instantiate(u, position[gps].position, Quaternion.identity, 0);
        //                    marbles.name = player.NickName;
        //                    PlayerManager.Marbles.Add(marbles.GetComponent<Marbles>());
        //                }
        //                else
        //                {
        //                    u = "00" + item.ID;
        //                    GameObject marbles = PhotonNetwork.Instantiate(u, position[gps].position, Quaternion.identity, 0);
        //                    marbles.name = player.NickName;
        //                    PlayerManager.Marbles.Add(marbles.GetComponent<Marbles>());
        //                }
        //            }
        //        }
        //    }
        //}
        
        

        //foreach (var player in PhotonNetwork.PlayerList)
        //{
        //    if (player.NickName == PhotonNetwork.LocalPlayer.NickName)
        //    {
        //        u = "00" + ID;
        //        GameObject marbles = PhotonNetwork.Instantiate(u, position[gps].position, Quaternion.identity, 0);
        //        marbles.name = player.NickName;
        //        PlayerManager.Marbles.Add(marbles.GetComponent<Marbles>());
        //    }
        //    else
        //    {
        //        u = "00" + ID;
        //        GameObject marbles = PhotonNetwork.Instantiate(u, position[gps].position, Quaternion.identity, 0);
        //        marbles.name = player.NickName;
        //        PlayerManager.Marbles.Add(marbles.GetComponent<Marbles>());
        //    }
        //}
    }
    private void Update()
    {
        mrbs = marbles;        
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        
    }
    public void OnApplicationPause(bool pause)
    {
        
    }
}
   

    
