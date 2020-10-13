using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementSystem : MonoBehaviour
{
    public static MovementSystem _Instance;
    public bool _EnableGyro;
    public Rigidbody _Rigidbody;
    public Text Text;

    private void Awake()
    {
        _Instance = this;
    }

    private void Start()
    {
        _EnableGyro = true;
        Input.gyro.enabled = true;
        Input.gyro.updateInterval = 0.1f;
    }

    private void Update()
    {
        if (_EnableGyro == false) return;
        float _horizontal = Input.gyro.gravity.x;
        Vector3 _movement = new Vector3(_horizontal, 0.0f, 0.0f);
        Vector3 _actual_direction = Camera.main.transform.TransformDirection(_movement);
        _Rigidbody.AddForce(_actual_direction * 500 * 2); // * 50 * 2
        //Text.text = _horizontal.ToString();
    }
}
