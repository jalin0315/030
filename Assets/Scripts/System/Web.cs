using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Web : MonoBehaviour
{
    public Prompt 提示;

    public Login 登入;

    public NetwordLauncher 開始連線;

    public GameObject 系統面板;

    public L_ 讀檔;

    void Start()
    {
        //StartCoroutine(GetDate("http://localhost/UnityBackend/GetDate.php"));    //日期
        //StartCoroutine(GetUsers("http://localhost/UnityBackend/GetUsers.php"));    //用戶
        //StartCoroutine(Login("testuser", "123456"));    //登錄
        //StartCoroutine(RegisterUser("-/-", "123456"));    //創立
    }

    IEnumerator GetDate(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
            }
        }
    }
    IEnumerator GetUsers(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
            }
        }
    }

    public IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://192.168.0.3/UnityBackend/Login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                提示.myText.text = www.error.ToString();
            }
            else
            {
                提示.myText.text = www.downloadHandler.text.ToString();    //登入成功
                if (提示.myText.text == "登入成功")
                {
                    開始連線.EnterGame();
                    讀檔.善良();
                    系統面板.SetActive(false);
                }
                else if (提示.myText.text == "帳號密碼錯誤或此帳號不存在")
                {
                    登入.usernameInput.text = null;
                    登入.passwordInput.text = null;
                }
                else if (提示.myText.text == "帳號密碼錯誤或此帳號不存在")
                {
                    登入.usernameInput.text = null;
                    登入.passwordInput.text = null;
                }
            }
        }
    }
    public IEnumerator RegisterUser(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://192.168.0.3/UnityBackend/RegisterUser.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                提示.myText.text = www.error.ToString();
            }
            else
            {
                提示.myText.text = www.downloadHandler.text.ToString();
                if (提示.myText.text == "建立帳號中...帳號建立成功")
                {
                    開始連線.EnterGame();
                    讀檔.善良();
                    系統面板.SetActive(false);
                }
                else if (提示.myText.text == "這個帳號已經被使用")
                {
                    登入.usernameInput.text = null;
                    登入.passwordInput.text = null;
                }
            }
        }
    }
}
