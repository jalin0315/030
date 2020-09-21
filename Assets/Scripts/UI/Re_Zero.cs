using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Re_Zero : MonoBehaviour
{
    public L_ l;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void F5()
    {
        for (int i = 0; i < target.transform.childCount; i++)
        {
            if (l.loadMarblesID.Contains(target.transform.GetChild(i).GetComponent<MarblesWarehouse>().IDCalibration))
            {
                target.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
