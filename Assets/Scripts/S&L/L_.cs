using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Photon.Pun;
using UnityEngine.SceneManagement;



public class L_ : MonoBehaviourPun
{
    //宣告一個字串讀取檔案，宣告一個Data物件(上一篇定義的可用來存放遊戲資訊)來取得字串轉換後的物件
    public static string username,轉嫁;

    public static MarblesEquipment marbles;

    public string loadPlayerName;
    public int loadPlayerMoney;
    public List<int> loadMarblesID = new List<int>();
    public int loadShowAndFight;

    public GameObject nameIn,start;
    public Text money;
    public NetwordLauncher playername;

    public static string 帳號;
    public string fileName,oaoa;
    //string fileName = "gamesave.dat";

    public static int check;
    static L_ instance;
    public void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        //else if (instance != this)
        //{
        //    enabled = false;
        //}

        if (S_L.isOpen)
        {
            善良();
        }
    }
    public void n善良()
    {
        Debug.Log("n善良");
        nameIn.SetActive(true);
        loadPlayerMoney = 100;
        loadShowAndFight = 1;
        money.text = loadPlayerMoney.ToString();
        loadMarblesID.Add(1);
    }
    public void 善良()
    {
        Debug.Log("善良");
        StartCoroutine(Main.Instance.Web.Load(帳號, fileName));        
    }
    public void Load()
    {
        if (check != 0 && instance != this)
        {
            loadPlayerName = marbles.playerName;
            loadPlayerMoney = marbles.playerMoney;
            loadMarblesID = marbles.marblesID;
            loadShowAndFight = marbles.showAndFight;
            playername.nameText.text = loadPlayerName;
            PhotonNetwork.NickName = loadPlayerName;
            money.text = loadPlayerMoney.ToString();
            Debug.LogWarning("讀取完成,你好  " + loadPlayerName);
        }
        else if (check == 0)
        {
            loadPlayerName = marbles.playerName;
            loadPlayerMoney = marbles.playerMoney;
            loadMarblesID = marbles.marblesID;
            loadShowAndFight = marbles.showAndFight;
            start.SetActive(false);
            playername.nameText.text = loadPlayerName;
            PhotonNetwork.NickName = loadPlayerName;
            money.text = loadPlayerMoney.ToString();
            Debug.LogWarning("讀取完成,你好  " + loadPlayerName);
        }
    }
    private void Update()
    {
        //獲取編號
        int index = SceneManager.GetActiveScene().buildIndex;
        if (index == 0)
        {
            if (money != null)
            {
                money.text = loadPlayerMoney.ToString();
            }
            else
            {
                money = GameObject.Find("Money").GetComponent<Text>();
                money.text = loadPlayerMoney.ToString();
            }
            if (playername == null)
            {
                playername = GameObject.Find("NetworkLauncher").GetComponent<NetwordLauncher>();
                playername.nameText.text = loadPlayerName;
            }
        }
        else
        {
            check = 1;
        }
    }
}