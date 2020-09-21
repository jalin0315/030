using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TrackPoint : MonoBehaviourPun
{
    [Header("Debug")] [SerializeField] private bool _ShowLine = false;
    private GameObject _TrackPoint;
    public int _ActiveWaypointIndex = 0;
    public int _MultiLapWaypointIndex = 0;
    public int _PreviousLapIndex = -1;
    private Vector3 _LineStart;
    private Vector3 _LineEnd;
    private float _DistanceToEndPoint;
    private float _DistanceToStartPoint;
    public int _RankGive = -1;

    private void Start()
    {
        _TrackPoint = new GameObject();
        _TrackPoint.name = "TrackPoint_" + photonView.Owner.NickName;
        IconManager.SetIcon(_TrackPoint, IconManager.Icon.DiamondPurple);
    }

    private float _update_time;
    private void Update()
    {
        SetPosition();
        SetActiveWaypoint();
        RankManager._Instance.SetRank(new PlayerRank()
        {
            _ActiveWaypointIndex = _ActiveWaypointIndex,
            _MultiLapWaypointIndex = _MultiLapWaypointIndex,
            _DistanceToWaypoint = _DistanceToEndPoint,
            _Name = name,
            _RankGive = _RankGive
        });
        if (_ShowLine) Debug.DrawLine(transform.position, _TrackPoint.transform.position, Color.magenta);
    }

    // 為路徑點設置追蹤點位置
    private void SetPosition()
    {
        _LineStart = WaypointsManager._Instance._Waypoints[_ActiveWaypointIndex].position;
        _LineEnd = _LineStart;
        if (_ActiveWaypointIndex < WaypointsManager._Instance._Waypoints.Count - 1) _LineEnd = WaypointsManager._Instance._Waypoints[_ActiveWaypointIndex + 1].position;
        Vector3 _normal = (_LineStart - _LineEnd).normalized;
        Vector3 _pos = _LineStart + Vector3.Project(transform.position - _LineStart, _normal);
        _pos.x = Mathf.Clamp(_pos.x, Mathf.Min(_LineStart.x, _LineEnd.x), Mathf.Max(_LineStart.x, _LineEnd.x));
        _pos.y = Mathf.Clamp(_pos.y, Mathf.Min(_LineStart.y, _LineEnd.y), Mathf.Max(_LineStart.y, _LineEnd.y));
        _pos.z = Mathf.Clamp(_pos.z, Mathf.Min(_LineStart.z, _LineEnd.z), Mathf.Max(_LineStart.z, _LineEnd.z));
        _TrackPoint.transform.position = _pos;
        _DistanceToEndPoint = Vector3.Distance(_LineEnd, _TrackPoint.transform.position);
        _DistanceToStartPoint = Vector3.Distance(_LineStart, _TrackPoint.transform.position);
    }

    // Set active waypoint index and distance to waypoint
    private void SetActiveWaypoint()
    {
        if (RankManager._Instance._MultiLapMode == true)
        {
            if (0.01f >= _DistanceToEndPoint)
            {
                if (_ActiveWaypointIndex < WaypointsManager._Instance._Waypoints.Count - 1)
                {
                    _ActiveWaypointIndex++;
                    SetPosition();
                    if (_MultiLapWaypointIndex == RankManager._NumberOfTurns - 1)
                    {
                        _RankGive = PlayerRank._Instance._RankGive + 1;
                        PlayerRank._Instance._RankGive += 1;                        
                        return;
                    }
                    if (_ActiveWaypointIndex == WaypointsManager._Instance._Count - 1)
                    {
                        _ActiveWaypointIndex = 0;
                        _MultiLapWaypointIndex++;
                        _PreviousLapIndex = _MultiLapWaypointIndex - 1;
                        SetPosition();
                    }
                }
            }
        }
        if (RankManager._Instance._MultiLapMode == false)
        {
            if (0.1f >= _DistanceToEndPoint)
            {
                if (_ActiveWaypointIndex < WaypointsManager._Instance._Waypoints.Count - 1)
                {
                    _ActiveWaypointIndex++;
                    SetPosition();
                    if (_ActiveWaypointIndex == WaypointsManager._Instance._Waypoints.Count - 1)
                    {
                        _RankGive = PlayerRank._Instance._RankGive + 1;
                        PlayerRank._Instance._RankGive += 1;
                    }
                }
            }
        }
    }
}
