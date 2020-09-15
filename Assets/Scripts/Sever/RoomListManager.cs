using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class RoomListManager : MonoBehaviourPunCallbacks
{/*
    public GameObject roomNamePrefab;
    public Transform gridLayout;
    public string rom;*/    //房間列表 擱置

    //public override void OnRoomListUpdate(List<RoomInfo> roomList)
    //{
    //    Debug.Log("房間數量 : " + roomList.Count);
    //    for (int i = 0; i < roomList.Count; i++)
    //    {
    //        if (roomList[i].PlayerCount == 0)
    //        {
    //            roomList.Remove(roomList[i]);
    //        }
    //    }        
    //}

    public static List<RoomInfo> roomID = new List<RoomInfo>();
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        /*for (int i = 0; i < gridLayout.childCount; i++)
        {
            if (gridLayout.GetChild(i).GetChild(0).GetComponent<Text>().text == roomList[i].Name)
            {
                Destroy(gridLayout.GetChild(i).gameObject);
            }
        }*/    //房間列表 擱置

        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].PlayerCount == 0)
            {
                roomList.Remove(roomList[i]);
            }
        }
        roomID = roomList;
        /*foreach (var room in roomList)
        {
            GameObject newRoom = Instantiate(roomNamePrefab, gridLayout.position, Quaternion.identity);

            newRoom.transform.GetChild(0).GetComponent<Text>().text = room.Name;
            newRoom.transform.GetChild(1).GetComponent<Text>().text = room.PlayerCount.ToString();
            newRoom.transform.GetChild(2).GetComponent<Text>().text = "/" + room.MaxPlayers.ToString();
            rom = room.Name;
            newRoom.transform.SetParent(gridLayout);
        }*/    //房間列表 擱置
    }
}