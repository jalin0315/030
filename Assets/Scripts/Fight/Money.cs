using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public int i;
    void Awake()
    {
        GetComponent<UnityEngine.UI.Text>().text = GameObject.Find("S&L").GetComponent<L_>().loadPlayerMoney.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
