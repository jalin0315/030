using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OpenTimeClose : MonoBehaviourPun
{
    public float timing, time;
    public bool isOpen;
    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOpen)
        {
            time += 1 * Time.deltaTime;
            if (time >= timing)
            {
                time = 0;
                gameObject.SetActive(false);
            }
        }
        if (isOpen)
        {
            time += 1 * Time.deltaTime;
            if (time >= timing)
            {
                if (!PhotonNetwork.IsMasterClient)
                    return;
                time = 0;
                PhotonNetwork.LoadLevel(DrawLots.i + 1);
                gameObject.SetActive(false);
            }
        }
    }
}