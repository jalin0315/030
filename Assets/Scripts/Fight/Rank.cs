using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rank : MonoBehaviour
{
    public List<GameObject> marbles = new List<GameObject>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("彈珠") && !marbles.Contains(other.gameObject))
        {
            marbles.Add(other.gameObject);
            other.GetComponent<Marbles>().rank += 1;
        }
    }
}
