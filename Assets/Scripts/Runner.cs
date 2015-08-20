using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using PlayerPrefs = PreviewLabs.PlayerPrefs;

public class Runner : MonoBehaviour
{
	
		public static float distanceTraveled, score;
		public Vector2 acceleration;
		public float runningVelocity;
		public Vector2 jumpVelocity;
		public float GameOverY;
		private bool touchingPlatform;
		//	private bool strikeCharacter;
		private Vector2 startPosition;
		//	public AudioSource jumpSound,hitSound,dieSound,strikeSound;
		public AudioClip jumpClip, hitClip, dieClip, strikeClip, pickupClip;
		private bool strikePress;
		private static int lifeForRunner;
		//		public  Image[] heart;
		public Text heartCount;
		Animator anim;
		// Use this for initialization

		void Awake ()
		{
				anim = GetComponent<Animator> ();
				GameEventManager.GameStart += GameStart;
				GameEventManager.GameOver += GameOver;
				startPosition = transform.localPosition;
		
				renderer.enabled = false;
				rigidbody2D.isKinematic = true;
				enabled = false;
				strikePress = false;
				
		}

		void OnDestroy ()
		{
				GameEventManager.GameStart -= GameStart;
				GameEventManager.GameOver -= GameOver;
		}

		void Start ()
		{
		
				anim.SetBool ("die", false);
			
		}
	
		public void OnApplicationQuit ()
		{
				PlayerPrefs.Flush ();
		}
	
		private void GameStart ()
		{
				distanceTraveled = 0f;
				lifeForRunner = 2;
				score = 0f;
				heartCount.text = "x2";
				transform.localPosition = startPosition;
				renderer.enabled = true;
				rigidbody2D.isKinematic = false;
				enabled = true;
				print ("this game start in runner");
				GameObject.Find ("ParrallaxCamera").GetComponent<ParallaxScrol> ().enabled = true;
				GameObject.Find ("Character").GetComponent<Runner> ().enabled = true;
		
				anim.SetBool ("die", false);
		
		
		}
	
		private void GameOver ()
		{
				renderer.enabled = false;
				rigidbody2D.isKinematic = true;
				enabled = false;
		}
	
		void FixedUpdate ()
		{
				anim.SetBool ("touchingPlatform", touchingPlatform);
		
				if (touchingPlatform) {
			
						gameObject.rigidbody2D.velocity = new Vector2 (runningVelocity, rigidbody2D.position.y);
			
				} 
		
		}
	
		void CallTriggerGameOver ()
		{
				GameEventManager.TriggerGameOver ();
		
		}
	
		void OnTriggerEnter2D (Collider2D other)
		{
				if (other.gameObject.tag == "Coin") {
						UIManager.SetDistance (++score);
			
						if (PlayerPrefs.GetFloat ("Sound") == 1.0f)
								audio.PlayOneShot (pickupClip);
						other.gameObject.SetActive (false);
				}
				if (other.gameObject.tag == "Enemy7" || other.gameObject.tag == "Enemy" || other.gameObject.tag == "Enemy2" || other.gameObject.tag == "Enemy3" || other.gameObject.tag == "Enemy4" || other.gameObject.tag == "Enemy5" || other.gameObject.tag == "Enemy6") {
			
						{
								if (PlayerPrefs.GetFloat ("Sound") == 1.0f)
										audio.PlayOneShot (dieClip);
								if (lifeForRunner == 0) {
					
										GameObject.Find ("ParrallaxCamera").GetComponent<ParallaxScrol> ().enabled = false;
					
										anim.SetBool ("die", true);
										Invoke ("CallTriggerGameOver", 1.00f);
								} else {	
										anim.SetTrigger ("collision");
										lifeForRunner--;
										heartCount.text = "x" + lifeForRunner;
					
								}
								other.gameObject.SetActive (false);
				
						}
				}
		
		}
	
		void OnCollisionEnter2D (Collision2D other)
		{
				if (other.gameObject.tag == "Platform") {
						touchingPlatform = true;
				}
				if (other.gameObject.tag == "Enemy7" || other.gameObject.tag == "Enemy" || other.gameObject.tag == "Enemy2" || other.gameObject.tag == "Enemy3" || other.gameObject.tag == "Enemy4" || other.gameObject.tag == "Enemy5" || other.gameObject.tag == "Enemy6") {
			
			
			
						if (PlayerPrefs.GetFloat ("Sound") == 1.0f)
								audio.PlayOneShot (dieClip);
						if (lifeForRunner == 0) {
				
				
								GameObject.Find ("ParrallaxCamera").GetComponent<ParallaxScrol> ().enabled = false;
				
								anim.SetBool ("die", true);
				
								Invoke ("CallTriggerGameOver", 1.00f);
				
						} else {	
								anim.SetTrigger ("collision");
				
								lifeForRunner--;
								heartCount.text = "x" + lifeForRunner;
				
				
						}
						other.gameObject.SetActive (false);
			
				}
		}
	
		void OnCollisionExit2D (Collision2D other)
		{
				
		
		}
	
		public void Jump ()
		{
				if (PlayerPrefs.GetFloat ("Sound") == 1.0f)
						audio.PlayOneShot (jumpClip);
				if (touchingPlatform) {
						gameObject.rigidbody2D.AddForce (new Vector2 (15.5f, 20), ForceMode2D.Impulse);
						touchingPlatform = false;
						anim.SetTrigger ("jump");
						Invoke ("jumpPressFalse", 1.02f);
			
				}
		}
	
		void jumpPressFalse ()
		{
		}
	
		public void Strike ()
		{
				if (!strikePress) {
						if (PlayerPrefs.GetFloat ("Sound") == 1.0f)
								audio.PlayOneShot (strikeClip);
						strikePress = true;
						anim.SetTrigger ("strike");
						Vector2 positionOFCenter = new Vector2 (0.4f, -0.60f);
						GetComponent<BoxCollider2D> ().center = positionOFCenter;
						Invoke ("strikePressFalse", 1.2f);
				}
		}
	
		void strikePressFalse ()
		{
				strikePress = false;
				Vector2 positionOFCenter = new Vector2 (0.4f, 0.53f);
				GetComponent<BoxCollider2D> ().center = positionOFCenter;
		
		}
	
		// Update is called once per frame
		void Update ()
		{
		
		
		
		
				transform.localRotation = Quaternion.identity;
				//				Vector2 tempValue = transform.localPosition;
		
				distanceTraveled = transform.localPosition.x;

				Vector3 pos = new Vector3 (transform.localPosition.x, transform.localPosition.y, -6f);
				transform.localPosition = pos;

		}
}
