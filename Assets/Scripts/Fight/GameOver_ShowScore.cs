using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver_ShowScore : MonoBehaviour
{
    public static int Peace;
    public Text peace, p1Name, p2Name, p1Win, p2Win, p1Lose, p2Lose, result;

    public NameAndScore NS;
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
    private void OnEnable()
    {
        peace.text = "平局次數 : " + Peace.ToString();

        p1Name.text = NS.p1.text;

        p2Name.text = NS.p2.text;

        p1Win.text = "勝利次數 : " + NameAndScore.P1Win.ToString();

        p2Win.text = "勝利次數 : " + NameAndScore.P2Win.ToString();

        p1Lose.text = "失敗次數 : " + (3 - Peace - NameAndScore.P1Win).ToString();

        p2Lose.text = "失敗次數 : " + (3 - Peace - NameAndScore.P2Win).ToString();

        if (NameAndScore.P1Win > NameAndScore.P2Win)
        {
            result.text = "勝利";
        }
        else if (NameAndScore.P1Win < NameAndScore.P2Win)
        {
            result.text = "失敗";
        }
        else if (NameAndScore.P1Win == NameAndScore.P2Win)
        {
            result.text = "平局";
        }
    }
}
