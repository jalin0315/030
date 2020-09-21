using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WaypointsManager : MonoBehaviour
{
    public static WaypointsManager _Instance;
    [HideInInspector] public string _NamePrefix;
    [HideInInspector] public Vector3 _PositionAdder = Vector3.zero;
    [HideInInspector] public IconManager.Icon _SelectedIcon;
    [HideInInspector] public IconManager.LabelIcon _SelectedLabelIcon;
    [HideInInspector] public Color _PointLineColor;
    [HideInInspector] public int _Count = 0;
    [HideInInspector] public List<Transform> _Waypoints = new List<Transform>();

    // 啟用 / 禁用編輯模式
    public enum Mode
    {
        EnableEdit,
        DisableEdit
    }

    // 生成路徑點類型
    public enum DrawPointType
    {
        Icon,
        LabelIcon
    }
    [HideInInspector] public Mode _Mode;
    [HideInInspector] public DrawPointType _DrawPointType;

    private void Awake()
    {
        _Instance = this;
    }

    private void Update()
    {
        ResetWaypoints();
    }

    private void ResetWaypoints()
    {
        _Waypoints.Clear();
        foreach (Transform _waypoint in GetComponentsInChildren<Transform>())
        {
            if (_waypoint != transform) _Waypoints.Add(_waypoint);
        }
    }

    public void Return()
    {
        if (_Waypoints.Count == 0) return;
        int _count = _Waypoints.Count;
        DestroyImmediate(_Waypoints[_count - 1].gameObject);
        _Count--;
    }

    public void ClearAllPoints()
    {
        if (_Waypoints.Count == 0) return;
        for (int _i = 0; _i < _Waypoints.Count; _i++)
        {
            DestroyImmediate(_Waypoints[_i].gameObject);
            _Count = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _PointLineColor;
        for (int _i = 0; _i < _Waypoints.Count - 1; _i++)
        {
            Gizmos.DrawLine(_Waypoints[_i].position, _Waypoints[_i + 1].position);
        }
    }
}
