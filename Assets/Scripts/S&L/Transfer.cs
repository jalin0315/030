﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transfer : MonoBehaviour
{
    public Text userName,userPass;

    Button me;
    void Start()
    {
        me = GetComponent<Button>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (userName.text.Length >= 5 && userPass.text.Length >= 5 && userPass.text.Length <= 16)
        {
            me.interactable = true;
        }
        else
        {
            me.interactable = false;
        }
    }
    public void transfer()
    {
        S_.username = userName.text.ToString();
        L_.username = userName.text.ToString();        
    }
}