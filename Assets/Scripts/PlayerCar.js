
///////// for GUI Scaling

private var originalWidth = 2048.0;  // define here the original resolution
private var originalHeight = 1536.0;
private var scale: Vector3;

var FrontWheel_left : WheelCollider;
var FrontWheel_right : WheelCollider;
var RearWheel_left : WheelCollider;
var RearWheel_right : WheelCollider;

var GearRatio : float[];
var CurrentGear : int = 0;


var EngineTorque : float = 600.0;
var MaxEngineRPM : float = 3000.0;
var MinEngineRPM : float = 1000.0;
var speedFactor:float = 1;
private var AccelerateVal:int;
private var EngineRPM : float = 0.0;

private var zeroAcceleration: Vector3;
private var currentAcceletaion:Vector3;
private var sensH:float = 10;
private var sensV:float = 10;
private var smooth:float = 0.5;
private var GetAxisH:float = 0;
private var GetAxisV:float = 0;

function Start () {
	// I usually alter the center of mass to make the car more stable. I'ts less likely to flip this way.
	AccelerateVal = 40;

	rigidbody.centerOfMass = Vector3(0, 0,0); 
	
	zeroAcceleration = Input.acceleration;
	currentAcceletaion = Vector3.zero;
	
	
}

function Update () {
	
	// This is to limith the maximum speed of the car, adjusting the drag probably isn't the best way of doing it,
	// but it's easy, and it doesn't interfere with the physics processing.
	rigidbody.drag = rigidbody.velocity.magnitude / 240;
	
	// Compute the engine RPM based on the average RPM of the two wheels, then call the shift gear function
	EngineRPM = (FrontWheel_left.rpm + RearWheel_left.rpm)/2 * GearRatio[CurrentGear];
	ShiftGears();
	
//	FrontWheel_left.motorTorque = EngineTorque / GearRatio[CurrentGear] * speedFactor;
	RearWheel_left.motorTorque = EngineTorque / GearRatio[CurrentGear] * speedFactor;

	
	
	var dir : Vector3 = Vector3.zero;
	dir.x = Input.acceleration.x;
        
	currentAcceletaion = Vector3.Lerp(currentAcceletaion , Input.acceleration - zeroAcceleration, Time.deltaTime/smooth);
	GetAxisH = Mathf.Clamp(currentAcceletaion.x * sensH, -1, 1);


	RearWheel_right.motorTorque = EngineTorque / GearRatio[CurrentGear] * Input.GetAxis("Vertical");
	RearWheel_left.motorTorque = EngineTorque / GearRatio[CurrentGear] * Input.GetAxis("Vertical");
	
	// the steer angle is an arbitrary value multiplied by the user input.		
	FrontWheel_right.steerAngle = 12 * Input.GetAxis("Horizontal");
	FrontWheel_left.steerAngle = 12 * Input.GetAxis("Horizontal");
	

	
}

function ShiftGears() {
	// this funciton shifts the gears of the vehcile, it loops through all the gears, checking which will make
	// the engine RPM fall within the desired range. The gear is then set to this "appropriate" value.
	if ( EngineRPM >= MaxEngineRPM ) {
		var AppropriateGear : int = CurrentGear;
		
		for ( var i = 0; i < GearRatio.length; i ++ ) {
			if ( RearWheel_left.rpm * GearRatio[i] < MaxEngineRPM ) {
				AppropriateGear = i;
				break;
			}
		}
		
		CurrentGear = AppropriateGear;
	}
	
	if ( EngineRPM <= MinEngineRPM ) {
		AppropriateGear = CurrentGear;
		
		for ( var j = GearRatio.length-1; j >= 0; j -- ) {
			if ( RearWheel_left.rpm * GearRatio[j] > MinEngineRPM ) {
				AppropriateGear = j;
				break;
			}
		}
		
		CurrentGear = AppropriateGear;
	}
}


