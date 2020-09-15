using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Prompt : MonoBehaviour
{
    public float charsPerSecond = 0.2f;//打字間隔的時間
    private string words;//儲存需要顯示的文字
    private bool isActive = false;
    private float timer;//計時器
    public Text myText;
    private int currentPos = 1;

    private void Start()
    {        
        timer = 0f;
        charsPerSecond = Mathf.Max(0.2f, charsPerSecond);
        myText = GetComponent<Text>();        
    }

    private void Update()
    {
        OnStartWriter();
    }

    public void StartEffect()
    {
        words = myText.text;//獲取Text的文字資訊，儲存到words中，然後動態更新文字
        //顯示的內容，實現打字機的效果
        myText.text = "";
        isActive = true;
        
    }

    //執行打字任務
    public void OnStartWriter()
    {
        if (isActive)
        {
            timer += Time.deltaTime;
            if (timer >= charsPerSecond)
            {
                timer = 0f;
                currentPos++;
                //重新整理文字顯示內容
                myText.text = words.Substring(1, currentPos-1);
                if (currentPos >= words.Length)
                {
                    OnFinish();
                }
            }
        }
    }

    //結束打字，初始化資料
    private void OnFinish()
    {
        isActive = false;
        timer = 0;
        currentPos = 0;
        myText.text = words;
    }
}