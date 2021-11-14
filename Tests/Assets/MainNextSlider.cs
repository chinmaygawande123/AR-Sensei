using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainNextSlider : MonoBehaviour
{
	public int Changer;
	
	public Rester EmailManager;
	
	public Transform[] Slides;
	
	public Transform Centre;
	
	public Transform Back;
		
	public Transform Down;
	
	public TMP_InputField EmailField; 

	public float speedSlide;
	
	public MenuManager MenuManager;
	
	public GameObject MainLoading;
	
	public string RoomName;
	
	public TextMeshProUGUI RoomNameText;
	
	public TextMeshProUGUI JoinRoomText;
	
	public TMP_InputField ClassID;

	public void ChangeSlide(int SlideInt)
	{
		Changer = SlideInt;
	}
	
	public void SendAlert()
	{
		EmailManager.SendAlert(EmailField.text);
	}
	
	// Awake is called when the script instance is being loaded.
	protected void Awake()
	{
		Application.targetFrameRate = 60;
		MainLoading.SetActive(true);
	}
	
	public void CreateRoom()
	{
		MenuManager.OnCreateRoomBtn();
		MainLoading.SetActive(true);
	}
	
	public bool CommandForString;
	
	public string tempID;
	
	public void JoinClass()
	{
		if(CommandForString)
		{
			MenuManager.OnJoinRoomBtn(tempID);
		}
		
		else
		{
			MenuManager.OnJoinRoomBtn(ClassID.text);
		}
		
		MainLoading.SetActive(true);
	}

    // Update is called once per frame
    void Update()
	{
    	
		RoomNameText.text = "Room ID : " + RoomName;
		
		JoinRoomText.text = "Room ID : " + RoomName;
    	
	    if(Changer == 1)
	    {
	    	GetToCentre(Slides[0]);
	    	MainLoading.SetActive(false);
	    }
	    
	    if(Changer == -1)
	    {
	    	GetToBack(Slides[0]);
	    }
	    
	    if(Changer == 2)
	    {
	    	GetToCentre(Slides[1]);
	    }
	    
	    if(Changer == -2)
	    {
	    	GetToBack(Slides[1]);
	    }
	    
		if(Changer == 3)
		{
			GetToCentre(Slides[2]);
		}
		
		if(Changer == -3)
		{
			GetToBack(Slides[2]);
		}
		
		if(Changer == 4)
		{
			GetToCentre(Slides[3]);
		}
		
		if(Changer == -4)
		{
			GetToBack(Slides[3]);
		}
		
		if(Changer == 5)
		{
			GetToCentre(Slides[4]);
		}
	    
		if(Changer == -5)
		{
			GetToBack(Slides[4]);
		}
    }
    
	void GetToCentre(Transform Object)
	{
		Object.position = Vector3.Lerp(Object.position, Centre.position, speedSlide);
	}
	
	void GetToBack(Transform Object)
	{
		Object.position = Vector3.Lerp(Object.position, Back.position, speedSlide);
	}
	
	void GetDown(Transform Object)
	{
		Object.position = Vector3.Lerp(Object.position, Down.position, speedSlide);
	}
}
