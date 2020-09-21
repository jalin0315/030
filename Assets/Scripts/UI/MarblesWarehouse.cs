using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarblesWarehouse : MonoBehaviour
{
    public int IDCalibration;    
    Button me;
    public L_ l;
    public S_ s;
    private void Awake()
    {
        if (!l.loadMarblesID.Contains(IDCalibration))
        {
            gameObject.SetActive(false);
        }
    }
    void Start()
    {
        me = GetComponent<Button>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (l.loadShowAndFight == IDCalibration && l.loadMarblesID.Count > 1) 
        {
            me.interactable = false;
        }
        else if (l.loadShowAndFight != IDCalibration)
        {
            me.interactable = true;
        }
    }
    public void Touch()
    {
        l.loadShowAndFight = IDCalibration;
        s.善良();
    }
}
