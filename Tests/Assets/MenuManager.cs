using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using agora_gaming_rtc;
using agora_utilities;
public class MenuManager: MonoBehaviourPunCallbacks {
	
	public string HostName;
	
	public List<string> AllParticipants;
	
	public List<string> ParticpantsWithoutHost;
	
	public List<string> LeftParticipants;
	
	public List<GameObject> ListOfVideoRaws;
	
	public MainNextSlider UIHandler;
	
	public GameObject HostAlertExit;
	
	public GameObject CreateRoomFailedExit;
	
	public GameObject JoinRoomFailedExit;
	
	public GameObject AudioConnectionObject;
	
	public HelloUnity3D SoundManager;
	
	public TestHome VideoManager;
	
	private void Start() 
	{
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
	
	public override void OnConnectedToMaster() 
	{
		print("Connected To The Master Server!");		
		UIHandler.MainLoading.SetActive(false);
	}
	
	
	public string RoomID;
	
	public void OnCreateRoomBtn() 
	{
		PhotonNetwork.NickName = PlayerPrefs.GetString("First Name") + " " + PlayerPrefs.GetString("Second Name");
		int ID1 = Random.Range(111111,999999);
		RoomID = "" + ID1;
		NetworkManager.instance.CreateRoom(RoomID);
	}
	
	public void OnJoinRoomBtn(string RoomName) 
	{
		PhotonNetwork.NickName = PlayerPrefs.GetString("First Name") + " " + PlayerPrefs.GetString("Second Name");
		NetworkManager.instance.JoinRoom(RoomName);
		UIHandler.MainLoading.SetActive(true);
		Invoke("IfJoinFails", 30);
	}
	
	void IfJoinFails()
	{
		JoinRoomFailedExit.SetActive(true);
		UIHandler.MainLoading.SetActive(false);
	}

	
	public override void OnCreatedRoom()
	{
		HostName = PhotonNetwork.NickName;
		UIHandler.Changer = 1;
		UIHandler.RoomName = RoomID;
		SoundManager.PChannelName = RoomID;
		SoundManager.JoinChannel();
		Invoke("AudioConnectionNotification", 1);
	}
	
	public override void OnJoinedRoom() 
	{
		if(PhotonNetwork.NickName != HostName)
		{
			UIHandler.Changer = 3;
		}
		
		UIHandler.MainLoading.SetActive(false);	
		CancelInvoke("IfJoinFails");
		print("Joined the Room");
		if(UIHandler.CommandForString)
		{
			SoundManager.PChannelName = UIHandler.tempID;
		}
		else
		{
			SoundManager.PChannelName = UIHandler.ClassID.text;
		}
		
		SoundManager.JoinChannel();
		photonView.RPC("HostRefresh", RpcTarget.All);
		photonView.RPC("AddPaticipants", RpcTarget.All, PhotonNetwork.NickName);
		Invoke("AudioConnectionNotification", 1);
	}
	
	public override void OnCreateRoomFailed (short returnCode, string message)
	{
		CreateRoomFailedExit.SetActive(true);
		UIHandler.MainLoading.SetActive(false);
	}
	
	public override void OnJoinRoomFailed (short returnCode, string message)
	{
		CancelInvoke("IfJoinFails");
		print("Join Failed");
		JoinRoomFailedExit.SetActive(true);
		UIHandler.MainLoading.SetActive(false);
	}
	
	[PunRPC]
	void HostRefresh()
	{
		if(PhotonNetwork.NickName == HostName)
		{
			photonView.RPC("AddHost", RpcTarget.All, PhotonNetwork.NickName);
			
			for(int i = 0; i < AllParticipants.Count; i++)
			{
				photonView.RPC("AddPaticipants", RpcTarget.All, AllParticipants[i]);
			}
		}
	}
	
	[PunRPC]
	void AddPaticipants(string ParticipantName)
	{
		
		print("Was here with : " + ParticipantName);
		
		if(!AllParticipants.Contains(ParticipantName))
		{
			AllParticipants.Add(ParticipantName);
			
			if(ParticpantsWithoutHost.Contains(ParticipantName))
			{
				ParticpantsWithoutHost.Remove(ParticipantName);
			}
			
			ParticpantsWithoutHost.Add(ParticipantName);
		}
	}
	
	[PunRPC]
	void AddHost(string InputName)
	{
		HostName = InputName;
	}

	
	public override void OnPlayerLeftRoom(Player otherPlayer) 
	{
		
		if(HostName == otherPlayer.NickName)
		{
			photonView.RPC("HostExited", RpcTarget.All);
		}
		
		print(otherPlayer.NickName + " left the room.");
		
		AllParticipants.Remove(otherPlayer.NickName);
	}
	
	public override void OnLeftRoom()
	{
		
		if(isVideoToo == 1)
		{
			VideoManager.onLeaveButtonClicked();
		}
		
		else
		{
			SoundManager.IfClickedGo = 0;
			SoundManager.LeaveChannel();
		}
		
		HostName = "";
		VideoManager.PChannelName = "";
		SoundManager.PChannelName = "";
		
		if(PlayerPrefs.GetString("Indivisual") == "Teacher")
		{
			if(UIHandler.Changer == 4)
			{			
				for(int i = 0; i < VideoManager.ParticipantsList.Count; i++)
				{
					if(VideoManager.ParticipantsList[i].GetComponent<VideoSurface>())
					{
						Destroy(VideoManager.ParticipantsList[i].GetComponent<VideoSurface>());
					}
				}
				
				UIHandler.Slides[0].position = UIHandler.Back.position;
				UIHandler.Slides[2].position = UIHandler.Back.position;
				UIHandler.Changer = -4;
			}
			
			else
			{
				UIHandler.Changer = -1;
			}
		}
		
		else
		{
			if(UIHandler.Changer == 4)
			{
				for(int i = 0; i < VideoManager.ParticipantsList.Count; i++)
				{
					if(VideoManager.ParticipantsList[i].GetComponent<VideoSurface>())
					{
						Destroy(VideoManager.ParticipantsList[i].GetComponent<VideoSurface>());
					}
				}
				
				UIHandler.Slides[0].position = UIHandler.Back.position;
				UIHandler.Slides[2].position = UIHandler.Back.position;
				UIHandler.Changer = -4;
			}
			
			else
			{
				UIHandler.Changer = -3;
			}
		}
		
		print("Left");
		
		AllParticipants.Clear();
		ParticpantsWithoutHost.Clear();
		
		isVideoToo = 0;

	}
	
	
	[PunRPC]
	void HostExited()
	{
		OnLeaveLobbyBtn();
		HostAlertExit.SetActive(true);
		SoundManager.IfClickedGo = 0;
	}
	
	
	public int isVideoToo;
	
	public string SubjectChoosen;
	
	public void OnLeaveLobbyBtn() 
	{
		PhotonNetwork.LeaveRoom();
	}
	
	public void LeaveVideoCall()
	{
		VideoManager.onLeaveButtonClicked();
	}
	
	public void OnLeaveWithVideo()
	{
		isVideoToo = 1;
		OnLeaveLobbyBtn();
	}
	
	public void OnStartGameBtn() 
	{
		NetworkManager.instance.photonView.RPC("ChangeScene", RpcTarget.All, SubjectChoosen);
	}
	
	public void MoveToVideoCallBtn()
	{
		UIHandler.MainLoading.SetActive(true);
		int RandomVideoNumber = Random.Range(111111,999999);
		string VideoID = RandomVideoNumber+"";
		photonView.RPC("MoveToVideoCallServer", RpcTarget.All, VideoID);
		//VideoManager.onJoinButtonClicked();
	}
	
	[PunRPC]
	void MoveToVideoCallServer(string VideoCallID)
	{
		UIHandler.MainLoading.SetActive(true);
		SoundManager.IfClickedGo = 1;
		VideoManager.PChannelName = VideoCallID;
		VideoManager.ParticipantsList = new List<GameObject>(ListOfVideoRaws);
		SoundManager.LeaveChannel();
	}
	
	public void SendEndCall(string Sub)
	{
		photonView.RPC("SendNowEndCall", RpcTarget.All, Sub);
	}
	
	[PunRPC]
	void SendNowEndCall(string Subject)
	{
		SubjectChoosen = Subject;
		UIHandler.MainLoading.SetActive(true);
		StartCoroutine(WaitToStartGame());
	}
	
	public GameObject MainCanvasObj;
	
	IEnumerator WaitToStartGame()
	{
		yield return new WaitForEndOfFrame();
		MainCanvasObj.SetActive(false);
		OnStartGameBtn();
	}
	
	public void AfterVideoJoinVoice()
	{
		if(PhotonNetwork.NickName == HostName)
		{
			int SoundChannelName = Random.Range(111111,999999);
			photonView.RPC("AfterVideoJoinVoiceNow", RpcTarget.All, SoundChannelName);
		}	
	}
	
	[PunRPC]
	void AfterVideoJoinVoiceNow(int SoundChannelName)
	{
		SoundManager.PChannelName = "" + SoundChannelName;
		SoundManager.JoinChannel();
	}
	
	IEnumerator StartARorVRNow()
	{
		yield return new WaitForEndOfFrame();
		OnStartGameBtn();
	}
	
	void AudioConnectionNotification()
	{
		AudioConnectionObject.SetActive(true);
	}
	
	[Header("NameBoxes")]
	
	
	public TextMeshProUGUI HHostBox;
	
	public TextMeshProUGUI[] HPaticipantsText;
	
	public TextMeshProUGUI LHostBox;
	
	public TextMeshProUGUI[] LPaticipantsText;
	
	public GameObject AddPaticipantsBlocker;
	
	public GameObject GoNextBlocker;
	
	// Update is called every frame, if the MonoBehaviour is enabled.
	protected void Update()
	{
		if(SceneManager.GetActiveScene().name == "MainUI")
		{
			// All This Stuff is Of First Scene
		
			if(AllParticipants.Count == 1)
			{
				GoNextBlocker.SetActive(true);
			}
		
			if (AllParticipants.Count != 1 && AllParticipants.Count != 0)
			{
				GoNextBlocker.SetActive(false);
			}
		

		
			if(AllParticipants.Count == 5)
			{
				AddPaticipantsBlocker.SetActive(true);
			}
		
			if (AllParticipants.Count != 5)
			{
				AddPaticipantsBlocker.SetActive(false);
			}
		
		
			for(int i = 0; i < AllParticipants.Count; i++)
			{
				if(AllParticipants[i] == HostName)
				{
					HHostBox.text = AllParticipants[i] + " (Host)";
					LHostBox.text = AllParticipants[i] + " (Host)";
				}
			}			
		
			if(ParticpantsWithoutHost.Contains(HostName))
			{
				ParticpantsWithoutHost.Remove(HostName);
			}
		
			for(int i = 0; i < ParticpantsWithoutHost.Count; i++)
			{
				HPaticipantsText[i].transform.parent.gameObject.SetActive(true);
				HPaticipantsText[i].text = ParticpantsWithoutHost[i];
				LPaticipantsText[i].transform.parent.gameObject.SetActive(true);
				LPaticipantsText[i].text = ParticpantsWithoutHost[i];
			}
		
			for(int i = 0; i < HPaticipantsText.Length; i++)
			{
				if(i < AllParticipants.Count - 1)
				{
					HPaticipantsText[i].transform.parent.gameObject.SetActive(true);
					LPaticipantsText[i].transform.parent.gameObject.SetActive(true);
				}
			
				else
				{
					HPaticipantsText[i].transform.parent.gameObject.SetActive(false);
					LPaticipantsText[i].transform.parent.gameObject.SetActive(false);
				}
			}
		}
	}
}