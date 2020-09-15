using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class NetwordLauncher : MonoBehaviourPunCallbacks,IPunObservable
{
    //public static NetwordLauncher instance;
    public InputField roomNameCreate, roomNameSearch;
    public InputField playerName;
    public Text nameText, roomText;
    public Image roomList;
    public GameObject chat;
    public bool random,create;
    public int index;

    public byte Maxplayer;  

    public GameObject roomNoExist;

    public string roomName;

    ShowMarbles marbles;
    public GameObject playerNamePrefab;
    public Transform gridLayout;

    public GameObject 抽大圖, 抽小圖,創建面板;    //抽小圖還沒做

    public int ran,mapCheck;
    public static int mapSelect;
    public Button slot,StarT;

    public Dropdown maxplayer;
    public bool check1 = true, check2 = true, check3 = true, check4 = true;

    public Toggle pas;

    Hashtable rmnm = new Hashtable();
    Hashtable noPas = new Hashtable();

    public S_ save;
    public L_ load;

    bool isPublic;
    private void Start()
    {
        mapSelect = 1;
        marbles = GameObject.Find("ShowTime").gameObject.GetComponent<ShowMarbles>();
        //if (instance == null)
        //{
        //    instance = this;
        //    DontDestroyOnLoad(this);
        //}
        random = false;
        PhotonNetwork.AutomaticallySyncScene = true;    //AutomaticallySyncScene自動同步場景
        S_L.Judgment = 0;
    }
    public void NameCheck()
    {
        if (PhotonNetwork.NickName != null)
        {
            nameText.text = PhotonNetwork.NickName;
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        Debug.Log("加入大廳");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        roomNoExist.SetActive(true);
    }
    public void JoinButton()
    {   
        if (roomNameSearch.text.Length < 1)
        {
            return;
        }
        else
        {
            PhotonNetwork.JoinRoom(roomNameSearch.text);
            marbles.isBack = true;
        }
    }
    public void CreateButton()
    {
        if (random)
        {
            roomText.text = "滾滾滾起來";
            RoomOptions options = new RoomOptions { MaxPlayers = 8 };
            PhotonNetwork.CreateRoom("滾滾滾起來", options, default);
        }
        else
        {
            if (!pas.isOn)
            {
                if (roomNameCreate.text.Length < 1 || roomNameCreate.text.Length > 12)
                    return;
                if (RoomListManager.roomID.Count > 0)
                {
                    foreach (var room in RoomListManager.roomID)
                    {
                        if (room.Name == roomNameCreate.text)
                        {
                            PromptUI.PromptPut[4].SetActive(true);
                            return;
                        }
                        else
                        {
                            創建面板.SetActive(false);

                            isPublic = true;
                            if (noPas.ContainsKey("Public"))
                            {
                                noPas.Remove("Public");
                                noPas.Add("Public", isPublic);
                                PhotonNetwork.LocalPlayer.SetCustomProperties(noPas, null);
                            }
                            else
                            {
                                noPas.Add("Public", isPublic);
                                PhotonNetwork.LocalPlayer.SetCustomProperties(noPas, null);
                            }

                            roomText.text = roomNameCreate.text;
                            int i = int.Parse(maxplayer.transform.GetChild(0).GetComponent<Text>().text);

                            Maxplayer = (byte)i;
                            RoomOptions options = new RoomOptions();
                            options.CustomRoomProperties = noPas;
                            options.CustomRoomPropertiesForLobby = new string[] { "Public" };


                            PhotonNetwork.CreateRoom(roomNameCreate.text, options, default);
                            marbles.isBack = true;
                            create = true;
                        }
                    }
                }
                else
                {
                    創建面板.SetActive(false);

                    isPublic = true;
                    if (noPas.ContainsKey("Public"))
                    {
                        noPas.Remove("Public");
                        noPas.Add("Public", isPublic);
                        PhotonNetwork.LocalPlayer.SetCustomProperties(noPas, null);
                    }
                    else
                    {
                        noPas.Add("Public", isPublic);
                        PhotonNetwork.LocalPlayer.SetCustomProperties(noPas, null);
                    }
                    
                    roomText.text = roomNameCreate.text;
                    int i = int.Parse(maxplayer.transform.GetChild(0).GetComponent<Text>().text);

                    Maxplayer = (byte)i;
                    RoomOptions options = new RoomOptions();
                    options.CustomRoomProperties = noPas;
                    options.CustomRoomPropertiesForLobby = new string[] { "Public" };



                    PhotonNetwork.CreateRoom(roomNameCreate.text, options, default);
                    marbles.isBack = true;
                    create = true;
                }

            }
            else
            {
                if (roomNameCreate.text.Length < 1 || roomNameCreate.text.Length > 12)
                    return;

                if (RoomListManager.roomID.Count > 0)
                {
                    foreach (var room in RoomListManager.roomID)
                    {
                        if (room.Name == roomNameCreate.text)
                        {
                            PromptUI.PromptPut[4].SetActive(true);
                            return;
                        }
                        else
                        {
                            創建面板.SetActive(false);
                            roomText.text = roomNameCreate.text;
                            int i = int.Parse(maxplayer.transform.GetChild(0).GetComponent<Text>().text);

                            Maxplayer = (byte)i;
                            RoomOptions options = new RoomOptions { MaxPlayers = Maxplayer, IsVisible = false };
                            PhotonNetwork.CreateRoom(roomNameCreate.text, options, default);
                            marbles.isBack = true;
                            create = true;
                        }
                    }
                }
                else
                {
                    創建面板.SetActive(false);
                    roomText.text = roomNameCreate.text;
                    int i = int.Parse(maxplayer.transform.GetChild(0).GetComponent<Text>().text);
                    Maxplayer = (byte)i;
                    RoomOptions options = new RoomOptions { MaxPlayers = Maxplayer, IsVisible = false };                    
                    PhotonNetwork.CreateRoom(roomNameCreate.text, options, default);
                    marbles.isBack = true;
                    create = true;
                }
            }
        }
        //if (pass != null)
        //{            
        //    roomText.text = roomNameCreate.text;
        //    int i = int.Parse(maxplayer.transform.GetChild(0).GetComponent<Text>().text);
        //    Maxplayer = (byte)i;

        //    RoomOptions options = new RoomOptions();
        //    options.MaxPlayers = Maxplayer;
        //    options.CustomRoomProperties.Add("Pass", pass);
        //    options.CustomRoomPropertiesForLobby = new string[1] { "Pass" };

        //    PhotonNetwork.CreateRoom(roomNameCreate.text, options, default);
        //    marbles.isBack = true;
        //    create = true;                        
        //}
        if (rmnm.ContainsKey("name"))    
        {
            rmnm.Remove("name");
            rmnm.Add("name", roomText.text);
            PhotonNetwork.LocalPlayer.SetCustomProperties(rmnm, null);
        }
        else
        {
            rmnm.Add("name", roomText.text);
            PhotonNetwork.LocalPlayer.SetCustomProperties(rmnm, null);
        }
    }    

    public override void OnJoinedRoom()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            foreach (var player in PhotonNetwork.PlayerList)
            {
                if (player.IsMasterClient)
                {
                    roomText.text = player.CustomProperties["name"].ToString();
                }
            }
        }

        marbles.isBack = true;
        Debug.LogWarning(PhotonNetwork.NickName + "加入房間");
        index = SceneManager.GetActiveScene().buildIndex;

        foreach (Player p in PhotonNetwork.PlayerList)
        {
            GameObject pl = Instantiate(playerNamePrefab, gridLayout.position, Quaternion.identity);
            pl.transform.GetChild(0).GetComponent<Text>().text = p.NickName;
            pl.transform.SetParent(gridLayout);
            // Add a button for each player in the room.
            // You can use p.NickName to access the player's nick name.
        }
        if (random)
        {
            photonView.RPC("Distinguish", RpcTarget.AllBuffered);
        }

        //photonView.RPC("PlayerInRoom", RpcTarget.AllBuffered);
        //if (index == 1 && check)
        //{
        //    chat.GetComponent<Chat>().enabled = true;
        //    check = false;
        //}
        //if (index == 0)
        //{
        //    PhotonNetwork.LoadLevel(1);
        //}        
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)    //當進入房間的人數(扣除第一人) >=1 開始按鈕浮現
    {
        GameObject pl = Instantiate(playerNamePrefab, gridLayout.position, Quaternion.identity);
        pl.transform.GetChild(0).GetComponent<Text>().text = newPlayer.NickName;
        pl.transform.SetParent(gridLayout);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        for (int i = 0; i < gridLayout.childCount; i++)
        {
            if (gridLayout.GetChild(i).GetChild(0).GetComponent<Text>().text == otherPlayer.NickName)
            {
                Destroy(gridLayout.GetChild(i).gameObject);
            }
        }
    }
    public void EnterGame()
    {     
        PhotonNetwork.ConnectUsingSettings();

        if (PlayerPrefs.GetString("Name").Length != 0)
        {
            playerName.text = PlayerPrefs.GetString("Name");
        }
    }
    public void PlayButton()
    {
        if (playerName.text.Length < 1)
            return;

        PhotonNetwork.NickName = playerName.text;

        save.savePlayerName = playerName.text;

        for (int i = 0; i < ButtonControl.PlayerNameClose.Length; i++)
        {
            ButtonControl.PlayerNameClose[i].SetActive(false);
        }
    }
    //public void GameStart()
    //{        
    //    if (PhotonNetwork.IsMasterClient)
    //    {
    //        S_L.Check += 1;
    //        PhotonNetwork.LoadLevel(1);
    //        photonView.RPC("Friend", RpcTarget.All);
    //    }
    //}

    public void 我就自嗨()
    {
        S_L.Judgment = 1;
        SceneManager.LoadScene(1);
        S_L.Check += 1;
    }

    public void RandomRoom()
    {
        S_L.Check += 1;
        random = true;
        marbles.isBack = true;
        PhotonNetwork.JoinRandomRoom(noPas, 0);
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //noRoomLook.SetActive(true);
        CreateButton();
    }    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            PlayerPrefs.DeleteKey("Name");
        }
        //index = SceneManager.GetActiveScene().buildIndex;
        //if (index == 1 && check) 
        //{
        //    PhotonNetwork.ConnectUsingSettings();
        //    check = false;
        //}


        if (PhotonNetwork.PlayerList.Length == 8 && check1 && random && ran == 8)  
        {
            photonView.RPC("PlayerInRoom_1", RpcTarget.All);
            check1 = false;            
        }
        if (PhotonNetwork.PlayerList.Length == 8 && !check1 && !PromptUI.PromptPut[1].activeSelf && check2 && random && ran == 8) 
        {
            抽大圖.SetActive(true);
            check2 = false;
            //PhotonNetwork.LoadLevel(1);
        }
        if (ran != Maxplayer && mapCheck != 0 && check3) 
        {
            photonView.RPC("PlayerInRoom_2", RpcTarget.All);
            check3 = false;
        }
        if (create && !PromptUI.PromptPut[2].activeSelf && PhotonNetwork.IsMasterClient && !check3 && check4)
        {
            Debug.Log(mapSelect);
            PhotonNetwork.LoadLevel(mapSelect);
            check4 = false;
        }
        if (create && PhotonNetwork.IsMasterClient) 
        {
            slot.enabled = true;
            StarT.gameObject.SetActive(true);
        }
    }
    [PunRPC]
    public void PlayerInRoom_1()
    {        
        PromptUI.PromptPut[1].SetActive(true);    //抽圖面板倒數開啟      
    }
    [PunRPC]
    public void PlayerInRoom_2()
    {
        PromptUI.PromptPut[2].SetActive(true);    //入場面板倒數開啟        
    }
    [PunRPC]
    public void Distinguish()
    {
        ran += 1;
    }
    [PunRPC]
    public void Map()
    {
        mapCheck = 1;
    }
    public void ButtonCheckMap()
    {
        photonView.RPC("Map", RpcTarget.All);
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
    public override void OnJoinedLobby()
    {
        //nameText.text = PhotonNetwork.NickName;
    }
    public void RoomList()
    {        
        PhotonNetwork.JoinRoom(roomName);        
        PromptUI.PromptPut[3].SetActive(false);        
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        create = true;
    }
}
