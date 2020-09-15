using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptUI : MonoBehaviour
{
    public List<GameObject> promptPut = new List<GameObject>();
    public static List<GameObject> PromptPut = new List<GameObject>();
    void Update()
    {
        PromptPut = promptPut;
    }
}
