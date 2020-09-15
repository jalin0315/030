using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThisIsCamera : MonoBehaviour
{
    public GameObject target;
    public GameObject[] targetAllIn;
    public int u = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        target = targetAllIn[u];
        transform.LookAt(target.transform);
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (u < 7)
            {
                u += 1;
            }
            else if (u >= 7) 
            {
                u = 0;
            }
        }
    }
}
