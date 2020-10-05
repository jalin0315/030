using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Photon.Pun;
using UnityEngine.UI;

public class S_ : MonoBehaviourPun
{
    public static string 帳號;
    //public List<Equipment> slot = new List<Equipment>();
    public static string username;

    public L_ l;

    public string savePlayerName;
    public List<int> saveMarblesID = new List<int>();

    public InputField myname;
    private void Update()
    {

    }
    //string fileName = "gamesave.dat";
    public void 善良()
    {
        //宣告一個Listy作為道具列表並存入兩個道具

        //用剛剛宣告好的Class創建一個儲存數值的物件，並給予數值欄位對應的數值(例如生命設定為100，金錢250....)

        MarblesEquipment MbE = new MarblesEquipment
        {
            marblesID = l.loadMarblesID,
            playerMoney = l.loadPlayerMoney,
            playerName = l.loadPlayerName,
            showAndFight = l.loadShowAndFight
        };

        //把剛剛創建好的數值物件轉為Json字串，並用JsonInfo參數儲存，接下來把這個字串寫入指定的檔案位置(下面紅色字請改成自己的路徑《都可以》最後面是檔案名稱)

        string jsonInfo = JsonUtility.ToJson(MbE, true);
        StartCoroutine(Main.Instance.Web.Save(帳號, jsonInfo));
        //File.WriteAllText(Application.persistentDataPath + "/" + fileName, jsonInfo);

        Debug.LogWarning("寫入完成");
           
    }
    public void n善良()
    {
        MarblesEquipment MbE = new MarblesEquipment
        {
            marblesID = l.loadMarblesID,
            playerMoney = l.loadPlayerMoney,
            playerName = myname.text,
            showAndFight = l.loadShowAndFight
        };


        string jsonInfo = JsonUtility.ToJson(MbE, true);
        StartCoroutine(Main.Instance.Web.Save(帳號, jsonInfo));

        Debug.LogWarning("寫入完成");
    }

}

