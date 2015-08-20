// ----------- CAR TUTORIAL SAMPLE PROJECT, ? Andrew Gotow 2009 -----------------

// Here's the basic car script described in my tutorial at www.gotow.net/andrew/blog.
// A Complete explaination of how this script works can be found at the link above, along
// with detailed instructions on how to write one of your own, and tips on what values to 
// assign to the script variables for it to work well for your application.

// Contact me at Maxwelldoggums@Gmail.com for more information.




// These variables allow the script to power the wheels of the car.
var FrontLeftWheel : WheelCollider;
var FrontRightWheel : WheelCollider;
var RearLeftWheel : WheelCollider;
var RearRightWheel : WheelCollider;

// These variables are for the gears, the array is the list of ratios. The script
// uses the defined gear ratios to determine how much torque to apply to the wheels.
var GearRatio : float[];
var CurrentGear : int = 0;

// These variables are just for applying torque to the wheels and shifting gears.
// using the defined Max and Min Engine RPM, the script can determine what gear the
// car needs to be in.
var EngineTorque : float = 1000.0;
var BrakeTorgue : float = 400;
var MaxEngineRPM : float = 3000.0;
var MinEngineRPM : float = 1000.0;
private var EngineRPM : float = 0.0;
var dir : Vector3 = Vector3.zero;
private var AccelerateVal: int;
var value1 : float = 0;
var petrolTime : float;
var health_green : GUITexture;
var health_red : GUITexture;
///////////////
var barDisplay : float = 0;
var pos : Vector2 = new Vector2(20,40);
var size : Vector2 = new Vector2(128,16);
var progressBarEmpty : Texture2D;
var progressBarFull : Texture2D;
var lifeCheck : boolean = false;
var gameOverMenu : GameObject;
var pauseButton : GameObject;
var levelComplete : GameObject;
var min : int;
var min1 : int;
var sec : int;
var sec1 : int;
var timecount : float;
var starttime : float;
var timeCounter : GUIText;
var bestTimeShow : GUIText;

/////////////////

function Start () {
	// I usually alter the center of mass to make the car more stable. I'ts less likely to flip this way.
	rigidbody.centerOfMass += Vector3(0, -1.5, .1);  // more stable
	PlayMusic();
	AccelerateVal=30;
	petrolTime = 0;
	Time.timeScale = 1;
	if(play.is_play){
	starttime = Time.time;
	}
//	pos = new Vector2(400,400);
//	size = new Vector2(128,32);
	besttime=PlayerPrefs.GetFloat("besttime");
}

