using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeForce : MonoBehaviour
{
    public float x, y, z;
    public bool isOpen;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("彈珠"))
        {
            collision.gameObject.GetComponent<ConstantForce>().force = new Vector3(x, y, z);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("彈珠") && !isOpen)
        {
            other.gameObject.GetComponent<ConstantForce>().force = new Vector3(x, y, z);
        }
        if (other.gameObject.CompareTag("彈珠") && isOpen)
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(transform.right * 100, ForceMode.Impulse);
            other.gameObject.GetComponent<ConstantForce>().force = new Vector3(40, -190, 0);
        }
    }
}
