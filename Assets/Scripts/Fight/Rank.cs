﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Rank : MonoBehaviourPun
{
    public static Rank _Instance;
    public static List<Marbles> Marbles = new List<Marbles>();
    public List<Marbles> marbles = new List<Marbles>();

    public List<Transform> ball = new List<Transform>();

    public GameObject rank, over,exit,time;
    public Text money,moneyLook;

    public bool isOpen;
    public static bool isStart;
    S_ s;
    L_ l;
    private void Awake()
    {
        _Instance = this;
    }
    private void Start()
    {
        isOpen = true;
        s = GameObject.Find("S&L").GetComponent<S_>();
        l = GameObject.Find("S&L").GetComponent<L_>();
    }
    private void Update()
    {
        if (PhotonNetwork.PlayerList.Length == 1)
        {
            exit.SetActive(true);
            return;
        }
        else
        {
            marbles = Marbles;
            //GameOver();

            if (isStart)
            {
                time.SetActive(true);
            }
        }        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("彈珠") && !ball.Contains(other.transform)) 
        {
            ball.Add(other.transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("彈珠"))
        {
            other.GetComponent<Marbles>().lap += 1;            
        }
    }
    public void GameOver()
    {
        if (Marbles.Count == PhotonNetwork.PlayerList.Length && PhotonNetwork.PlayerList.Length != 0) 
        {
            rank.SetActive(false);
            over.SetActive(true);
            exit.SetActive(true);
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                over.transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<UnityEngine.UI.Text>().text = rank.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Text>().text;
            }            
        }

        if (over.activeSelf && isOpen)
        {
            for (int i = 0; i < Marbles.Count; i++)
            {
                if (i == 0)
                {
                    l.loadPlayerMoney = int.Parse(money.text) + Marbles[i].bet * 5 ;
                    if (Marbles[i].name == PhotonNetwork.LocalPlayer.NickName)
                    {
                        l.loadPlayerMoney = l.loadPlayerMoney + 25;
                    }
                }
                else if (i == 1)
                {
                    l.loadPlayerMoney = l.loadPlayerMoney + Marbles[i].bet * 4;
                    if (Marbles[i].name == PhotonNetwork.LocalPlayer.NickName)
                    {
                        l.loadPlayerMoney = l.loadPlayerMoney + 15;
                    }
                }
                else if (i == 2)
                {
                    l.loadPlayerMoney = l.loadPlayerMoney + Marbles[i].bet * 3;
                    if (Marbles[i].name == PhotonNetwork.LocalPlayer.NickName)
                    {
                        l.loadPlayerMoney = l.loadPlayerMoney + 10;
                    }
                }
                else
                {
                    l.loadPlayerMoney = l.loadPlayerMoney + Marbles[i].bet * 0;
                }
            }
            moneyLook.text = l.loadPlayerMoney.ToString();
            s.善良();
            for (int i = 0; i < Marbles.Count; i++)
            {
                Marbles[i].lap = 0;
                Marbles[i].GetComponent<TrackPoint>().enabled = false;
            }
            Marbles.Clear();            
            isOpen = false;
        }
    }
}
