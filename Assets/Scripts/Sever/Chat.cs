using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Chat;
using Photon.Pun;


public class Chat : MonoBehaviourPunCallbacks, IChatClientListener
{

	private ChatClient chatClient;
	public string userName;
	public string currentChannelName;

	public InputField inputField;
	public Text outputText;
	public Text playerName;
	public Text roomName;
	public bool canLook;
	bool one;
	
	void Start()
	{
		if (!PhotonNetwork.IsMasterClient)
			return;
		one = true;
		canLook = false;
		Application.runInBackground = true;
		currentChannelName = PhotonNetwork.CurrentRoom.Name;
		userName = PhotonNetwork.LocalPlayer.NickName.ToString();
		chatClient = new ChatClient(this);
		chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, "1.0", new AuthenticationValues(userName));
	}
	public void AddLine(string lineString)
	{		
		outputText.text += "\n " + lineString ;
	}

	public void OnApplicationQuit()
	{
		if (chatClient != null)
		{
			chatClient.Disconnect();
		}
	}

	public void DebugReturn(ExitGames.Client.Photon.DebugLevel level, string message)
	{
		if (level == ExitGames.Client.Photon.DebugLevel.ERROR)
		{
			Debug.LogError(message);
		}
		else if (level == ExitGames.Client.Photon.DebugLevel.WARNING)
		{
			Debug.LogWarning(message);
		}
		else
		{
			Debug.Log(message);
		}
	}

	public override void OnConnected()
	{
		AddLine("已連線.");
		Welcome();
	}

	public void OnDisconnected()
	{
		AddLine("已斷線.");
		//chatClient.PublishMessage(currentChannelName, "<color=red>" + "(已退出房間)" + "</color>");
		//canLook = false;  沒作用
	}

	public void OnChatStateChange(ChatState state)
	{
		Debug.Log("OnChatStateChange = " + state);
	}

	public void OnSubscribed(string[] channels, bool[] results)
	{
		//AddLine(string.Format("채널 입장 ({0})", string.Join(",", channels)));	
	}

	public void OnUnsubscribed(string[] channels)
	{
		//AddLine(string.Format("채널 퇴장 ({0})", string.Join(",", channels)));		
	}
	
	public void OnGetMessages(string channelName, string[] senders, object[] messages)
	{
		if (one)
		{
			messages = new object[0];
			one = false;
		}
		if (canLook)
		{
			//for (int i = 0; i < messages.Length; i++)
			//{
			//	AddLine(string.Format("{0} : {1}", senders[i], messages[i].ToString()));
			//}    適合做私訊

			for (int i = 0; i < messages.Length; i++)
			{
				AddLine(string.Format("{0} : {1}", senders[i], messages[i].ToString()));
			}
		}
	}

	public void OnPrivateMessage(string sender, object message, string channelName)
	{
		Debug.Log("OnPrivateMessage : " + message);
	}

	public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
	{
		Debug.Log("status : " + string.Format("{0} is {1}, Msg : {2} ", user, status, message));
	}
	void Update()
	{		
		chatClient.Service();
	}
	public void Input_OnEndEdit(string text)
	{
		if (chatClient.State == ChatState.ConnectedToFrontEnd)
		{
			chatClient.PublishMessage(currentChannelName, inputField.text);
			inputField.text ="";
		}
	}
	public void OnUserSubscribed(string channel, string user)
	{
		throw new System.NotImplementedException();
	}

	public void OnUserUnsubscribed(string channel, string user)
	{
		throw new System.NotImplementedException();
	}
	public void Welcome()
	{
		chatClient.Subscribe(new string[] { currentChannelName }, 10); 		
		chatClient.PublishMessage(currentChannelName, "<color=red>" + "(已加入房間)" + "</color>");
		canLook = true;
	}
}
