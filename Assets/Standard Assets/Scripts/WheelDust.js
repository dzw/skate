// Define the variables used in the script, the Corresponding collider is the wheel collider at the position of
// the visible wheel, the slip prefab is the prefab instantiated when the wheels slide, the rotation value is the
// value used to rotate the wheel around it's axel.
var CorrespondingCollider : WheelCollider;
var SlipPrefab : GameObject;
public var RotationValue : float = 0.0;

function Update () {

	// define a hit point for the raycast collision
	var hit : RaycastHit;
	// Find the collider's center point, you need to do this because the center of the collider might not actually be
	// the real position if the transform's off.
	var ColliderCenterPoint : Vector3 = CorrespondingCollider.transform.TransformPoint( CorrespondingCollider.center );

	
	// define a wheelhit object, this stores all of the data from the wheel collider and will allow us to determine
	// the slip of the tire.
	var CorrespondingGroundHit : WheelHit;
	CorrespondingCollider.GetGroundHit( CorrespondingGroundHit );
	
	// if the slip of the tire is greater than 2.0, and the slip prefab exists, create an instance of it on the ground at
	// a zero rotation.
	if ( Mathf.Abs( CorrespondingGroundHit.sidewaysSlip ) > .15 ) {
		if ( SlipPrefab ) {
			Instantiate( SlipPrefab, CorrespondingGroundHit.point, Quaternion.identity );
		}
	}
	
}