using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSystem : MonoBehaviour
{
    [SerializeField] private float _TimeScale;
    [SerializeField] private float _FixedDeltaTime;

    private void Awake()
    {
        Time.timeScale = _TimeScale;
        Time.fixedDeltaTime = Time.timeScale * _FixedDeltaTime;
    }

    private void Start()
    {

    }

    private void Update()
    {

    }
}
