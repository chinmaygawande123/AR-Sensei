using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CognitiveServicesTTS;
using System;
using System.Threading.Tasks;

public class UIManager : MonoBehaviour {

	public SpeechManager speech;
    
	public string[] Dialogues;

    private void Start()
    {

    }

    // The spinning cube is only used to verify that speech synthesis doesn't introduce
    // game loop blocking code.
    public void Update()
    {

    }

    /// <summary>
    /// Speech synthesis can be called via REST API or Speech Service SDK plugin for Unity
	/// </summary>
	/// 
	
	public int SpeechStatus;
	
    public async void SpeechPlayback()
    {
        if (speech.isReady)
        {
	        string msg = Dialogues[SpeechStatus];
	        speech.voiceName = VoiceName.enUSSaraNeural;
	        speech.VoicePitch = 0;
	        await Task.Run(() => speech.SpeakWithSDKPlugin(msg));
	        /*
	        
            if (useSDK.isOn)
            {
                // Required to insure non-blocking code in the main Unity UI thread.
                await Task.Run(() => speech.SpeakWithSDKPlugin(msg));
            }
            else
            {
                // This code is non-blocking by default, no need to run in background
                speech.SpeakWithRESTAPI(msg);
	        }
	        
	        */
        } else
        {
            Debug.Log("SpeechManager is not ready. Wait until authentication has completed.");
        }
    }

    public void ClearText()
    {
        
    }
}
