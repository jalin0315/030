using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Check : MonoBehaviourPun
{
    public GameObject[] check;
    void Start()
    {
        if (PhotonNetwork.IsConnected) 
        {
            for (int i = 0; i < check.Length; i++)
            {
                check[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
