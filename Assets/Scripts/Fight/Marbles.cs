using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marbles : MonoBehaviour
{
    public Vector3 vv;
    int i;
    public int ID,rank;
    private void Awake()
    {
        vv = transform.position;
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(transform.right * -400, ForceMode.Impulse);
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            i += 1;
            transform.position = vv;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Debug.Log(i);
        }
        if (other.gameObject.CompareTag("Over"))
        {
            Instantiate(gameObject, new Vector3(vv.x, vv.y, vv.z), transform.rotation);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GG"))
        {
            transform.position = new Vector3(vv.x, vv.y + 10, vv.z);
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<ConstantForce>().enabled = false ;
        }

        if (collision.gameObject.CompareTag("Finish"))    //第一個到 計算排名
        {
            i += 1;
            transform.position = vv;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Debug.Log(i);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        
    }
}
