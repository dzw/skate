using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using PlayerPrefs = PreviewLabs.PlayerPrefs;

public class UIManager : MonoBehaviour
{

		// Use this for initialization
		public Text distanceText, scoreText, highScoreText;
		public Button facebookLogin, jumpButton, strikeButton, playButton, settingButton, backButtonFromSetting, playAgain, fbShare, soundButton, musicButton, backButton;
		public Image settingViewImage, gameOverScene, logo ;
		private string lastResponse = null;
		private static bool play;
		private int highScore ;
		private static UIManager instance;
		// Use this for initialization
		void Awake ()
		{
				GameEventManager.GameStart += GameStart;
				GameEventManager.GameOver += GameOver;
//		DontDestroyOnLoad(this.gameObject);
		}

		void OnDestroy ()
		{
				GameEventManager.GameStart -= GameStart;
				GameEventManager.GameOver -= GameOver;
		}

		void Start ()
		{

				instance = this;
				


				jumpButton.GetComponent<CanvasGroup> ().alpha = 0;
				strikeButton.GetComponent<CanvasGroup> ().alpha = 0;
				strikeButton.GetComponent<CanvasGroup> ().interactable = false;
				jumpButton.GetComponent<CanvasGroup> ().interactable = false;
				FB.Init (SetInit, OnHideUnity);

				if (PlayerPrefs.GetFloat ("Music") == 0.20f) {
						musicButton.GetComponent<CanvasGroup> ().alpha = 0.20f;
						GameObject.Find ("Background_music").GetComponent<AudioSource> ().Pause ();
				} else {
						musicButton.GetComponent<CanvasGroup> ().alpha = 1.00f;
						GameObject.Find ("Background_music").GetComponent<AudioSource> ().Play ();
			
				}
				if (PlayerPrefs.GetFloat ("Sound") == 0.20f) {
						soundButton.GetComponent<CanvasGroup> ().alpha = 0.20f;
				} else {
						soundButton.GetComponent<CanvasGroup> ().alpha = 1.0f;
				}


				highScoreText.text = PlayerPrefs.GetInt ("HighScore").ToString ();
				playButton.onClick.AddListener (() => {

						playButton.interactable = false;
						playButton.GetComponent<CanvasGroup> ().alpha = 0;

						backButton.interactable = false;
						backButton.GetComponent<CanvasGroup> ().alpha = 0;

						logo.GetComponent<CanvasGroup> ().alpha = 0;
						GameEventManager.TriggerGameStart ();

				});
				playAgain.onClick.AddListener (() => {
						gameOverScene.GetComponent<CanvasGroup> ().alpha = 0;
						//			scoreText.GetComponent<CanvasGroup> ().alpha = 0;
			
						backButton.interactable = false;
						backButton.GetComponent<CanvasGroup> ().alpha = 0;
						gameOverScene.GetComponent<CanvasGroup> ().interactable = false;
						gameOverScene.GetComponent<CanvasGroup> ().blocksRaycasts = false;
						fbShare.GetComponent<CanvasGroup> ().alpha = 0;
						playAgain.GetComponent<CanvasGroup> ().alpha = 0;
						settingButton.GetComponent<CanvasGroup> ().alpha = 1;
						settingButton.interactable = true;
						GameEventManager.TriggerGameStart ();
			
				});

				soundButton.onClick.AddListener (() => {
						if (PlayerPrefs.GetFloat ("Sound") == 1.0f) {
								soundButton.GetComponent<CanvasGroup> ().alpha = 0.2f;
								PlayerPrefs.SetFloat ("Sound", 0.2f);
						} else {
								soundButton.GetComponent<CanvasGroup> ().alpha = 1.0f;
								PlayerPrefs.SetFloat ("Sound", 1.0f);
				
						}
				});

				backButton.onClick.AddListener (() => {
//						MainScene.pressSpinner = false;
						print (MainScene.pressSpinner + "    ki je asehe ");
						Time.timeScale = 1;
						Managers.Instance.SceneChage (0);
				});
				musicButton.onClick.AddListener (() => {
						if (PlayerPrefs.GetFloat ("Music") == 1.0f) {
								musicButton.GetComponent<CanvasGroup> ().alpha = 0.2f;
								GameObject.Find ("Background_music").GetComponent<AudioSource> ().Pause ();
								PlayerPrefs.SetFloat ("Music", 0.2f);
						} else {
								musicButton.GetComponent<CanvasGroup> ().alpha = 1.0f;
								GameObject.Find ("Background_music").GetComponent<AudioSource> ().Play ();
								PlayerPrefs.SetFloat ("Music", 1.0f);
				
						}			
				});
				fbShare.onClick.AddListener (() => {
						if (!FB.IsLoggedIn)
								CallFBLogin ();
			
						FB.Feed (
				link: "http://www.siliconorchard.com",
				linkName: "Skate All The Way",
				linkCaption: "Enjoy the game",
				linkDescription: "My Score is " + scoreText.text + ", What is yours?",
				picture: "http://www.siliconorchard.com//silicon_src2/fb_imgforskaterunner.png",
				callback: LogCallback
						);
			
				});
				
				settingButton.onClick.AddListener (() => {
						settingButton .GetComponent<CanvasGroup> ().alpha = 0;
						settingViewImage.GetComponent<CanvasGroup> ().alpha = 1;
						settingViewImage.GetComponent<CanvasGroup> ().blocksRaycasts = true;
						settingViewImage.GetComponent<CanvasGroup> ().interactable = true;
						backButton.interactable = true;
						backButton.GetComponent<CanvasGroup> ().alpha = 1;
						Time.timeScale = 0;
			
				});
				backButtonFromSetting.onClick.AddListener (() => {
						settingButton .GetComponent<CanvasGroup> ().alpha = 1;
						settingViewImage.GetComponent<CanvasGroup> ().alpha = 0;
						settingViewImage.GetComponent<CanvasGroup> ().blocksRaycasts = false;
						settingViewImage.GetComponent<CanvasGroup> ().interactable = false;
						backButton.interactable = false;
						backButton.GetComponent<CanvasGroup> ().alpha = 0;
						Time.timeScale = 1;
				});


				facebookLogin.onClick.AddListener (() => {
						print ("fb working");
		
						CallFBLogin ();
						print (lastResponse);
		
				});
		}

