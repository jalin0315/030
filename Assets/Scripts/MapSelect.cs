using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class MapSelect : MonoBehaviourPunCallbacks,IPunObservable
{
    public int MapID,o;
    Hashtable lookMap = new Hashtable();

    public void Update()
    {
        if (NetwordLauncher.mapSelect != 0 && MapID == 0 && PhotonNetwork.IsMasterClient) 
        {
            o = NetwordLauncher.mapSelect - 1;
            transform.GetChild(o).gameObject.SetActive(true);
            if (!lookMap.ContainsKey("map"))
            {
                lookMap.Add("map", o);
                PhotonNetwork.LocalPlayer.SetCustomProperties(lookMap, null);
            }
            else
            {
                lookMap.Remove("map");
                lookMap.Add("map", o);
                PhotonNetwork.LocalPlayer.SetCustomProperties(lookMap, null);
            }
            //for (int i = 0; i < transform.childCount; i++)
            //{
            //    transform.GetChild(i).gameObject.SetActive(false);
            //}
        }

        if (!PhotonNetwork.IsMasterClient)
        {            
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                if (player.IsMasterClient)
                {
                    o = (int)player.CustomProperties["map"];
                }
            }
            transform.GetChild(o).gameObject.SetActive(true); 
        }
    }
    public void ID()
    {
        NetwordLauncher.mapSelect = MapID;
    }
    public void Close()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        photonView.RPC("CloseOtherPlayer", RpcTarget.Others);
    }
    [PunRPC]
    public void CloseOtherPlayer()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}
