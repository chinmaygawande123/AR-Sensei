using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitchingScript : MonoBehaviour
{
	
	public Transform MainObject;
	public Transform UpObject;
	
	public int ScrollUpNow;
	
	public GameObject AstronomySelection;
	public GameObject BiologySelection;
	
	public GameObject MainLoading;
	
	public MenuManager MenuManager;
	
	public void ScrollUp()
	{
		ScrollUpNow = 1;
	}
	
	// Update is called every frame, if the MonoBehaviour is enabled.
	protected void Update()
	{
		if(ScrollUpNow == 1)
		{
			MainObject.position = Vector3.Lerp(MainObject.position, UpObject.position, 0.1f);
		}
	}
	
	public void ChooseSubject(string Subject)
	{
		
		if(Subject == "Astronomy")
		{
			AstronomySelection.SetActive(true);
			BiologySelection.SetActive(false);
		}
		
		else if(Subject == "Biology")
		{
			AstronomySelection.SetActive(false);
			BiologySelection.SetActive(true);
		}
		
		StartCoroutine(WaitAndSendExit(Subject));
				
	}
	
	IEnumerator WaitAndSendExit(string Subject)
	{
		yield return new WaitForEndOfFrame();
		MenuManager.SendEndCall(Subject);
	}

	
}
