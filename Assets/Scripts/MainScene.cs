using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System;

//using Mono.Data.SqliteClient;
//using System.Data;
using System.Collections.Generic;
public class MainScene : MonoBehaviour
{

		public Button spinner, playAgain, accept, user, yes, no, one, two, three, four, five, six, seven, eight, nine, ten, eleven, twelve, thriteen, fourteen, fifteen, sixteen, seventeen, eighteen;
		public Image popUp, popUpLogOut, testImage;
		public Text loading, category, userName;
		public static string salt, sign;
		public static bool pressSpinner = false;
		public GameObject choose1, choose2;

		void Start ()
		{	
				pressSpinner = false;

				userName.text = PlayerPrefs.GetString (Constant.USER_NAME);
				playAgain.onClick.AddListener (() => {
			
						print ("hide popup");
						hidePopup ();
				});
				user.onClick.AddListener (() => {
						showPopUpLogout ();
					
				});

//				accept.onClick.AddListener (() => {
//						Application.LoadLevel ((int)Constant.SCENES.GAMESCENE);
//					
//				});
				one.onClick.AddListener (() => {
						PlayerPrefs.SetInt ("World", 1);
						Managers.Instance.SceneChage (1);	
						hidePopup ();
				});

				two.onClick.AddListener (() => {
						PlayerPrefs.SetInt ("World", 2);
						Managers.Instance.SceneChage (1);
						hidePopup ();
				});
				three.onClick.AddListener (() => {
						PlayerPrefs.SetInt ("World", 3);
						Managers.Instance.SceneChage (1);	
						hidePopup ();
				});
				four.onClick.AddListener (() => {
						PlayerPrefs.SetInt ("World", 4);
						Managers.Instance.SceneChage (1);	
						hidePopup ();
				});
				five.onClick.AddListener (() => {
						PlayerPrefs.SetInt ("World", 5);
						Managers.Instance.SceneChage (1);			
						hidePopup ();
				});
				six.onClick.AddListener (() => {
						PlayerPrefs.SetInt ("World", 6);
						Managers.Instance.SceneChage (1);			
						hidePopup ();
				});
				seven.onClick.AddListener (() => {
						PlayerPrefs.SetInt ("World", 7);
						Managers.Instance.SceneChage (1);		
						hidePopup ();
				});
				eight.onClick.AddListener (() => {
						PlayerPrefs.SetInt ("World", 8);
						Managers.Instance.SceneChage (1);			
						hidePopup ();
				});
				nine.onClick.AddListener (() => {
						PlayerPrefs.SetInt ("World", 9);
						Managers.Instance.SceneChage (1);			
						hidePopup ();
				});
				ten.onClick.AddListener (() => {
						PlayerPrefs.SetInt ("World", 10);
						Managers.Instance.SceneChage (1);			
						hidePopup ();
				});
				eleven.onClick.AddListener (() => {
						PlayerPrefs.SetInt ("World", 11);
						Managers.Instance.SceneChage (1);			
						hidePopup ();
				});
				twelve.onClick.AddListener (() => {
						PlayerPrefs.SetInt ("World", 12);
						Managers.Instance.SceneChage (1);			
						hidePopup ();
				});
				thriteen.onClick.AddListener (() => {
						PlayerPrefs.SetInt ("World", 13);
						Managers.Instance.SceneChage (1);			
						hidePopup ();
				});
				fourteen.onClick.AddListener (() => {
						PlayerPrefs.SetInt ("World", 14);
						Managers.Instance.SceneChage (1);			
						hidePopup ();
				});
				fifteen.onClick.AddListener (() => {
						PlayerPrefs.SetInt ("World", 15);
						Managers.Instance.SceneChage (1);			
						hidePopup ();
				});
				sixteen.onClick.AddListener (() => {
						PlayerPrefs.SetInt ("World", 16);
						Managers.Instance.SceneChage (1);			
						hidePopup ();
				});
				seventeen.onClick.AddListener (() => {
						PlayerPrefs.SetInt ("World", 17);
						Managers.Instance.SceneChage (1);
						hidePopup ();
				});
				eighteen.onClick.AddListener (() => {
						PlayerPrefs.SetInt ("World", 18);
						Managers.Instance.SceneChage (1);
						hidePopup ();
				});
				spinner.onClick.AddListener (() => {
						print ("spinner press");
						float timer = UnityEngine.Random.Range (2.5f, 5.0f);
						if (!pressSpinner) {
								pressSpinner = true;
								print ("inside condition spinner press");
								
								iTween.RotateBy (GameObject.Find ("Wheel"), new Vector3 (0, 0, UnityEngine.Random.Range (-220, -1360)), timer);
								Invoke ("CategorySelectShow", timer += 0.35f);
						}
						Invoke ("SpinnerFalse", timer);
				});
		}

