using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Rank : MonoBehaviourPun
{
    public List<GameObject> marbles = new List<GameObject>();
    int i;
    void Start()
    {
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (i > PhotonNetwork.PlayerList.Length - 1)
        {
            i = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("彈珠"))
        {
            other.GetComponent<Marbles>().rankint = i + 1;
            i += 1;
        }
    }
}
