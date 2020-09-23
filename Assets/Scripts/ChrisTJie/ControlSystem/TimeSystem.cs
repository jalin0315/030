using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSystem : MonoBehaviour
{
    [SerializeField] private float _TimeScale;
    [SerializeField] private float _FixedDeltaTime;
    [SerializeField] private int _Fps;

    private void Awake()
    {
        Time.timeScale = _TimeScale;
        Time.fixedDeltaTime = Time.timeScale * _FixedDeltaTime;
        Application.targetFrameRate = _Fps;
    }

    private void Start()
    {

    }

    private void Update()
    {

    }
}
