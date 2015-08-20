using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class FirstScene : MonoBehaviour {
	public Button play;
	// Use this for initialization
	void Start () {
		play.onClick.AddListener(()=>{
			Managers.Instance.SceneChage(1);
		});
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
