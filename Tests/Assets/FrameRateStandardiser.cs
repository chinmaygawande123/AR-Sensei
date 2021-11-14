using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateStandardiser : MonoBehaviour
{
	// Awake is called when the script instance is being loaded.
	protected void Awake()
	{
		Application.targetFrameRate = 60;
	}
}
