using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckID : MonoBehaviour
{
   
	public GameObject StudentObjects;
	
	public GameObject SignUpCanvas;
	
	public GameObject EndCallButton;
	
	public GameObject SceneSwitchButton;
	
	public bool isTeacher;
	
	// Awake is called when the script instance is being loaded.
	protected void Awake()
	{
		Application.targetFrameRate = 60;
				
		#if UNITY_EDITOR
		
		if(isTeacher == true)
		{
			PlayerPrefs.SetString("Indivisual", "Teacher");
		}
		
		else
		{
			PlayerPrefs.SetString("Indivisual", "Student");
		}
		
		print(PlayerPrefs.GetString("Indivisual"));
		
		#endif		
							
				
		if(PlayerPrefs.GetInt("Verification") == 1)
		{
			SignUpCanvas.gameObject.SetActive(false);
		}
	    
		else
		{
			SignUpCanvas.gameObject.SetActive(true);
		}
	}
	
	void Update()
	{
		
		#if !UNITY_EDITOR
		
		if(PlayerPrefs.GetString("Indivisual") =="Student")
		{
			StudentObjects.SetActive(true);
			EndCallButton.SetActive(false);
			SceneSwitchButton.SetActive(false);
		}
		
		else
		{
			StudentObjects.SetActive(false);
			EndCallButton.SetActive(true);
			SceneSwitchButton.SetActive(true);
		}
		
		#endif
	}
   
}
