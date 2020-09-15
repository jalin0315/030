using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Pun.UtilityScripts;


public class NameAndScore : MonoBehaviourPunCallbacks,IPunObservable
{
    public Text p1, p2;

    public static int score_, Score_, SScore_, P1Win, P2Win;
    public Text S1, s1, SS1, S2, s2, SS2, LookWin1, LookWin2;

    Hashtable Sc = new Hashtable();

    public GameObject nameAndScore,wait;

    void Awake()
    {
        PhotonNetwork.LocalPlayer.AddScore(SScore_);

        if (!Sc.ContainsKey("score_"))
        {
            Sc.Add("score_", score_);
        }
        if (!Sc.ContainsKey("Score_"))
        {
            Sc.Add("Score_", score_);
        }
        PhotonNetwork.LocalPlayer.SetCustomProperties(Sc, null);
    }
    private void Start()
    {
        
    }
    void Update()
    {
        SScore_ = score_ + Score_;
        s1.text = score_.ToString();
        S1.text = Score_.ToString();
        SS1.text = SScore_.ToString();
        LookWin1.text = P1Win.ToString();
        LookWin2.text = P2Win.ToString();

        if (S_L.Judgment != 1 && !wait.activeSelf)
        {
            nameAndScore.SetActive(true);             
        }

        if (Sc["score_"].ToString() != s1.text)
        {
            Sc.Remove("score_");
            PhotonNetwork.LocalPlayer.SetCustomProperties(Sc, null);
            Sc.Add("score_", score_);
        }
        if (Sc["Score_"].ToString() != S1.text)
        {
            Sc.Remove("Score_");
            PhotonNetwork.LocalPlayer.SetCustomProperties(Sc, null);
            Sc.Add("Score_", Score_);
        }
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (p1.text == otherPlayer.NickName)
        {
            p1.text = null;
        }
        if (p2.text == otherPlayer.NickName)
        {
            p2.text = null;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //if (stream.IsWriting)
        //{
        //    Debug.LogError("01");
        //    stream.SendNext(p2s);
        //    stream.SendNext(p2S);
        //    stream.SendNext(p2SS);
        //}
        //else
        //{
        //    Debug.LogError("02");
        //    p2s = (int)stream.ReceiveNext();
        //    p2S = (int)stream.ReceiveNext();
        //    p2SS = (int)stream.ReceiveNext();
        //}
    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (PhotonNetwork.CountOfPlayersInRooms < 2)
        {
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                if (player.NickName == PhotonNetwork.NickName)
                {
                    p1.text = PhotonNetwork.NickName;
                }
                else
                {
                    p2.text = player.NickName;
                }
            }
        }

        

        if (PhotonNetwork.CountOfPlayersInRooms < 2)
        {
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                if (player.NickName == PhotonNetwork.NickName)
                {
                    s1.text = score_.ToString();
                    S1.text = Score_.ToString();
                    SS1.text = SScore_.ToString();

                    if (SS1.text != player.GetScore().ToString())
                    {
                        player.SetScore(SScore_);
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.F6))
                    {
                        Debug.LogError(player.GetScore().ToString());
                    }
                    if (player.CustomProperties.ContainsKey("score_"))
                    {
                        s2.text = player.CustomProperties["score_"].ToString();
                    }
                    if (player.CustomProperties.ContainsKey("Score_"))
                    {
                        S2.text = player.CustomProperties["Score_"].ToString();
                    }

                    SS2.text = player.GetScore().ToString();
                }
            }
        }
    }
    public void Clear()
    {
        P1Win = 0;
        P2Win = 0;
    }
}