		void LogCallback (FBResult response)
		{
				Debug.Log (response.Text);
		}

		public static void SetDistance (float distance)
		{
				instance.distanceText.text = distance.ToString ("f0");
		}

		public static void DisableHeart (int i)
		{
		}
		// Update is called once per frame
		void Update ()
		{
			
		
				if (PlayerPrefs.GetInt ("HighScore") < (Runner.score)) {
		

						highScoreText.text = (Runner.score).ToString ();
						PlayerPrefs.SetInt ("HighScore", (int)Runner.score);

				}

		
		}

		private void SetInit ()
		{
				enabled = true; 
				// "enabled" is a magic global; this lets us wait for FB before we start rendering
		}
	
		private void OnHideUnity (bool isGameShown)
		{
				if (!isGameShown) {
						// pause the game - we will need to hide
						Time.timeScale = 0;
				} else {
						// start the game back up - we're getting focus again
						Time.timeScale = 1;
				}
		}

		private void CallFBLogin ()
		{
				FB.Login ("email,publish_actions", LoginCallback);
		}
	
		void LoginCallback (FBResult result)
		{
				if (result.Error != null)
						lastResponse = "Error Response:\n" + result.Error;
				else if (!FB.IsLoggedIn) {
						lastResponse = "Login cancelled by Player";
				} else {
						lastResponse = "Login was successful!";
				}
		}

		private void GameOver ()
		{
				jumpButton.GetComponent<CanvasGroup> ().alpha = 0;
				jumpButton.GetComponent<CanvasGroup> ().interactable = false;
				strikeButton.GetComponent<CanvasGroup> ().alpha = 0;
				strikeButton.GetComponent<CanvasGroup> ().interactable = false;
//		scoreText.GetComponent<CanvasGroup> ().alpha = 1;
				scoreText.text = (Runner.score).ToString ("f0");
				SetDistance (0.0f);
				gameOverScene.GetComponent<CanvasGroup> ().alpha = 1;
				gameOverScene.GetComponent<CanvasGroup> ().interactable = true;
				gameOverScene.GetComponent<CanvasGroup> ().blocksRaycasts = true;
				fbShare.GetComponent<CanvasGroup> ().alpha = 1;
				playAgain.GetComponent<CanvasGroup> ().alpha = 1;
				settingButton.GetComponent<CanvasGroup> ().alpha = 0;
				settingButton.interactable = false;
				backButton.interactable = true;
				backButton.GetComponent<CanvasGroup> ().alpha = 1;
				enabled = true;
		}

		private void GameStart ()
		{
				print ("This game start in UIMANAGER");
				jumpButton.GetComponent<CanvasGroup> ().alpha = 1;
				strikeButton.GetComponent<CanvasGroup> ().alpha = 1;
				jumpButton.GetComponent<CanvasGroup> ().interactable = true;
				strikeButton.GetComponent<CanvasGroup> ().interactable = true;

				enabled = false;
		}
}
