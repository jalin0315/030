using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Pun;


public class L_ : MonoBehaviourPun
{
    //宣告一個字串讀取檔案，宣告一個Data物件(上一篇定義的可用來存放遊戲資訊)來取得字串轉換後的物件
    public static string username;

    string LoadData;
    MarblesEquipment marbles;

    public string loadPlayerName;
    public int loadPlayerMoney;
    public List<int> loadMarblesID = new List<int>();
    public int loadShowAndFight;

    public GameObject nameIn,start,money;
    public NetwordLauncher playername;
    //string fileName = "gamesave.dat";
    public void 善良()
    {

        string fileName = username;

        //讀取指定路徑的Json檔案並轉成字串(路徑同上一篇)

        if (File.Exists(Application.persistentDataPath + "/" + fileName))
        {
            LoadData = File.ReadAllText(Application.persistentDataPath + "/" + fileName);
            //把字串轉換成Data物件
            marbles = JsonUtility.FromJson<MarblesEquipment>(LoadData);
            loadPlayerName = marbles.playerName;
            loadPlayerMoney = marbles.playerMoney;
            loadMarblesID = marbles.marblesID;
            loadShowAndFight = marbles.showAndFight;
            start.SetActive(false);
            playername.nameText.text = loadPlayerName;
            PhotonNetwork.NickName = loadPlayerName;
            money.GetComponent<UnityEngine.UI.Text>().text = loadPlayerMoney.ToString();
            Debug.LogWarning("讀取完成,你好  " + loadPlayerName);
        }
        else
        {
            nameIn.SetActive(true);
            loadPlayerMoney = 100;
            loadShowAndFight = 1;
            money.GetComponent<UnityEngine.UI.Text>().text = loadPlayerMoney.ToString();
            loadMarblesID.Add(1);
        }

    }
}