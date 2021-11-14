using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObject : MonoBehaviour
{
	public GameObject ObjectToDisable;
	
	public void DisableNow()
	{
		ObjectToDisable.SetActive(false);
	}
	
	public void QuitNow()
	{
		Application.Quit();	
	}
	
}
