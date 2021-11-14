using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScriptBiology : MonoBehaviour
{
	
	public ARTapToPlaceObject TapHandler;
	
	public TextMeshProUGUI InstructionText;
	
	public GameObject HologramBrain;
	
	public void StartAR()
	{
		TapHandler.enabled = true;
		HologramBrain.SetActive(true);
		InstructionText.text = "Move your mobile phone to scan the floor.";
	}
}
