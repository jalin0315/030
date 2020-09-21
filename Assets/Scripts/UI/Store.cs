using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    public L_ l;
    public S_ s;
    public MarblesEquipment data;
    public int IDCalibration,money;    //calibration校準
    public GameObject noMoney;
    public Text Money;
    Button me;
    void Awake()
    {
        money = int.Parse(Money.text);
        me = GetComponent<Button>();

        foreach (var marbles in l.loadMarblesID)
        {
            if (marbles == IDCalibration)
            {
                me.interactable = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SavePurchasedMarbles()
    {
        if (l.loadPlayerMoney > 0 && l.loadPlayerMoney - money >= 0)
        {
            l.loadMarblesID.Add(IDCalibration);
            l.loadPlayerMoney = l.loadPlayerMoney - money;
            s.善良();
            me.interactable = false;
        }
        else
        {
            noMoney.SetActive(true);
        }
    }
}
