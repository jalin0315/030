using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RankManager : MonoBehaviour
{
    public static RankManager _Instance;
    public bool _MultiLapMode;
    public int _NumberOfTurns;
    public static List<GameObject> _RankText = new List<GameObject>();
    public List<GameObject> _rankText = new List<GameObject>();

    private Dictionary<string, PlayerRank> _Players;

    private void Awake()
    {
        _Instance = this;
    }

    private void Start()
    {
        _Players = new Dictionary<string, PlayerRank>();
    }

    public void SetRank(PlayerRank _player)
    {
        _Players[_player._Name] = _player;
        if (_MultiLapMode == true)
        {
            IOrderedEnumerable<KeyValuePair<string, PlayerRank>> _SortedPlayers = _Players.OrderBy(_x => _x.Value._RankGive).OrderBy(_x => _x.Value._DistanceToWaypoint).OrderByDescending(_x => _x.Value._ActiveWaypointIndex).OrderByDescending(_x => _x.Value._MultiLapWaypointIndex);
            int _i = 0;
            foreach (KeyValuePair<string, PlayerRank> _item in _SortedPlayers)
            {
                _RankText[_i].transform.SetParent(PlayerManager.Gps[_i].transform);
                _RankText[_i].transform.position = PlayerManager.Gps[_i].transform.position;
                _i++;
            }
            return;
        }
        if (_MultiLapMode == false)
        {
            IOrderedEnumerable<KeyValuePair<string, PlayerRank>> _SortedPlayers = _Players.OrderBy(_x => _x.Value._RankGive).OrderBy(_x => _x.Value._DistanceToWaypoint).OrderByDescending(_x => _x.Value._ActiveWaypointIndex);
            int _i = 0;
            foreach (KeyValuePair<string, PlayerRank> _item in _SortedPlayers)
            {
                _RankText[_i].transform.SetParent(PlayerManager.Gps[_i].transform);
                _RankText[_i].transform.position = PlayerManager.Gps[_i].transform.position;
                _i++;
            }
            return;
        }
    }
    void Update()
    {
        _rankText = _RankText;
    }
}

public class PlayerRank
{
    public static readonly PlayerRank _Instance;
    static PlayerRank()
    {
        _Instance = new PlayerRank();
    }

    public string _Name { get; set; }
    public int _ActiveWaypointIndex { get; set; }
    public int _MultiLapWaypointIndex { get; set; }
    public float _DistanceToWaypoint { get; set; }
    public int _RankGive { get; set; }

    public override string ToString()
    {
        return _ActiveWaypointIndex + "__" + _Name;
    }
}