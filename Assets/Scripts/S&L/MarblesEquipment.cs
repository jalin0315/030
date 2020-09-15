using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

[System.Serializable]
public class MarblesEquipment
{
    public string playerName;
    public int playerMoney;
    public List<int> marblesID = new List<int>();
    public int showAndFight;
}
