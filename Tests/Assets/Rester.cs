using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

public class Rester : MonoBehaviour
{
	
	
	public NextSlider UIHandler;
	
	public MainNextSlider NextUIHandler;
	
	public GameObject LoadingCircle;
	
	public TextMeshProUGUI ErrorText;
	
	public Image SuccessImage;
	
	private static readonly string POSTAddUserURL = "https://rapidemail.rmlconnect.net/v1.0/messages/sendMail";
	
	public int IsForAlert;
	
	string NotifyJson =  @"{" + "\n" +
		@"    ""owner_id"": ""29062675""," + "\n" +
		@"    ""token"": ""4yBNdZyJl73yqpVjeUIOybVQ""," + "\n" +
		@"    ""smtp_user_name"":""smtp77873780""," + "\n" +
		@"    ""message"": {" + "\n" +
		@"        ""html"": """"," + "\n" +
		@"        ""text"": ""This message is from Unity!!!! Yay""," + "\n" +
		@"        ""subject"": ""Class Alert!""," + "\n" +
		@"        ""from_email"": ""classnotification@arsensei.edu""," + "\n" +
		@"        ""from_name"": ""AR Sensei""," + "\n" +
		@"        ""to"": [" + "\n" +
		@"            {" + "\n" +
		@"                ""email"": ""shailagawande111@gmail.com""," + "\n" +
		@"                ""name"": ""Recipient Name""," + "\n" +
		@"                ""type"": ""to""" + "\n" +
		@"            }" + "\n" +
		@"        ]," + "\n" +
		@"        ""headers"": {" + "\n" +
		@"            ""Reply-To"": ""noreply@rapidemail.rmlconnect.net""," + "\n" +
		@"            ""X-Unique-Id"": ""id """ + "\n" +
		@"        }  " + "\n" +
		@"    }" + "\n" +
		@"}" + "\n" +
		@"";
		
	string OTPJson =  @"{" + "\n" +
		@"    ""owner_id"": ""29062675""," + "\n" +
		@"    ""token"": ""4yBNdZyJl73yqpVjeUIOybVQ""," + "\n" +
		@"    ""smtp_user_name"":""smtp77873780""," + "\n" +
		@"    ""message"": {" + "\n" +
		@"        ""html"": """"," + "\n" +
		@"        ""text"": ""Your OTP for log in is 345322""," + "\n" +
		@"        ""subject"": ""AR Sensei Otp""," + "\n" +
		@"        ""from_email"": ""classnotification@arsensei.edu""," + "\n" +
		@"        ""from_name"": ""AR Sensei""," + "\n" +
		@"        ""to"": [" + "\n" +
		@"            {" + "\n" +
		@"                ""email"": ""shailagawande111@gmail.com""," + "\n" +
		@"                ""name"": ""Recipient Name""," + "\n" +
		@"                ""type"": ""to""" + "\n" +
		@"            }" + "\n" +
		@"        ]," + "\n" +
		@"        ""headers"": {" + "\n" +
		@"            ""Reply-To"": ""classnotification@arsensei.edu""," + "\n" +
		@"            ""X-Unique-Id"": ""id """ + "\n" +
		@"        }  " + "\n" +
		@"    }" + "\n" +
		@"}" + "\n" +
		@"";
	
	
	public string RoomID;
	
