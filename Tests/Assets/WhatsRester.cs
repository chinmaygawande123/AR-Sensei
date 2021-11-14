using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

public class WhatsRester : MonoBehaviour
{
	
	//
	
	public GameObject MainLoading;
		
	private static readonly string POSTAddUserURL = "https://rapidapi.rmlconnect.net/wbm/v1/message";
		
	public TMP_InputField PhoneNumber;
	
	public GameObject PopUp;
	
	public GameObject SentSuccessPop;
	
	public GameObject ErrorBox;
	
	public void EnablePopUp()
	{
		if(PhoneNumber.text.Length == 10)
		{
			PopUp.SetActive(true);
		}
	}
	
	public void SendWhatsapp()
	{
		
		PopUp.SetActive(false);
		MainLoading.SetActive(true);
		
		string Tempnumber = "+91" + PhoneNumber.text + "\",";
		
		string WhatsappJSON = @"{" + "\n" +
			@"""phone"": """ + Tempnumber + "\n" +
			@"""media"": {" + "\n" +
			@"""type"": ""media_template""," + "\n" +
			@"""template_name"": ""admission_confirmation""," + "\n" +
			@"""lang_code"": ""en""," + "\n" +
			@"    ""body"": [" + "\n" +
			@"            {" + "\n" +
			@"                ""text"": ""Student""" + "\n" +
			@"            }," + "\n" +
			@"            {" + "\n" +
			@"                ""text"": ""AR Sensei. Your Teacher has sent this message to Alert you about the next class. {Note : The messaage after this is useless, Since I have used this template and not a custom one. Thank You!}""" + "\n" +
			@"            }," + "\n" +
			@"            {" + "\n" +
			@"                ""text"": ""14/11/2021""" + "\n" +
			@"            }," + "\n" +
			@"            {" + "\n" +
			@"                ""text"": ""tommorow.""" + "\n" +
			@"            }," + "\n" +
			@"            {" + "\n" +
			@"                ""text"": ""+919011014215""" + "\n" +
			@"            }" + "\n" +
			@"        ]" + "\n" +
			@"  }" + "\n" +
			@"}";
			
		POST(WhatsappJSON);
	}
	
	public WWW POST(string jsonStr)
	{
		WWW www;
		Hashtable postHeader = new Hashtable();
		postHeader.Add("Content-Type", "application/json");
		postHeader.Add("Authorization", "617bf267245383001100f854");

		// convert json string to byte
		var formData = System.Text.Encoding.UTF8.GetBytes(jsonStr);

		www = new WWW(POSTAddUserURL, formData, postHeader);
		StartCoroutine(WaitForRequest(www));
		return www;
	}
	
	IEnumerator WaitForRequest(WWW data)
	{
		yield return data; // Wait until the download is done
		
		print( "Was here" + data.text);
		
		if (data.error != null)
		{
			Debug.Log("There was an error sending request: " + data.error);
			
			ErrorBox.SetActive(true);
		}
		
		else
		{
			MainLoading.SetActive(false);
			
			SentSuccessPop.SetActive(true);
			
			print(data.text);
		}
	}
	
	public void SentSuccessDone()
	{
		SentSuccessPop.SetActive(false);
		PhoneNumber.text = "";
	} 
	
	public void ErrorBoxDone()
	{
		ErrorBox.SetActive(false);
		PhoneNumber.text = "";
	}
}
