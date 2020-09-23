using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class CameraTrigger : MonoBehaviour
{
    [SerializeField] private SphereCollider _SphereCollider;
    [SerializeField] private Color _TriggerColor;

    private void OnTriggerEnter(Collider other)
    {
        if (CameraSystem._Instance._LookAt != other.transform) return;
        CameraSystem._Instance.LocalControl(transform);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _TriggerColor;
        Gizmos.DrawWireSphere(transform.position, _SphereCollider.radius);
    }
}
