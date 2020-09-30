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
    public string ta,name1;

    void Start()
    {
        loginButton.onClick.AddListener(() =>
        {
            StartCoroutine(Main.Instance.Web.Login(usernameInput.text, passwordInput.text));
        }); 
        
        createButton.onClick.AddListener(() =>
        {
            StartCoroutine(Main.Instance.Web.RegisterUser(usernameInput.text, passwordInput.text));
        });
        //da.onClick.AddListener(() =>
        //{
        //    StartCoroutine(Main.Instance.Web.Save(name1,ta));
        //});
    }
    void Update()
    {
        //name1 = usernameInput.text.ToString();
    }
}
