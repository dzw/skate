using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public Vector2 offset;
	
	public float recycleOffset, enemyChance;
	public void enemyIfAvailable(Vector2 pos){
		if(gameObject.activeSelf || enemyChance <= Random.Range(20f, 100f)) {
			return;
		}
		transform.localPosition = pos + offset;
		gameObject.SetActive(true);
	}
	// Use this for initialization
	void Start () {
		GameEventManager.GameOver += GameOver;
		gameObject.SetActive(true);
	}
	
	void Update () {

		if(transform.localPosition.x + recycleOffset < Runner.distanceTraveled){
			gameObject.SetActive(false);
			return;
		}
		
	}



//
//	void OnCollisionEnter2D(Collider2D other) {
//						print ("collision enemy");
//						if (other.gameObject.name == "Character") {
//							print ("collision");
//							gameObject.SetActive (false);
//						}
//		}
	private void GameOver () {
		gameObject.SetActive(false);
	}
}
