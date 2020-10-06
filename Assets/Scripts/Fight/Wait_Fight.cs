using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Wait_Fight : MonoBehaviourPun
{
    public static bool orange;
    public float timing;
    bool  muu;
    Text wait;
    public GameObject[] waitt;
    float QQDD,load;

    public GameObject Stop;
    private void Start()
    {
        wait = GetComponent<Text>();        
        orange = true;
        load = timing;
    }
    void Update()
    {
        if (muu)
        {
            timing -= 1 * Time.deltaTime;
            wait.text = ((int)timing).ToString();
        }
        if (orange)
        {
            QQDD += 1 * Time.deltaTime;
            //NetwordLauncher.random = false;                      
        }
        if (QQDD >= 1)
        {
            muu = true;
            orange = false;
        }
        if (timing <= 0 && PhotonNetwork.PlayerList.Length >= 4)
        {
            muu = false;
            for (int i = 0; i < waitt.Length; i++)
            {
                waitt[i].SetActive(false);
            }
            Stop.SetActive(false);
            timing = load;
        }
        if (PhotonNetwork.PlayerList.Length < 4)
        {
            for (int i = 0; i < waitt.Length; i++)
            {
                waitt[i].SetActive(false);
            }            
            Stop.SetActive(false);
        }
    }
}
