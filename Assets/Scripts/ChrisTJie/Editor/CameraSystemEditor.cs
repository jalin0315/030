using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(CameraSystem))]
public class CameraSystemEditor : Editor
{
    private CameraSystem _c_s;
    private SerializedProperty _SpectatorPlayerList;
    private SerializedProperty _SceneCameraList;

    private void OnEnable()
    {
        _SpectatorPlayerList = serializedObject.FindProperty("_SpectatorPlayerList");
        _SceneCameraList = serializedObject.FindProperty("_SceneCameraList");
    }

    public override void OnInspectorGUI()
    {
        _c_s = target as CameraSystem;
        if (_c_s._Mode == CameraSystem.Mode.Disable)
        {
            if (GUILayout.Button("啟用編輯")) _c_s._Mode = CameraSystem.Mode.Enable;
            return;
        }
        if (_c_s._Mode == CameraSystem.Mode.Enable)
        {
            if (GUILayout.Button("關閉編輯"))
            {
                _c_s._Mode = CameraSystem.Mode.Disable;
                return;
            }
            _c_s._SwitchButtonText = EditorGUILayout.ObjectField("顯示切換按鈕文字控制", _c_s._SwitchButtonText, typeof(Text), true) as Text;
            _c_s._LeftButton = EditorGUILayout.ObjectField("左按鈕控制", _c_s._LeftButton, typeof(Button), true) as Button;
            _c_s._RightButton = EditorGUILayout.ObjectField("右按鈕控制", _c_s._RightButton, typeof(Button), true) as Button;
            _c_s._SpectatorsName = EditorGUILayout.ObjectField("被觀戰者控制", _c_s._SpectatorsName, typeof(Text), true) as Text;
            _c_s._CameraMode = (CameraSystem.CameraMode)EditorGUILayout.EnumPopup("攝影機模式", _c_s._CameraMode);
            if (_c_s._CameraMode == CameraSystem.CameraMode.Local)
            {
                CameraSystem._LocalPlayer = EditorGUILayout.ObjectField("本地玩家", CameraSystem._LocalPlayer, typeof(Transform), true) as Transform;
                _c_s._Pivot = EditorGUILayout.ObjectField("攝影機樞紐校正物件", _c_s._Pivot, typeof(Transform), true) as Transform;
                _c_s._Y_Offset = EditorGUILayout.FloatField("偏移高度數值", _c_s._Y_Offset);
                _c_s._Distance = EditorGUILayout.FloatField("距離於目標物數值", _c_s._Distance);
                _c_s._SmoothSpeed = EditorGUILayout.FloatField("平滑速率", _c_s._SmoothSpeed);
                return;
            }
            if (_c_s._CameraMode == CameraSystem.CameraMode.Spectator)
            {
                _c_s._FixedTouchField = EditorGUILayout.ObjectField("觸控系統", _c_s._FixedTouchField, typeof(FixedTouchField), true) as FixedTouchField;
                serializedObject.Update();
                EditorGUILayout.PropertyField(_SpectatorPlayerList, new GUIContent("觀察總玩家列表"), true);
                serializedObject.ApplyModifiedProperties();
                _c_s._Y_SpectatorOffset = EditorGUILayout.FloatField("偏移高度數值", _c_s._Y_SpectatorOffset);
                _c_s._SpectatorSmoothSpeed = EditorGUILayout.FloatField("平滑速率", _c_s._SpectatorSmoothSpeed);
                _c_s._CameraAngleSpeed = EditorGUILayout.FloatField("左右觸控靈敏度", _c_s._CameraAngleSpeed);
                _c_s._CameraPosSpeed = EditorGUILayout.FloatField("上下觸控靈敏度", _c_s._CameraPosSpeed);
                return;
            }
            if (_c_s._CameraMode == CameraSystem.CameraMode.Scene)
            {
                serializedObject.Update();
                EditorGUILayout.PropertyField(_SceneCameraList, new GUIContent("場景總攝影機列表"), true);
                serializedObject.ApplyModifiedProperties();
                _c_s._SceneCameraSmoothSpeed = EditorGUILayout.FloatField("平滑速率", _c_s._SceneCameraSmoothSpeed);
            }
        }
    }
}
