using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AttributeSystem))]
public class AttributeSystemEditor : Editor
{
    private AttributeSystem _a_s;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        _a_s = target as AttributeSystem;
        if (_a_s._Mode == AttributeSystem.Mode.Enable)
        {
            if (GUILayout.Button("關閉編輯"))
            {
                _a_s._Mode = AttributeSystem.Mode.Disable;
                return;
            }
            _a_s._NamePrefix = EditorGUILayout.TextField("標題名稱", _a_s._NamePrefix);
            if (GUILayout.Button("實例化物件")) _a_s.Instance();
            _a_s._CopyType = EditorGUILayout.Toggle("複製實例化參數數值", _a_s._CopyType);
            _a_s._ColliderType = (AttributeSystem.ColliderType)EditorGUILayout.EnumPopup("觸發器選擇", _a_s._ColliderType);
            if (_a_s._ColliderType == AttributeSystem.ColliderType.None)
            {
                _a_s._AttributeMode = AttributeSystem.AttributeMode.None;
                return;
            }
            if (GUILayout.Button("生成觸發器")) _a_s.AddCollider();
            if (GUILayout.Button("刪除觸發器")) _a_s.DeleteCollider();
            _a_s._ColliderColor = EditorGUILayout.ColorField("觸發器顏色", _a_s._ColliderColor);
            if (_a_s._ColliderType == AttributeSystem.ColliderType.Box)
            {
                _a_s._BoxCollider = EditorGUILayout.ObjectField("方塊觸發器", _a_s._BoxCollider, typeof(BoxCollider), true) as BoxCollider;
                _a_s._BoxSize = EditorGUILayout.Vector3Field("方塊觸發器大小", _a_s._BoxSize);
            }
            else if (_a_s._ColliderType == AttributeSystem.ColliderType.Sphere)
            {
                _a_s._SphereCollider = EditorGUILayout.ObjectField("圓球觸發器", _a_s._SphereCollider, typeof(SphereCollider), true) as SphereCollider;
                _a_s._SphereRadius = EditorGUILayout.FloatField("圓球觸發器大小", _a_s._SphereRadius);
            }
            _a_s._AttributeMode = (AttributeSystem.AttributeMode)EditorGUILayout.EnumPopup("屬性模式改變", _a_s._AttributeMode);
            if (_a_s._AttributeMode == AttributeSystem.AttributeMode.None) return;
            if (_a_s._AttributeMode == AttributeSystem.AttributeMode.Gravity)
            {
                _a_s._ConstantForceValue = EditorGUILayout.Vector3Field("恆力設置", _a_s._ConstantForceValue);
                _a_s._Disposable = EditorGUILayout.Toggle("一次性觸發", _a_s._Disposable);
                return;
            }
            if (_a_s._AttributeMode == AttributeSystem.AttributeMode.Force)
            {
                _a_s._ForceValue = EditorGUILayout.FloatField("力道設置", _a_s._ForceValue);
                _a_s._Direction = (AttributeSystem.Direction)EditorGUILayout.EnumPopup("方向性", _a_s._Direction);
                if (_a_s._Direction == AttributeSystem.Direction.Customize) _a_s._ForceDirectionValue = EditorGUILayout.Vector3Field("自定義方向設置", _a_s._ForceDirectionValue);
                return;
            }
            if (_a_s._AttributeMode == AttributeSystem.AttributeMode.Velocity)
            {
                _a_s._VelocityValue = EditorGUILayout.Vector3Field("速率設置", _a_s._VelocityValue);
                return;
            }
        }
        if (_a_s._Mode == AttributeSystem.Mode.Disable)
        {
            if (GUILayout.Button("開啟編輯"))
            {
                _a_s._Mode = AttributeSystem.Mode.Enable;
                return;
            }
        }
    }
}
