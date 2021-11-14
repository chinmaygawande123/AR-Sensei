using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class JoinFrom : MonoBehaviour
{
	public int JoinID;
	
	public MenuManager MenuManager;
	
	public void JoinMeeting()
	{
		MenuManager.OnJoinRoomBtn(JoinID + "");
	}
}
