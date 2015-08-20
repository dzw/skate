using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	public Vector2 offset;

	public float recycleOffset, coinChance;
	public void coinIfAvailable(Vector2 pos){
		print ("calling");
		if(gameObject.activeSelf || coinChance <= Random.Range(0f, 100f)) {
			return;
		}
		transform.localPosition = pos + offset;
		gameObject.SetActive(true);
	}
	// Use this for initialization
	void Start () {
		GameEventManager.GameOver += GameOver;
		gameObject.SetActive(false);
	}
	
	void Update () {
		if(transform.localPosition.x + recycleOffset < Runner.distanceTraveled){
			gameObject.SetActive(false);
			return;
		}

	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.name == "Character") {
						
//			print ("coin collision");
			gameObject.SetActive (false);
				}

	}
	private void GameOver () {
		gameObject.SetActive(false);
	}
}
