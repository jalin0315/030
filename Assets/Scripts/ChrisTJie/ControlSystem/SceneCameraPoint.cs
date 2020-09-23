using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SceneCameraPoint : MonoBehaviour
{
    [SerializeField] private Color _Color;
    [SerializeField] private float _RayLength;
    [SerializeField] private float _Radius;

    private void Update()
    {
        Vector3 _forward = transform.TransformDirection(Vector3.forward) * _RayLength;
        Debug.DrawRay(transform.position, _forward, _Color);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _Color;
        Gizmos.DrawSphere(transform.position, _Radius);
    }
}
