using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

public class UIHandler : MonoBehaviourPunCallbacks
{
   
	public Image SwitchTop;
	
	public GameObject DigestiveButton;
	
	public GameObject BrainsButton;
	
	public Sprite Brains;
	
	public Sprite DigestiveSystem;
	
	public int PopUpController;
	
	public Transform PopUp;
	
	public Transform Up;
	
	public Transform Down;
	
	public string[] ArrayInformation;
	
	public AudioClip[] BrainsClip;
	
	public AudioSource AudioSource;
	
	public GameObject DigestiveSystemCanvas;
	
	public GameObject BrainsCanvas;
	
	public GameObject Brain;
	
	public GameObject DigestiveSystemObj;
	
	public void SwitchToDigestive()
	{
		photonView.RPC("ServerSwitchDigestive", RpcTarget.All);
	}
	
	[PunRPC]
	void ServerSwitchDigestive()
	{
		print("Switch Happened!");
		DigestiveSystemCanvas.SetActive(true);
		BrainsCanvas.SetActive(false);
		Brain.SetActive(false);
		DigestiveSystemObj.SetActive(true);
	}
   
	public void SwitchToBranis()
	{
		photonView.RPC("ServerSwitchBrains", RpcTarget.All);
	}
	
	[PunRPC]
	void ServerSwitchBrains()
	{
		DigestiveSystemCanvas.SetActive(false);
		BrainsCanvas.SetActive(true);	
		Brain.SetActive(true);
		DigestiveSystemObj.SetActive(false);
	}
	
	public GameObject[] Switchers;
		
	public void ChangeInformation(int PositionInt)
	{
		photonView.RPC("ServerChangeInformation", RpcTarget.All, PositionInt);
	}
	
	[PunRPC]
	void ServerChangeInformation(int PositionInt)
	{
		PopUpController = PositionInt;
	}
	
	public BrainChanger BrainChanger;
	
	public TestHome VideoManager;
	
	public void UpClickedToPlay()
	{
		photonView.RPC("ServerUpClickedToPlay", RpcTarget.All);
	}
	
	[PunRPC]
	void ServerUpClickedToPlay()
	{
		VideoManager = FindObjectOfType<TestHome>();
		VideoManager.MuteMe();
		StartCoroutine(FirstMuteAndThenPlay());
	}
	
	int ContinuosVolumeSave;
	
	IEnumerator FirstMuteAndThenPlay()
	{
		yield return new WaitForEndOfFrame();
		AudioSource.clip = BrainsClip[BrainChanger.PartChanger];
		ContinuosVolumeSave = 1;
		AudioSource.Play();
		InvokeRepeating("CheckAfterMuting", 0, 0.1f);
	}
	
	void CheckAfterMuting()
	{
		if(!AudioSource.isPlaying)
		{
			VideoManager.ResumeMe();
			CancelInvoke("CheckAfterMuting");
		}
	}
	
	public GameObject DownButton;
	
	#if UNTIY_ANDROID
	
	static int STREAMMUSIC;
	static int FLAGSHOWUI = 1;
 
	private static AndroidJavaObject audioManager;
 
	private static AndroidJavaObject deviceAudio
	{
		get
		{
			if (audioManager == null)
			{
				AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
				AndroidJavaObject currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
				AndroidJavaObject context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
				AndroidJavaClass audioManagerClass = new AndroidJavaClass("android.media.AudioManager");
				AndroidJavaClass contextClass = new AndroidJavaClass("android.content.Context");
 
				STREAMMUSIC = audioManagerClass.GetStatic<int>("STREAM_MUSIC");
				string Context_AUDIO_SERVICE = contextClass.GetStatic<string>("AUDIO_SERVICE");
 
				audioManager = context.Call<AndroidJavaObject>("getSystemService", Context_AUDIO_SERVICE);
 
				if (audioManager != null)
					Debug.Log("[AndroidNativeVolumeService] Android Audio Manager successfully set up");
				else
					Debug.Log("[AndroidNativeVolumeService] Could not read Audio Manager");
			}
			return audioManager;
		}
 
	}
 
	private static int GetDeviceMaxVolume()
	{
		return deviceAudio.Call<int>("getStreamMaxVolume", STREAMMUSIC);
	}
 
	public float GetSystemVolume()
	{
		int deviceVolume = deviceAudio.Call<int>("getStreamVolume", STREAMMUSIC);
		float scaledVolume = (float)(deviceVolume / (float)GetDeviceMaxVolume());
 
		return scaledVolume;
	}
 
	public void SetSystemVolume(float volumeValue)
	{
		int scaledVolume = (int)(volumeValue * (float)GetDeviceMaxVolume());
		deviceAudio.Call("setStreamVolume", STREAMMUSIC, scaledVolume, FLAGSHOWUI);
	}
	
	#endif
	
	// Update is called every frame, if the MonoBehaviour is enabled.
	protected void Update()
	{
		
		AudioSource.volume = 1;
		
		if(ContinuosVolumeSave == 1)
		{
			#if UNTIY_ANDROID
			print("Device Volume is : " + GetSystemVolume());
			print("Device Max Volume is : " + GetDeviceMaxVolume());
			print("Audio Source Volume is : " + AudioSource.volume);
			SetSystemVolume(15);
			ContinuosVolumeSave = 0;
			#endif
		}
		
		if(AudioSource.isPlaying)
		{
			DownButton.SetActive(false);
		}
		
		else
		{
			DownButton.SetActive(true);
		}
		
		if(PopUpController == 0)
		{
			PopUp.position = Vector3.Lerp(PopUp.position, Down.position, 0.1f);
			SwitcherStatus(true);
		}
		
		else
		{
			PopUp.position = Vector3.Lerp(PopUp.position, Up.position, 0.1f);
			SwitcherStatus(false);
		}
	}
	
	void SwitcherStatus(bool Satus)
	{
		for(int i = 0; i < Switchers.Length; i++)
		{
			Switchers[i].SetActive(Satus);
		}
	}
}
