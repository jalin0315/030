using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Write : MonoBehaviourPunCallbacks
{
    public static bool isOpen;    //連線開關

    private void Update()
    {
        isOpen = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (isOpen)
        //{
        //    if (other.gameObject.CompareTag("彈珠")/* && other.GetComponent<Marbles>().分數判定*/)
        //    {
        //        if (transform.parent.parent.GetComponent<Score>().score_ >= 0)
        //        {
        //            NameAndScore.Score_ = NameAndScore.Score_ + transform.parent.parent.GetComponent<Score>().score_;
        //            Debug.LogWarning("分數+" + Mathf.Abs(transform.parent.parent.GetComponent<Score>().score_));
        //        }
        //        else
        //        {
        //            NameAndScore.score_ = NameAndScore.score_ + transform.parent.parent.GetComponent<Score>().score_;
        //            Debug.LogWarning("分數-" + Mathf.Abs(transform.parent.parent.GetComponent<Score>().score_));
        //        }
        //    }
        //}
    }
}
