using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;
    public Button loginButton;
    public Button createButton;

    public Button da;
    public static string ta;

    void Start()
    {
        loginButton.onClick.AddListener(() =>
        {
            StartCoroutine(Main.Instance.Web.Login(usernameInput.text, passwordInput.text));
            S_.帳號 = usernameInput.text;
            L_.帳號 = usernameInput.text;
        }); 
        
        createButton.onClick.AddListener(() =>
        {
            StartCoroutine(Main.Instance.Web.RegisterUser(usernameInput.text, passwordInput.text));
            S_.帳號 = usernameInput.text;
            L_.帳號 = usernameInput.text;
        });
        //da.onClick.AddListener(() =>
        //{
        //    StartCoroutine(Main.Instance.Web.Save(name1, ta));
        //});
    }
    void Update()
    {
    }
}
