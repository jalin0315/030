using Photon.Pun.UtilityScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class AttributeSystem : MonoBehaviour
{
    [HideInInspector] public string _NamePrefix = "Attribute";
    [HideInInspector] public bool _CopyType;
    [HideInInspector] public BoxCollider _BoxCollider;
    [HideInInspector] public SphereCollider _SphereCollider;
    [HideInInspector] public Color _ColliderColor = Color.white;
    [HideInInspector] public Vector3 _BoxSize;
    [HideInInspector] public float _SphereRadius;
    [HideInInspector] public Vector3 _ConstantForceValue;
    [HideInInspector] public bool _Disposable;
    [HideInInspector] public float _ForceValue;
    [HideInInspector] public Vector3 _ForceDirectionValue;
    [HideInInspector] public Vector3 _VelocityValue;

    public enum Mode
    {
        Enable,
        Disable
    }
    [HideInInspector] public Mode _Mode = Mode.Enable;

    public enum ColliderType
    {
        None = -1,
        Box,
        Sphere
    }
    [HideInInspector] public ColliderType _ColliderType = ColliderType.None;

    public enum AttributeMode
    {
        None = -1,
        Gravity,
        Force,
        Velocity
    }
    [HideInInspector] public AttributeMode _AttributeMode = AttributeMode.None;

    public enum Direction
    {
        Right,
        Up,
        Forward,
        Customize
    }
    [HideInInspector] public Direction _Direction = Direction.Right;

    public void Instance()
    {
        if (_CopyType == true)
        {
            Component _source = GetComponent(typeof(AttributeSystem));
            // 同樣先取得 Component 的 Type
            Type _type = _source.GetType();
            GameObject _attribute_object = new GameObject(_NamePrefix);
            _attribute_object.transform.position = transform.position;
            _attribute_object.transform.parent = transform.parent;
            // 先把這個類型的初始 Component 加到物件上
            Component _target = _attribute_object.AddComponent(_type);
            // 使用 Reflection 取得此 Type 上的所有 Fields
            FieldInfo[] _fields = _type.GetFields();
            foreach (FieldInfo _field in _fields)
            {
                // 把來源 Component 上的所有 Field 數值設定到目標 Component 上
                _field.SetValue(_target, _field.GetValue(_source));
            }
            if (_ColliderType == ColliderType.None) return;
            if (_ColliderType == ColliderType.Box)
            {
                AttributeSystem _attribute_system = _attribute_object.GetComponent<AttributeSystem>();
                _attribute_object.AddComponent<BoxCollider>().isTrigger = true;
                _attribute_system._BoxCollider = _attribute_object.GetComponent<BoxCollider>();
                return;
            }
            if (_ColliderType == ColliderType.Sphere)
            {
                AttributeSystem _attribute_system = _attribute_object.GetComponent<AttributeSystem>();
                _attribute_object.AddComponent<SphereCollider>().isTrigger = true;
                _attribute_system._SphereCollider = _attribute_object.GetComponent<SphereCollider>();
                return;
            }
            return;
        }
        if (_CopyType == false)
        {
            GameObject _attribute_object = new GameObject(_NamePrefix, typeof(AttributeSystem));
            _attribute_object.transform.position = transform.position;
            _attribute_object.transform.parent = transform.parent;
            return;
        }
    }

    public void AddCollider()
    {
        if (_ColliderType == ColliderType.Box)
        {
            _BoxCollider = gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;
            _BoxCollider.isTrigger = true;
            return;
        }
        if (_ColliderType == ColliderType.Sphere)
        {
            _SphereCollider = gameObject.AddComponent(typeof(SphereCollider)) as SphereCollider;
            _SphereCollider.isTrigger = true;
            return;
        }
    }

    public void DeleteCollider()
    {
        if (_ColliderType == ColliderType.Box)
        {
            DestroyImmediate(gameObject.GetComponent<BoxCollider>());
            return;
        }
        if (_ColliderType == ColliderType.Sphere)
        {
            DestroyImmediate(gameObject.GetComponent<SphereCollider>());
            return;
        }
    }

    private List<ConstantForce> _object_constan_force = new List<ConstantForce>();
    private List<Vector3> _object_constan_force_original_value = new List<Vector3>();
    private void OnTriggerEnter(Collider other)
    {
        if (_AttributeMode == AttributeMode.None) return;
        if (_AttributeMode == AttributeMode.Gravity)
        {
            if (_Disposable == true)
            {
                ConstantForce _constant_force = other.GetComponent<ConstantForce>();
                _constant_force.force = _ConstantForceValue;
                return;
            }
            if (_Disposable == false)
            {
                ConstantForce _constant_force = other.GetComponent<ConstantForce>();
                _object_constan_force.Add(_constant_force);
                _object_constan_force_original_value.Add(_constant_force.force);
                _constant_force.force = _ConstantForceValue;
                return;
            }
        }
        if (_AttributeMode == AttributeMode.Force)
        {
            Rigidbody _rigidbody = other.GetComponent<Rigidbody>();
            if (_Direction == Direction.Right)
            {
                _rigidbody.AddForce(transform.right * _ForceValue, ForceMode.Impulse);
                return;
            }
            if (_Direction == Direction.Up)
            {
                _rigidbody.AddForce(transform.up * _ForceValue, ForceMode.Impulse);
                return;
            }
            if (_Direction == Direction.Forward)
            {
                _rigidbody.AddForce(transform.forward * _ForceValue, ForceMode.Impulse);
                return;
            }
            if (_Direction == Direction.Customize)
            {
                _rigidbody.AddForce(_ForceDirectionValue * _ForceValue, ForceMode.Impulse);
                return;
            }
        }
        if (_AttributeMode == AttributeMode.Velocity)
        {
            Rigidbody _rigidbody = other.GetComponent<Rigidbody>();
            _rigidbody.velocity = _VelocityValue;
            return;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_AttributeMode == AttributeMode.None) return;
        if (_AttributeMode == AttributeMode.Gravity)
        {
            if (_Disposable == true) return;
            if (_Disposable == false)
            {
                for (int _i = 0; _i < _object_constan_force.Count; _i++)
                {
                    if (_object_constan_force[_i].transform == other.transform)
                    {
                        ConstantForce _constant_force = other.GetComponent<ConstantForce>();
                        _constant_force.force = _object_constan_force_original_value[_i];
                        _object_constan_force.RemoveAt(_i);
                        _object_constan_force_original_value.RemoveAt(_i);
                        break;
                    }
                }
                return;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (_ColliderType == ColliderType.Box)
        {
            if (_BoxCollider != null)
            {
                Gizmos.color = _ColliderColor;
                _BoxCollider.size = _BoxSize;
                Gizmos.DrawWireCube(transform.position, new Vector3(_BoxSize.x, _BoxSize.y, _BoxSize.z));
                return;
            }
            else return;
        }
        if (_ColliderType == ColliderType.Sphere)
        {
            if (_SphereCollider != null)
            {
                Gizmos.color = _ColliderColor;
                _SphereCollider.radius = _SphereRadius;
                Gizmos.DrawWireSphere(transform.position, _SphereRadius);
                return;
            }
            else return;
        }
    }
}
