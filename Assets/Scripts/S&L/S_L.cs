﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class S_L : MonoBehaviourPunCallbacks
{
    public static S_L instance;
    public GameObject start;
    public static int Check, Judgment;    //judgment判斷
    public int check,judgment;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        //start = GameObject.Find("Start").gameObject;

        if (Check != 0)
        {
            GetComponent<L_>().善良();
        }
        if (start != null && Check != 0)
        {
            start.SetActive(false);            
        }
    }
    private void Update()
    {
        check = Check;
        judgment = Judgment;        
    }
}
