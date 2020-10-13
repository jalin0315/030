using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ExitRoom : MonoBehaviourPunCallbacks,IPunObservable
{
    public static ExitRoom _Instance;
    public GameObject WaitPanel,player;
    public NetwordLauncher net;


    private void Awake()
    {
        _Instance = this;
        /*if (PhotonNetwork.PlayerList.Length > 1)
        {
            gameObject.transform.parent.gameObject.SetActive(false);
        }*/
    }

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
    public void GameLeave()
    {
        RankManager.isOpen = true;
        RankManager._RankText.Clear();
        Money.Gambing_.Clear();        
        GameManager.marbles.Clear();
        PlayerManager.Gps.Clear();
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        if (index != 0)
        {
            SceneManager.LoadScene(0);            
        }
        base.OnLeftRoom();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}
