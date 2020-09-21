using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public int i;
    public static List<GameObject> Gambing_ = new List<GameObject>();
    public List<GameObject> gambing_ = new List<GameObject>();

    public List<int> bet = new List<int>();

    public Text moneyLook;

    void Awake()
    {
        moneyLook.text = GameObject.Find("S&L").GetComponent<L_>().loadPlayerMoney.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        gambing_ = Gambing_;

        for (int i = 0; i < Gambing_.Count; i++)
        {
            bet.Add(Gambing_[i].GetComponent<BetPrompt>().bet);
        }
    }
}