		void SpinnerFalse ()
		{
//				pressSpinner = false;
		}

		void CategorySelectShow ()
		{

				pressSpinner = false;
				RaycastHit2D hit = Physics2D.Raycast (Vector2.up, spinner.transform.position);
				if (hit.collider != null) {


						//			print ("wokring");
						if (hit.collider.tag == "Asian") {
								category.text = "City";
								PlayerPrefs.SetInt ("Stage", 1);
						}
						if (hit.collider.tag == "Classic") {
								category.text = "Nature";
								PlayerPrefs.SetInt ("Stage", 2);
						}
					
						if (hit.collider.tag == "City") {
								category.text = "Forrest";
								PlayerPrefs.SetInt ("Stage", 3);
						}
						if (hit.collider.tag == "Miscellaneous") {
								category.text = "Horror";
								PlayerPrefs.SetInt ("Stage", 4);
						}
						if (hit.collider.tag == "Horror") {
								category.text = "Alien";
								PlayerPrefs.SetInt ("Stage", 5);
						}
						if (hit.collider.tag == "Snow") {
								category.text = "Classics";
								PlayerPrefs.SetInt ("Stage", 6);
						}
						showPopUp ();

					
				}

		}
//	publi
		void CallBackAction (bool res, object obj)
		{
				print ("Call back from action in wheel Scene");
//				Managers.Instance.SceneChage ((int)Constant.SCENES.QUIZSCENE);


		}

		void showPopUpLogout ()
		{
				
		
				popUpLogOut.GetComponent<CanvasGroup> ().alpha = 1;
				popUpLogOut.GetComponent<CanvasGroup> ().interactable = true;
				popUpLogOut.GetComponent<CanvasGroup> ().blocksRaycasts = true;
		
		}

		void hidePopUpLogOut ()
		{
				popUpLogOut.GetComponent<CanvasGroup> ().alpha = 0;
				popUpLogOut.GetComponent<CanvasGroup> ().interactable = false;
				popUpLogOut.GetComponent<CanvasGroup> ().blocksRaycasts = false;
		}

		void showPopUp ()
		{
			




				popUp.GetComponent<CanvasGroup> ().alpha = 1;
				popUp.GetComponent<CanvasGroup> ().interactable = true;
				popUp.GetComponent<CanvasGroup> ().blocksRaycasts = true;

				int world = PlayerPrefs.GetInt ("Stage");

				stageHide ();
				switch (world) {
				case 1:
						one.gameObject.SetActive (true);
						two.gameObject.SetActive (true);
						three.gameObject.SetActive (true);
						break;
				case 2:
						four.gameObject.SetActive (true);
						five.gameObject.SetActive (true);
						six.gameObject.SetActive (true);
						break;
				case 3:
						seven.gameObject.SetActive (true);
						eight.gameObject.SetActive (true);
						nine.gameObject.SetActive (true);
						break;
				case 4:
						ten.gameObject.SetActive (true);
						eleven.gameObject.SetActive (true);
						twelve.gameObject.SetActive (true);
						break;
				case 5:
						thriteen.gameObject.SetActive (true);
						fourteen.gameObject.SetActive (true);
						fifteen.gameObject.SetActive (true);
						break;
				case 6:
						sixteen.gameObject.SetActive (true);
						seventeen.gameObject.SetActive (true);
						eighteen.gameObject.SetActive (true);
						break;
				}
				
		}

		void stageShow ()
		{
				
	
		}

		void stageHide ()
		{
				one.gameObject.SetActive (false);
				two.gameObject.SetActive (false);
				three.gameObject.SetActive (false);
				four.gameObject.SetActive (false);
				five.gameObject.SetActive (false);
				six.gameObject.SetActive (false);
				seven.gameObject.SetActive (false);
				eight.gameObject.SetActive (false);
				nine.gameObject.SetActive (false);
				ten.gameObject.SetActive (false);
				eleven.gameObject.SetActive (false);
				twelve.gameObject.SetActive (false);
				thriteen.gameObject.SetActive (false);
				fourteen.gameObject.SetActive (false);
				fifteen.gameObject.SetActive (false);
				sixteen.gameObject.SetActive (false);
				seventeen.gameObject.SetActive (false);
				eighteen.gameObject.SetActive (false);
				
		}

		void hidePopup ()
		{
				pressSpinner = false;
				popUp.GetComponent<CanvasGroup> ().alpha = 0;
				popUp.GetComponent<CanvasGroup> ().interactable = false;
				popUp.GetComponent<CanvasGroup> ().blocksRaycasts = false;

			
		
		

		}
		// Update is called once per frame
		void Update ()
		{
//			if()
				if (Input.GetKeyDown (KeyCode.Escape)) { 
			
						//			GoogleAdController.ShowInterstital();
						Application.Quit (); 
				}
		}
}
