using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BiologyHandler : MonoBehaviour
{
	
	public MenuManager MenuManager;
	
	public Image[] AllImages;
	
	public TextMeshProUGUI BrainFirstText;
	
	public TextMeshProUGUI DigestFirstText;
	
    // Start is called before the first frame update
    void Start()
    {
	    MenuManager = FindObjectOfType<MenuManager>();
    }

    // Update is called once per frame
    void Update()
	{
		print(PlayerPrefs.GetString("Indivisual"));
    	
	    if(PlayerPrefs.GetString("Indivisual") != "Teacher")
	    {
	    	for(int i = 0; i < AllImages.Length; i++)
	    	{
	    		AllImages[i].enabled = false;
	    	}
	    	
	    	BrainFirstText.text = "Wait for Teacher to intereact.";
	    	DigestFirstText.text = "Wait for Teacher to intereact.";
	    }
    }
}
