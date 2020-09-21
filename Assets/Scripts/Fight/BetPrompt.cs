using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetPrompt : MonoBehaviour
{
    int burden;    //負擔
    public GameObject gambLing;
    public bool isOpen;
    public GameObject[] slot;
    public int check,bet;
    public static int Check;

    void Start()
    {
        check = 0;
        Check = 0;
        burden = 5;
    }
    void Update()
    {
        if (check == 1 && Check == 1 && !isOpen)
        {
            isOpen = true;
        }
        if (check == 1 && Check == 2 && isOpen)
        {
            isOpen = false;
            check = 0;
            Check = 1;
        }
        if (check == 2 && Check == 2)
        {
            check = 0;
            Check = 0;
            isOpen = false;
        }
        gambLing.SetActive(isOpen);
    }
    public void ㄅㄨㄚˇㄍㄧㄠˋ()
    {
        if (burden > 0 && Gambling.gamblingQuantity > 0 && Gambling.moneyQuantity > 0) 
        {
            bet += 1;
            burden -= 1;
            Gambling.gamblingQuantity -= 1;
            Gambling.moneyQuantity -= 1;
            slot[burden].SetActive(true);
        }
    }
    public void ㄟˇㄍㄧㄚ()
    {
        if (burden < 5 && Gambling.gamblingQuantity < 5 )
        {
            bet -= 1;
            burden += 1;
            Gambling.gamblingQuantity += 1;
            Gambling.moneyQuantity += 1;
            slot[burden - 1].SetActive(false);
        }
    }
    public void OpenGambling()
    {
        check += 1;
        Check += 1;
    }
}
