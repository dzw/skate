using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManager : MonoBehaviour {

	public Text runnerText, instructionText, gameOverText; 
	// Use this for initialization
	void Start () {
		runnerText = GetComponent<Text> ();
		instructionText = GetComponent<Text> ();
		gameOverText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
