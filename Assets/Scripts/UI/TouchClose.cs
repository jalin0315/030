using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchClose : MonoBehaviour
{
    public bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() && isOpen) 
            {
                Debug.Log("點擊到UGUI的UI界面，會返回true");
            }
            else
            {
                Debug.Log("如果沒點擊到UGUI上的任何東西，就會返回false");
            }
        }
    }
}
