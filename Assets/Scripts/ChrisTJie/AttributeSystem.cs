using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class AttributeSystem : MonoBehaviour
{
    [SerializeField] [Range(0.0f, 100.0f)] private float _SphereColliderRadius;
    [SerializeField] [ColorUsage(true)] private Color _SphereColliderColor;
    [Space(20)]
    [SerializeField] private SphereCollider _SphereCollider;
    [Space(20)]
    [SerializeField] private bool _Gravity;
    [SerializeField] private bool _AddForce;
    [SerializeField] private bool _VectorZero;
    [Space(20)]
    [SerializeField] private Vector3 _ConstantForce;
    [SerializeField] [Range(0.0f, 500.0f)] private float _Force;

    private List<ConstantForce> _object_constan_force = new List<ConstantForce>();
    private List<Vector3> _object_constan_force_original_value = new List<Vector3>();
    private void OnTriggerEnter(Collider other)
    {
        if (_Gravity == true)
        {
            ConstantForce _constant_force;
            _constant_force = other.GetComponent<ConstantForce>();
            _object_constan_force.Add(_constant_force);
            _object_constan_force_original_value.Add(_constant_force.force);
            _constant_force.force = _ConstantForce;
            return;
        }
        if (_AddForce == true)
        {
            Rigidbody _rigidbody;
            _rigidbody = other.GetComponent<Rigidbody>();
            _rigidbody.AddForce(transform.forward * _Force, ForceMode.Impulse);
            return;
        }
        if (_VectorZero == true)
        {
            Rigidbody _rigidbody;
            _rigidbody = other.GetComponent<Rigidbody>();
            _rigidbody.velocity = Vector3.zero;
            return;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_Gravity == true)
        {
            for (int _i = 0; _i < _object_constan_force.Count; _i++)
            {
                if (_object_constan_force[_i].transform == other.transform)
                {
                    ConstantForce _constant_force;
                    _constant_force = other.GetComponent<ConstantForce>();
                    _constant_force.force = _object_constan_force_original_value[_i];
                    _object_constan_force.RemoveAt(_i);
                    _object_constan_force_original_value.RemoveAt(_i);
                    break;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _SphereColliderColor;
        Gizmos.DrawWireSphere(transform.position, _SphereColliderRadius);
        _SphereCollider.radius = _SphereColliderRadius;
    }
}
