using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gambling : MonoBehaviour
{
    public Text money;
    public static int gamblingQuantity, moneyQuantity;
    void Start()
    {
        gamblingQuantity = 5;
        moneyQuantity = int.Parse(money.text);
    }

    // Update is called once per frame
    void Update()
    {
        money.text = moneyQuantity.ToString();
    }
}
