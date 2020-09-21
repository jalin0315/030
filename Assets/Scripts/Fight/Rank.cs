using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Rank : MonoBehaviourPun
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("彈珠") && other.GetComponent<Marbles>().lap < RankManager._NumberOfTurns)
        {
            other.GetComponent<Marbles>().lap += 1;
        }
        else if (other.gameObject.CompareTag("彈珠") && other.GetComponent<Marbles>().lap == RankManager._NumberOfTurns)
        {
            other.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
