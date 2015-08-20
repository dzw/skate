using UnityEngine;
using System.Collections;

public class Camera2DFollow : MonoBehaviour {
	
	public Transform target;
	public float damping = 0;
	public float lookAheadFactor = 0;
	public float lookAheadReturnSpeed = 0.0f;
	public float lookAheadMoveThreshold = 0.0f;
	
	float offsetZ;
	Vector3 lastTargetPosition;
	Vector3 currentVelocity;
	Vector3 lookAheadPos;
	
	// Use this for initialization
	void Start () {
//		Camera.main.transform.position = new Vector3 (0.0f, 0f, 0f);
		lastTargetPosition = new Vector3(target.position.x,0,0);
		offsetZ = (transform.position - target.position).z;
		transform.parent = null;
	}
	
	// Update is called once per frame
	void Update () {
		
		// only update lookahead pos if accelerating or changed direction
		float xMoveDelta = (target.position - lastTargetPosition).x;

	    bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

		if (updateLookAheadTarget) {
			lookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta +20f) ;
		} else {
			lookAheadPos = Vector3.MoveTowards(lookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed) ;	
		}
		
		Vector3 aheadTargetPos = target.position + lookAheadPos + Vector3.forward * offsetZ;
//		Vector3 newPos = Vector3.SmoothDamp(new Vector3(transform.position.x,0,0), aheadTargetPos, ref currentVelocity, damping);
//				Vector3 newPos = Vector3.SmoothDamp(new Vector3(transform.position.x,0,0), new Vector3(aheadTargetPos.x,aheadTargetPos.y+10f,aheadTargetPos.z), ref currentVelocity, damping);
				Vector3 newPos = Vector3.SmoothDamp(new Vector3(transform.position.x,0,0), new Vector3(aheadTargetPos.x,target.position.y+ 10f,aheadTargetPos.z), ref currentVelocity, damping);
		
		
		transform.position = newPos;
		
//		lastTargetPosition = target.position;		
		lastTargetPosition = new Vector3(target.position.x,0,0);
	}
}
