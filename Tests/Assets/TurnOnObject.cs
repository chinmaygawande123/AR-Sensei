using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnObject : MonoBehaviour
{
	public GameObject ObjectToEnabled;
	
	public void EnableObject()
	{
		ObjectToEnabled.SetActive(true);
	}
}
