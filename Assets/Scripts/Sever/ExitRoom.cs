using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ExitRoom : MonoBehaviourPunCallbacks,IPunObservable
{
    public GameObject WaitPanel,player;
    public NetwordLauncher net;
    public void GoBackOnLine()
    {
        PhotonNetwork.LeaveRoom();
        WaitPanel.SetActive(false);
        net.mapCheck = 0;
        net.create = false;
        net.random = false;
        net.slot.enabled = false;
        net.StarT.gameObject.SetActive(false);
        net.check3 = true;
        foreach (var pl in PhotonNetwork.PlayerList)
        {
            if (pl.NickName == PhotonNetwork.NickName) 
            {
                for (int i = 0; i < player.transform.childCount; i++)
                {
                    Destroy(player.transform.GetChild(i).gameObject);
                }
            }
        }
    }
    public override void OnLeftRoom()
    {        
        base.OnLeftRoom();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}
