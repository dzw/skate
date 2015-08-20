using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	public Vector3 offset ;
	Transform targetTransform ;
	void Start () {
		
		targetTransform = GameObject.FindWithTag("Player").GetComponent<Transform> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.position = new Vector3( targetTransform.position.x+15 + offset.x,transform.position.y,transform.position.z  );
		
	}
}
