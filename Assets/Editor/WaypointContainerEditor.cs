using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WaypointsManager))]
public class WaypointContainerEditor : Editor
{
    RaycastHit _HitInfo;

    // Inspector 操作邏輯
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        WaypointsManager _w_m = target as WaypointsManager;
        if (_w_m._Mode == WaypointsManager.Mode.EnableEdit)
        {
            if (GUILayout.Button("關閉編輯"))
            {
                _w_m._Mode = WaypointsManager.Mode.DisableEdit;
                return;
            }
            _w_m._NamePrefix = EditorGUILayout.TextField("物件標題名稱", _w_m._NamePrefix);
            _w_m._PositionAdder = EditorGUILayout.Vector3Field("位置偏移", _w_m._PositionAdder);
            _w_m._DrawPointType = (WaypointsManager.DrawPointType)EditorGUILayout.EnumPopup("圖示類型", _w_m._DrawPointType);
            if (_w_m._DrawPointType == WaypointsManager.DrawPointType.Icon) _w_m._SelectedIcon = (IconManager.Icon)EditorGUILayout.EnumPopup("圖標", _w_m._SelectedIcon);
            else if (_w_m._DrawPointType == WaypointsManager.DrawPointType.LabelIcon) _w_m._SelectedLabelIcon = (IconManager.LabelIcon)EditorGUILayout.EnumPopup("標籤", _w_m._SelectedLabelIcon);
            _w_m._PointLineColor = EditorGUILayout.ColorField("路線色彩", _w_m._PointLineColor);
            EditorGUILayout.IntField("路徑點總數", _w_m._Waypoints.Count);
            EditorGUILayout.IntField("路徑點計數器", _w_m._Count);
            if (GUILayout.Button("返回上一步")) _w_m.Return();
            if (GUILayout.Button("重置路徑點計數器")) _w_m._Count = 0;
            if (GUILayout.Button("刪除全部路徑點")) _w_m.ClearAllPoints();
            EditorGUILayout.LabelField("說明：在編輯場景中，滑鼠中鍵新增路徑點。");
            return;
        }
        if (_w_m._Mode == WaypointsManager.Mode.DisableEdit)
        {
            if (GUILayout.Button("啟用編輯"))
            {
                _w_m._Mode = WaypointsManager.Mode.EnableEdit;
                return;
            }
        }
    }

    // 增加路徑點邏輯
    private void OnSceneGUI()
    {
        WaypointsManager _w_m = target as WaypointsManager;
        if (_w_m._Mode == WaypointsManager.Mode.EnableEdit)
        {
            // 紀錄當前的事件
            Event _e = Event.current;
            // 若當前事件為按下滑鼠右鍵並放開鍵鼠時則執行
            if (_e.type == EventType.MouseUp && _e.button == 2)
            {
                // 取得當前編輯場景畫面鼠標座標位置
                Ray _world_ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                if (Physics.Raycast(_world_ray, out _HitInfo)) AddWaypoint();
            }
        }
    }

    // 增加路徑點功能
    private void AddWaypoint()
    {
        WaypointsManager _w_m = target as WaypointsManager;
        GameObject _waypoint_object = new GameObject();
        _waypoint_object.transform.position = _HitInfo.point + _w_m._PositionAdder;
        _waypoint_object.transform.parent = Selection.activeGameObject.transform;
        _waypoint_object.name = _w_m._NamePrefix + "_" + _w_m._Count;
        if (_w_m._DrawPointType == WaypointsManager.DrawPointType.Icon) IconManager.SetIcon(_waypoint_object, _w_m._SelectedIcon);
        else if (_w_m._DrawPointType == WaypointsManager.DrawPointType.LabelIcon) IconManager.SetLabelIcon(_waypoint_object, _w_m._SelectedLabelIcon);
        _w_m._Count++;
    }
}