	public void SendAlert(string Email)
	{
		
		IsForAlert = 1;
		
		int ID1 = Random.Range(111111,999999);
				
		RoomID = "" + ID1;
		
		print(RoomID);
		
		string TempEmail = Email + "\",";
		
		string NotifyJson =  @"{" + "\n" +
			@"    ""owner_id"": ""29062675""," + "\n" +
			@"    ""token"": ""4yBNdZyJl73yqpVjeUIOybVQ""," + "\n" +
			@"    ""smtp_user_name"":""smtp77873780""," + "\n" +
			@"    ""message"": {" + "\n" +
			@"        ""html"": ""<!doctype html><html> <head> <meta name=\""viewport\"" content=\""width=device-width\"" /> <meta http-equiv=\""Content-Type\"" content=\""text/html; charset=UTF-8\"" /> <title>Simple Transactional Email</title> <style> img { border: none; -ms-interpolation-mode: bicubic; max-width: 100%; } body { background-color: #f6f6f6; font-family: sans-serif; -webkit-font-smoothing: antialiased; font-size: 14px; line-height: 1.4; margin: 0; padding: 0; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; } table { border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%; } table td { font-family: sans-serif; font-size: 14px; vertical-align: top; } .body { background-color: #f6f6f6; width: 100%; } .container { display: block; margin: 0 auto !important; max-width: 580px; padding: 10px; width: 580px; } .content { box-sizing: border-box; display: block; margin: 0 auto; max-width: 580px; padding: 10px; } .main { background: #ffffff; border-radius: 3px; width: 100%; } .wrapper { box-sizing: border-box; padding: 20px; } .content-block { padding-bottom: 10px; padding-top: 10px; } .footer { clear: both; margin-top: 10px; text-align: center; width: 100%; } .footer td, .footer p, .footer span, .footer a { color: #999999; font-size: 12px; text-align: center; } h1, h2, h3, h4 { color: #000000; font-family: sans-serif; font-weight: 400; line-height: 1.4; margin: 0; margin-bottom: 30px; } h1 { font-size: 35px; font-weight: 300; text-align: center; text-transform: capitalize; } p, ul, ol { font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; margin-bottom: 15px; } p li, ul li, ol li { list-style-position: inside; margin-left: 5px; } a { color: #3498db; text-decoration: underline; } .btn { box-sizing: border-box; width: 100%; } .btn > tbody > tr > td { padding-bottom: 15px; } .btn table { width: auto; } .btn table td { background-color: #ffffff; border-radius: 5px; text-align: center; } .btn a { background-color: #ffffff; border: solid 1px #3498db; border-radius: 5px; box-sizing: border-box; color: #3498db; cursor: pointer; display: inline-block; font-size: 14px; font-weight: bold; margin: 0; padding: 12px 25px; text-decoration: none; text-transform: capitalize; } .btn-primary table td { background-color: #3498db; } .btn-primary a { background-color: #3498db; border-color: #3498db; color: #ffffff; } .last { margin-bottom: 0; } .first { margin-top: 0; } .align-center { text-align: center; } .align-right { text-align: right; } .align-left { text-align: left; } .clear { clear: both; } .mt0 { margin-top: 0; } .mb0 { margin-bottom: 0; } .preheader { color: transparent; display: none; height: 0; max-height: 0; max-width: 0; opacity: 0; overflow: hidden; mso-hide: all; visibility: hidden; width: 0; } .powered-by a { text-decoration: none; } hr { border: 0; border-bottom: 1px solid #f6f6f6; margin: 20px 0; } @media only screen and (max-width: 620px) { table[class=body] h1 { font-size: 28px !important; margin-bottom: 10px !important; } table[class=body] p, table[class=body] ul, table[class=body] ol, table[class=body] td, table[class=body] span, table[class=body] a { font-size: 16px !important; } table[class=body] .wrapper, table[class=body] .article { padding: 10px !important; } table[class=body] .content { padding: 0 !important; } table[class=body] .container { padding: 0 !important; width: 100% !important; } table[class=body] .main { border-left-width: 0 !important; border-radius: 0 !important; border-right-width: 0 !important; } table[class=body] .btn table { width: 100% !important; } table[class=body] .btn a { width: 100% !important; } table[class=body] .img-responsive { height: auto !important; max-width: 100% !important; width: auto !important; } } @media all { .ExternalClass { width: 100%; } .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div { line-height: 100%; } .apple-link a { color: inherit !important; font-family: inherit !important; font-size: inherit !important; font-weight: inherit !important; line-height: inherit !important; text-decoration: none !important; } #MessageViewBody a { color: inherit; text-decoration: none; font-size: inherit; font-family: inherit; font-weight: inherit; line-height: inherit; } .btn-primary table td:hover { background-color: #34495e !important; } .btn-primary a:hover { background-color: #34495e !important; border-color: #34495e !important; } } </style> </head> <body class=\""\""> <span class=\""preheader\"">Your Class has been started.</span> <table role=\""presentation\"" border=\""0\"" cellpadding=\""0\"" cellspacing=\""0\"" class=\""body\""> <tr> <td>&nbsp;</td> <td class=\""container\""> <div class=\""content\""> <!-- START CENTERED WHITE CONTAINER --> <table role=\""presentation\"" class=\""main\""> <!-- START MAIN CONTENT AREA --> <tr> <td class=\""wrapper\""> <table role=\""presentation\"" border=\""0\"" cellpadding=\""0\"" cellspacing=\""0\""> <tr> <td> <p>Hi there,</p> <p>Your Class has been started. To Join use this code : XXX-XXX </p> <table role=\""presentation\"" border=\""0\"" cellpadding=\""0\"" cellspacing=\""0\"" class=\""btn btn-primary\""> </table> <p>Enjoy Learning!</p> <p>Team AR Sensei</p> </td> </tr> </table> </td> </tr> <!-- END MAIN CONTENT AREA --> </table> <!-- END CENTERED WHITE CONTAINER --> <!-- START FOOTER --> <div class=\""footer\""> <table role=\""presentation\"" border=\""0\"" cellpadding=\""0\"" cellspacing=\""0\""> <tr> <td class=\""content-block\""> <span class=\""apple-link\"">4th Dimension, 3rd Floor. MindSpace, Malad (West) Mumbai, Maharashtra 400064, IN</span> <br> Don't like these emails? <a href=\""http://i.imgur.com/CScmqnj.gif\"">Unsubscribe</a>. </td> </tr> <tr> <td class=\""content-block powered-by\""> Powered by <a href=\""https://routemobile.com\"">Route Mobile</a>. </td> </tr> </table> </div> <!-- END FOOTER --> </div> </td> <td>&nbsp;</td> </tr> </table> </body></html>""," + "\n" +
			@"        ""text"": """"," + "\n" +
			@"        ""subject"": ""AR Sensei Class Alert""," + "\n" +
			@"        ""from_email"": ""classnotification@arsensei.edu""," + "\n" +
			@"        ""from_name"": ""AR Sensei""," + "\n" +
			@"        ""to"": [" + "\n" +
			@"            {" + "\n" +
			@"                ""email"": """ + TempEmail + "\n" +
			@"                ""name"": ""Recipient Name""," + "\n" +
			@"                ""type"": ""to""" + "\n" +
			@"            }" + "\n" +
			@"        ]," + "\n" +
			@"        ""headers"": {" + "\n" +
			@"            ""Reply-To"": ""noreply@rapidemail.rmlconnect.net""," + "\n" +
			@"            ""X-Unique-Id"": ""id """ + "\n" +
			@"        }  " + "\n" +
			@"    }" + "\n" +
			@"}" + "\n" +
			@"";	
			
		NotifyJson = NotifyJson.Replace("XXX-XXX", RoomID);
		
		LoadingCircle.SetActive(true);
		
		POST(NotifyJson);
	}
	
	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	public void SendOTP(int OTP, string Email)
	{
		
		LoadingCircle.SetActive(true);
		
		string TempEmail = Email + "\",";

		string TheNewOTP =  OTP + "\",";

		string OTPJson =  @"{" + "\n" +
			@"    ""owner_id"": ""29062675""," + "\n" +
			@"    ""token"": ""4yBNdZyJl73yqpVjeUIOybVQ""," + "\n" +
			@"    ""smtp_user_name"":""smtp77873780""," + "\n" +
			@"    ""message"": {" + "\n" +
			@"        ""html"": ""<!doctype html><html> <head> <meta name=\""viewport\"" content=\""width=device-width\"" /> <meta http-equiv=\""Content-Type\"" content=\""text/html; charset=UTF-8\"" /> <title>Simple Transactional Email</title> <style> img { border: none; -ms-interpolation-mode: bicubic; max-width: 100%; } body { background-color: #f6f6f6; font-family: sans-serif; -webkit-font-smoothing: antialiased; font-size: 14px; line-height: 1.4; margin: 0; padding: 0; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; } table { border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%; } table td { font-family: sans-serif; font-size: 14px; vertical-align: top; } .body { background-color: #f6f6f6; width: 100%; } .container { display: block; margin: 0 auto !important; max-width: 580px; padding: 10px; width: 580px; } .content { box-sizing: border-box; display: block; margin: 0 auto; max-width: 580px; padding: 10px; } .main { background: #ffffff; border-radius: 3px; width: 100%; } .wrapper { box-sizing: border-box; padding: 20px; } .content-block { padding-bottom: 10px; padding-top: 10px; } .footer { clear: both; margin-top: 10px; text-align: center; width: 100%; } .footer td, .footer p, .footer span, .footer a { color: #999999; font-size: 12px; text-align: center; } h1, h2, h3, h4 { color: #000000; font-family: sans-serif; font-weight: 400; line-height: 1.4; margin: 0; margin-bottom: 30px; } h1 { font-size: 35px; font-weight: 300; text-align: center; text-transform: capitalize; } p, ul, ol { font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; margin-bottom: 15px; } p li, ul li, ol li { list-style-position: inside; margin-left: 5px; } a { color: #3498db; text-decoration: underline; } .btn { box-sizing: border-box; width: 100%; } .btn > tbody > tr > td { padding-bottom: 15px; } .btn table { width: auto; } .btn table td { background-color: #ffffff; border-radius: 5px; text-align: center; } .btn a { background-color: #ffffff; border: solid 1px #3498db; border-radius: 5px; box-sizing: border-box; color: #3498db; cursor: pointer; display: inline-block; font-size: 14px; font-weight: bold; margin: 0; padding: 12px 25px; text-decoration: none; text-transform: capitalize; } .btn-primary table td { background-color: #3498db; } .btn-primary a { background-color: #3498db; border-color: #3498db; color: #ffffff; } .last { margin-bottom: 0; } .first { margin-top: 0; } .align-center { text-align: center; } .align-right { text-align: right; } .align-left { text-align: left; } .clear { clear: both; } .mt0 { margin-top: 0; } .mb0 { margin-bottom: 0; } .preheader { color: transparent; display: none; height: 0; max-height: 0; max-width: 0; opacity: 0; overflow: hidden; mso-hide: all; visibility: hidden; width: 0; } .powered-by a { text-decoration: none; } hr { border: 0; border-bottom: 1px solid #f6f6f6; margin: 20px 0; } @media only screen and (max-width: 620px) { table[class=body] h1 { font-size: 28px !important; margin-bottom: 10px !important; } table[class=body] p, table[class=body] ul, table[class=body] ol, table[class=body] td, table[class=body] span, table[class=body] a { font-size: 16px !important; } table[class=body] .wrapper, table[class=body] .article { padding: 10px !important; } table[class=body] .content { padding: 0 !important; } table[class=body] .container { padding: 0 !important; width: 100% !important; } table[class=body] .main { border-left-width: 0 !important; border-radius: 0 !important; border-right-width: 0 !important; } table[class=body] .btn table { width: 100% !important; } table[class=body] .btn a { width: 100% !important; } table[class=body] .img-responsive { height: auto !important; max-width: 100% !important; width: auto !important; } } @media all { .ExternalClass { width: 100%; } .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div { line-height: 100%; } .apple-link a { color: inherit !important; font-family: inherit !important; font-size: inherit !important; font-weight: inherit !important; line-height: inherit !important; text-decoration: none !important; } #MessageViewBody a { color: inherit; text-decoration: none; font-size: inherit; font-family: inherit; font-weight: inherit; line-height: inherit; } .btn-primary table td:hover { background-color: #34495e !important; } .btn-primary a:hover { background-color: #34495e !important; border-color: #34495e !important; } } </style> </head> <body class=\""\""> <span class=\""preheader\"">Your Class has been started.</span> <table role=\""presentation\"" border=\""0\"" cellpadding=\""0\"" cellspacing=\""0\"" class=\""body\""> <tr> <td>&nbsp;</td> <td class=\""container\""> <div class=\""content\""> <!-- START CENTERED WHITE CONTAINER --> <table role=\""presentation\"" class=\""main\""> <!-- START MAIN CONTENT AREA --> <tr> <td class=\""wrapper\""> <table role=\""presentation\"" border=\""0\"" cellpadding=\""0\"" cellspacing=\""0\""> <tr> <td> <p>Hi there,</p> <p>Thank You for using AR Sensei. Your requested OTP for Authorisation is : XXX-XXX </p> <table role=\""presentation\"" border=\""0\"" cellpadding=\""0\"" cellspacing=\""0\"" class=\""btn btn-primary\""> </table> <p>Enjoy Learning!</p> <p>Team AR Sensei</p> </td> </tr> </table> </td> </tr> <!-- END MAIN CONTENT AREA --> </table> <!-- END CENTERED WHITE CONTAINER --> <!-- START FOOTER --> <div class=\""footer\""> <table role=\""presentation\"" border=\""0\"" cellpadding=\""0\"" cellspacing=\""0\""> <tr> <td class=\""content-block\""> <span class=\""apple-link\"">4th Dimension, 3rd Floor. MindSpace, Malad (West) Mumbai, Maharashtra 400064, IN</span> <br> Don't like these emails? <a href=\""http://i.imgur.com/CScmqnj.gif\"">Unsubscribe</a>. </td> </tr> <tr> <td class=\""content-block powered-by\""> Powered by <a href=\""https://routemobile.com\"">Route Mobile</a>. </td> </tr> </table> </div> <!-- END FOOTER --> </div> </td> <td>&nbsp;</td> </tr> </table> </body></html>""," + "\n" +
			@"        ""text"": """"," + "\n" +
			@"        ""subject"": ""AR Sensei Otp""," + "\n" +
			@"        ""from_email"": ""classnotification@arsensei.edu""," + "\n" +
			@"        ""from_name"": ""AR Sensei""," + "\n" +
			@"        ""to"": [" + "\n" +
			@"            {" + "\n" +
			@"                ""email"": """ + TempEmail + "\n" +
			@"                ""name"": ""Recipient Name""," + "\n" +
			@"                ""type"": ""to""" + "\n" +
			@"            }" + "\n" +
			@"        ]," + "\n" +
			@"        ""headers"": {" + "\n" +
			@"            ""Reply-To"": ""classnotification@arsensei.edu""," + "\n" +
			@"            ""X-Unique-Id"": ""id """ + "\n" +
			@"        }  " + "\n" +
			@"    }" + "\n" +
			@"}" + "\n" +
			@"";
			
		OTPJson = OTPJson.Replace("Hi there," , "Hi, " +  PlayerPrefs.GetString("First Name"));	
		OTPJson = OTPJson.Replace("XXX-XXX",  OTP + "");	

		POST(OTPJson);
	}
	
	public WWW POST(string jsonStr)
	{
		WWW www;
		Hashtable postHeader = new Hashtable();
		postHeader.Add("Content-Type", "application/json");

		// convert json string to byte
		var formData = System.Text.Encoding.UTF8.GetBytes(jsonStr);

		www = new WWW(POSTAddUserURL, formData, postHeader);
		StartCoroutine(WaitForRequest(www));
		return www;
	}
	
	IEnumerator WaitForRequest(WWW data)
	{
		
		ErrorText.text = "There was an error occured please check your connection.";
		
		yield return data; // Wait until the download is done
		if (data.error != null)
		{
			Debug.Log("There was an error sending request: " + data.error);
			LoadingCircle.SetActive(false);
			ErrorText.gameObject.SetActive(true);
		}
		else
		{
			Debug.Log("WWW Request: " + data.text);
			LoadingCircle.SetActive(false);
			ErrorText.gameObject.SetActive(false);
			
			if(IsForAlert == 1)
			{
				
				if(data.text.Contains("error"))
				{
					print("Alert Failed");
					ErrorText.gameObject.SetActive(true);
					ErrorText.text = "Please check if you have typed the correct E-mail.";
				}
				
				else
				{
					print("Alert Sent");
					NextUIHandler.Changer = -2; 
					InvokeRepeating("ImageFader", 0, 0.01f);
					SuccessImage.gameObject.SetActive(true);
				}
								
			}
			
			else
			{
				UIHandler.Changer = 5;
			}			
		}
	}
	
	void ImageFader()
	{
		Image img = SuccessImage;
		
		if(img.color.a > 0)
		{
			img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a - 0.005f);
		}
		
		else
		{
			CancelInvoke("ImageFader");
		}
	}
}
