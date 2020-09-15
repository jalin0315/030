using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float f1, f2, f3, f4, f5;

    public void Start()
    {
        f1 = 0.01f;
        f2 = 0.1f;
        f3 = 1f;
        f4 = 10f;
        f5 = 100f;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Time.timeScale = f1;
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Time.timeScale = f2;
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            Time.timeScale = f3;
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            Time.timeScale = f4;
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            Time.timeScale = f5;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
        }
    }
}
