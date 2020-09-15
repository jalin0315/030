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
    }
    void Update()
    {
        
    }
}
