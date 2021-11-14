using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SavedStringInput : MonoBehaviour
{
    
	public TextMeshProUGUI NameText;

    // Update is called once per frame
    void Update()
	{
	    NameText.text = PlayerPrefs.GetString("First Name") + " <sprite=0>";
    }
}
