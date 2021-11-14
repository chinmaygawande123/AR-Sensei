using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
#if(UNITY_2018_3_OR_NEWER && UNITY_ANDROID)
using UnityEngine.Android;
#endif
using System.Collections;

/// <summary>
///    TestHome serves a game controller object for this application.
/// </summary>
public class TestHome : MonoBehaviour
{

    // Use this for initialization
#if (UNITY_2018_3_OR_NEWER && UNITY_ANDROID)
    private ArrayList permissionList = new ArrayList();
#endif
    static TestHelloUnityVideo app = null;

    private string HomeSceneName = "SceneHome";

	private string PlaySceneName = "SceneHelloVideo";
    
	public List<GameObject> ParticipantsList;

    // PLEASE KEEP THIS App ID IN SAFE PLACE
    // Get your own App ID at https://dashboard.agora.io/
    [SerializeField]
    private string AppID = "your_appid";

    void Awake()
    {
#if (UNITY_2018_3_OR_NEWER && UNITY_ANDROID)
		permissionList.Add(Permission.Microphone);         
		permissionList.Add(Permission.Camera);               
#endif

        // keep this alive across scenes
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        CheckAppId();
    }

    void Update()
    {
        CheckPermissions();
    }

    private void CheckAppId()
    {
        Debug.Assert(AppID.Length > 10, "Please fill in your AppId first on Game Controller object.");
        GameObject go = GameObject.Find("AppIDText");
        if (go != null)
        {
            Text appIDText = go.GetComponent<Text>();
            if (appIDText != null)
            {
                if (string.IsNullOrEmpty(AppID))
                {
                    appIDText.text = "AppID: " + "UNDEFINED!";
                }
                else
                {
                    appIDText.text = "AppID: " + AppID.Substring(0, 4) + "********" + AppID.Substring(AppID.Length - 4, 4);
                }
            }
        }
    }

    /// <summary>
    ///   Checks for platform dependent permissions.
    /// </summary>
    private void CheckPermissions()
    {
#if (UNITY_2018_3_OR_NEWER && UNITY_ANDROID)
        foreach(string permission in permissionList)
        {
            if (!Permission.HasUserAuthorizedPermission(permission))
            {                 
				Permission.RequestUserPermission(permission);
			}
        }
#endif
    }

	public string PChannelName;
	public MenuManager MenuManager;

    public void onJoinButtonClicked()
	{
		
		Debug.Log((new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name);
    	    	    	
        // get parameters (channel name, channel profile, etc.)
	    string field = PChannelName;

        // create app if nonexistent
        if (ReferenceEquals(app, null))
        {
            app = new TestHelloUnityVideo(); // create app
            app.loadEngine(AppID); // load engine
        }

        // join channel and jump to next scene
		app.join(field);
        
		StartCoroutine(TurnOnVideo());
        
		//SceneManager.sceneLoaded += OnLevelFinishedLoading; // configure GameObject after scene is loaded
		// SceneManager.LoadScene(PlaySceneName, LoadSceneMode.Single);
    }

	public HelloUnity3D SoundManager;

    public void onLeaveButtonClicked()
    {
        if (!ReferenceEquals(app, null))
        {
            app.leave(); // leave channel
            app.unloadEngine(); // delete engine
            app = null; // delete app
        }
        
	    // StartCoroutine(WaitToStartGame());
    }
    
	public void DisableVid()
	{
		app.DisableCam();	
	}
	
	public void MuteMe()
	{
		app.MuteNow();	
	}
    
	public void ResumeMe()
	{
		app.EnableVoiceNow();
	}
    
	IEnumerator WaitForVideoToFinish()
	{
		yield return new WaitForEndOfFrame();
		GameObject.Find("Main Canvas").SetActive(false);
		MenuManager.AfterVideoJoinVoice();
		SoundManager.ForLastJoin = 1;
	}

	IEnumerator WaitToStartGame()
	{
		yield return new WaitForEndOfFrame();
		GameObject.Find("Main Canvas").SetActive(false);
		MenuManager.OnStartGameBtn();
	}

	IEnumerator TurnOnVideo()
	{
		yield return new WaitForEndOfFrame();
		app.Canidates = new List<GameObject>(ParticipantsList);
		app.onSceneHelloVideoLoaded();
	}

    public void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == PlaySceneName)
        {
            if (!ReferenceEquals(app, null))
            {
                app.onSceneHelloVideoLoaded(); // call this after scene is loaded
            }
            SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        }
    }

    void OnApplicationPause(bool paused)
    {
        if (!ReferenceEquals(app, null))
        {
            app.EnableVideo(paused);
        }
    }

    void OnApplicationQuit()
    {
        if (!ReferenceEquals(app, null))
        {
            app.unloadEngine();
        }
    }
}
