using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NextSlider : MonoBehaviour
{
	
	public int Changer;
	
	public Rester VerificationaManager;
	
	public Transform MainObject;
	
	public GameObject OTPCanvas;
		
	public Transform[] Slides;
	
	public Transform Centre;
	
	public Transform Back;
		
	public Transform Down;
	
	public GameObject Teacher;
	
	public GameObject Student;
	
	public GameObject BlockNext1;
	
	public GameObject BlockNext2;
	
	public GameObject BlockNext3;
	
	public GameObject BlockNext4;
	
	public GameObject BlockNext5;
	
	public float speedSlide;
	
	public TMP_InputField[] NamesInputField;
	
	public TMP_InputField NumberField;
	
	public TMP_InputField EmailField;
	
	public TMP_InputField OTPField;
	
	public TextMeshProUGUI EmailShowText;
	
	public TextMeshProUGUI ErrorText;
   
	public void ChangeSlide(int SlideInt)
	{
		Changer = SlideInt;
	}
	
	public void SelectStudentTeacher(int id)
	{
		BlockNext1.SetActive(false);
		
		if(id == 0)
		{
			Teacher.SetActive(true);
			Student.SetActive(false);
			PlayerPrefs.SetString("Indivisual", "Teacher");
		}
		
		if(id == 1)
		{
			Teacher.SetActive(false);
			Student.SetActive(true);
			PlayerPrefs.SetString("Indivisual", "Student");
		}
	}
	
	public void SaveName()
	{
		PlayerPrefs.SetString("First Name",NamesInputField[0].text);	
		PlayerPrefs.SetString("Second Name",NamesInputField[1].text);	
		Changer = 3;
	}
	
	public void SaveNumber()
	{
		PlayerPrefs.SetString("Number",NumberField.text);	
		Changer = 4;
	}
	
	int RandomSix;
	
	public void SendEmailNotification()
	{
	    RandomSix = Random.Range(111111,999999);
		
		VerificationaManager.SendOTP(RandomSix, EmailField.text);
		
		EmailShowText.text = EmailField.text;
		
		ErrorText.gameObject.SetActive(false);
	}
	
	public void BackToCheck()
	{
		Changer = -6;
		OTPField.text = "";
		ErrorText.gameObject.SetActive(false);
	}
	
	public void OTPVerfification()
	{
		if(OTPField.text == RandomSix + "")
		{
			Changer = 6;
			ErrorText.gameObject.SetActive(false);
		}
		
		else
		{
			ErrorText.gameObject.SetActive(true);
			ErrorText.text = "Incorrect OTP, Please check again.";
		}
	}
	
	public void VerificationDone()
	{
		Changer = 7;
		Invoke("SaveVerification", 1);
	}
	
	void SaveVerification()
	{
		PlayerPrefs.SetInt("Verification", 1);
	}
	
	// Awake is called when the script instance is being loaded.
	protected void Awake()
	{
		Application.targetFrameRate = 60;
		
		if(PlayerPrefs.GetInt("Verification") == 1)
		{
			this.gameObject.SetActive(false);
		}
	    
		else
		{
			this.gameObject.SetActive(true);
		}
		
	}
   
	int Small;
   
    // Update is called once per frame
    void Update()
	{
    	
	    if(Changer == 1)
	    {
	    	GetToCentre(Slides[0]);
	    }
	    
	    if(Changer == 2)
	    {
	    	GetToCentre(Slides[1]);
	    	
	    	if(!string.IsNullOrEmpty(NamesInputField[0].text) && !string.IsNullOrEmpty(NamesInputField[1].text))
	    	{
	    		BlockNext2.SetActive(false);
	    	}
	    	
	    	else
	    	{
		    	BlockNext2.SetActive(true);
	    	}
	    }
	    
	    if(Changer == 3)
	    {
	    	GetToCentre(Slides[2]);
	    	
	    	if(!string.IsNullOrEmpty(NumberField.text))
	    	{
	    		BlockNext3.SetActive(false);
	    	}
	    	
	    	else
	    	{
		    	BlockNext3.SetActive(true);
	    	}
	    }
	    
	    if(Changer == 4)
	    {
	    	GetToCentre(Slides[3]);
	    	
	    	if(!string.IsNullOrEmpty(EmailField.text))
	    	{
	    		BlockNext4.SetActive(false);
	    	}
	    	
	    	else
	    	{
		    	BlockNext4.SetActive(true);
	    	}
	    }
	    
	    if(Changer == 5)
	    {
	    	GetToCentre(Slides[4]);
	    	
	    	if(!string.IsNullOrEmpty(OTPField.text))
	    	{
	    		BlockNext5.SetActive(false);
	    	}
	    	
	    	else
	    	{
		    	BlockNext5.SetActive(true);
	    	}
	    }
	    
	    if(Changer == -6)
	    {
	    	GetToBack(Slides[4]);
	    }
	    
	    if(Changer == 6)
	    {
	    	GetToCentre(Slides[5]);
	    }
	    
	    if(Changer == 7)
	    {
	    	GetDown(MainObject);
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
