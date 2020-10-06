using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RankManager : MonoBehaviour
{
    public static RankManager _Instance;
    public bool _MultiLapMode;
    public static int _NumberOfTurns;    //圈數
    public int _numberOfTurns;    //圈數



    public static List<GameObject> _RankText = new List<GameObject>();
    public List<GameObject> _rankText = new List<GameObject>();
    private Dictionary<string, PlayerRank> _Players;

    public static bool isOpen;
    private void Awake()
    {
        _Instance = this;
    }

    private void Start()
    {
        _Players = new Dictionary<string, PlayerRank>();
        _NumberOfTurns = _numberOfTurns;
    }

    public void SetRank(PlayerRank _player)
    {
        if (isOpen)
            return;

        _Players[_player._Name] = _player;
        if (_MultiLapMode == true)
        {
            IOrderedEnumerable<KeyValuePair<string, PlayerRank>> _SortedPlayers = _Players.OrderBy(_x => _x.Value._RankGive).OrderBy(_x => _x.Value._DistanceToWaypoint).OrderByDescending(_x => _x.Value._ActiveWaypointIndex).OrderByDescending(_x => _x.Value._MultiLapWaypointIndex);
            int _i = 0;
            foreach (KeyValuePair<string, PlayerRank> _item in _SortedPlayers)
            {
                _RankText[_i].transform.GetChild(0).GetComponent<Text>().text = (_i + 1) + " . " + _item.Value._Name;
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
                _RankText[_i].transform.GetChild(0).GetComponent<Text>().text = (_i + 1) + " . " + _item.Value._Name;
                _i++;
            }
            return;
        }
    }
    private void Update()
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