//function OnGUI()
//{
//
//    // draw the background:
//    GUI.BeginGroup (new Rect (pos.x, pos.y, size.x, size.y));
//        GUI.Box (Rect (0,0, size.x, size.y),progressBarEmpty);
// 
//     // draw the filled-in part:
//        GUI.BeginGroup (new Rect (0, 0, (progressBarFull.width/2)-size.x * barDisplay, size.y));
//            GUI.Box(Rect (0,0, size.x, size.y),progressBarFull);
//        GUI.EndGroup ();
// 
//    GUI.EndGroup ();
// 
//} 
private var besttime:float;
function Update () {

		
		timecount = Time.time - starttime;
		min = (timecount/60f);
		sec = (timecount % 60f);
//		timeCounter.text = String.Format("{0:00}:{1:00}",min,sec);

//		barDisplay = petrolTime * 0.1;
//
//		petrolTime += Time.deltaTime;
//		print("petrolTime" + petrolTime);
//		health_green.transform.position.x -= 0.01*Time.deltaTime/petrolTime;

		min1 = (besttime/60f);
		sec1 = (besttime % 60f);
//		bestTimeShow.text = String.Format("{0:00}:{1:00}",min1,sec1);
	
		if (Input.touchCount>0) {
		for (var touch : Touch in Input.touches) {
		if(touch.phase==TouchPhase.Stationary){
			
				if (touch.position.x<Screen.width/2) {
					value1 = -1.5;
				}
			    else if(touch.position.x>Screen.width/2){
			    	value1 = 1.5;
			    }
			    
			  
		}
		else 
		{
			value1=0.005;
		}
		FrontLeftWheel.motorTorque = (EngineTorque / GearRatio[CurrentGear])* value1;
		FrontRightWheel.motorTorque = (EngineTorque / GearRatio[CurrentGear])* value1;
		
      
      }
	}
	
	
	// This is to limith the maximum speed of the car, adjusting the drag probably isn't the best way of doing it,
	// but it's easy, and it doesn't interfere with the physics processing.
	rigidbody.drag = rigidbody.velocity.magnitude / 250;
	
	// Compute the engine RPM based on the average RPM of the two wheels, then call the shift gear function
	EngineRPM = (FrontLeftWheel.rpm + FrontRightWheel.rpm)/2 * GearRatio[CurrentGear];
	ShiftGears();

	// set the audio pitch to the percentage of RPM to the maximum RPM plus one, this makes the sound play
	// up to twice it's pitch, where it will suddenly drop when it switches gears.
	audio.pitch = Mathf.Abs(EngineRPM / MaxEngineRPM) + .35 ;
	// this line is just to ensure that the pitch does not reach a value higher than is desired.
	if ( audio.pitch > 1 ) {
		audio.pitch = 1;
	}

	// finally, apply the values to the wheels.	The torque applied is divided by the current gear, and
	// multiplied by the user input variable.
//	if(petrolTime>=10){
//	EngineTorque = 0;
//	
//	} else {
//	EngineTorque = 400;
//	
//	}
	FrontLeftWheel.motorTorque = EngineTorque / GearRatio[CurrentGear] * Input.GetAxis("Vertical");
	FrontRightWheel.motorTorque = EngineTorque / GearRatio[CurrentGear] * Input.GetAxis("Vertical");
//	
	if(HandBrake.is_handbreak){
	RearRightWheel.brakeTorque = 500;
	RearLeftWheel.brakeTorque = 500;

	}else{
	RearRightWheel.brakeTorque = 0;
	RearLeftWheel.brakeTorque = 0;
	
	}
//		
//	// the steer angle is an arbitrary value multiplied by the user input.
	FrontLeftWheel.steerAngle = 16 * Input.GetAxis("Horizontal");
	FrontRightWheel.steerAngle = 16 * Input.GetAxis("Horizontal");
	
	///////////////////////////////
	// for mobile 
//	dir.x = Input.acceleration.x*AccelerateVal*Time.deltaTime/2;
//	FrontLeftWheel.steerAngle = AccelerateVal * dir.x;
//  	FrontRightWheel.steerAngle = AccelerateVal * dir.x;
    
}

function ShiftGears() {
	// this funciton shifts the gears of the vehcile, it loops through all the gears, checking which will make
	// the engine RPM fall within the desired range. The gear is then set to this "appropriate" value.
	if ( EngineRPM >= MaxEngineRPM ) {
		var AppropriateGear : int = CurrentGear;
		
		for ( var i = 0; i < GearRatio.length; i ++ ) {
			if ( FrontLeftWheel.rpm * GearRatio[i] < MaxEngineRPM ) {
				AppropriateGear = i;
				break;
			}
		}
		
		CurrentGear = AppropriateGear;
	}
	
	if ( EngineRPM <= MinEngineRPM ) {
		AppropriateGear = CurrentGear;
		
		for ( var j = GearRatio.length-1; j >= 0; j -- ) {
			if ( FrontLeftWheel.rpm * GearRatio[j] > MinEngineRPM ) {
				AppropriateGear = j;
				break;
			}
		}
		
		CurrentGear = AppropriateGear;
	}
}

function OnTriggerEnter(col : Collider){
if(col.transform.tag == "gameOver"){
yield WaitForSeconds(3);
gameOverMenu.active = true;
pauseButton.active = false;
Time.timeScale = 0;
StopMusic();
}
else if(col.transform.tag == "finish"){


levelComplete.active = true;
pauseButton.active = false;
Time.timeScale = 0;
StopMusic();

besttime=PlayerPrefs.GetFloat("besttime");


if(besttime==0)
{
besttime=timecount;
PlayerPrefs.SetFloat("besttime",besttime);

}
else
{
if(timecount<besttime)
{
besttime=timecount;
PlayerPrefs.SetFloat("besttime",besttime);

}
}
min = (besttime/60f);
sec = (besttime % 60f);
bestTimeShow.text = String.Format("{0:00}:{1:00}",min,sec);

}
}

//function OnTriggerEnter(col : Collider){
//if(col.transform.tag == "gameOver"){
//petrolTime = 0;
//barDisplay = 0;
//Destroy(col.gameObject);
//}
//}
function PlayMusic(){
if(Sound.is_Sound_On){
	audio.Play();
	
	}

}
function StopMusic(){

	audio.Stop();
	


}







