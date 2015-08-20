
static var Go:boolean=false;
var FrontLeftWheel : WheelCollider;
var FrontRightWheel : WheelCollider;
var RareLeftWheel : WheelCollider;
var RareRightWheel : WheelCollider;
private var Value :float=1;
var myCar:GameObject;


// These variables are for the gears, the array is the list of ratios. The script
// uses the defined gear ratios to determine how much torque to apply to the wheels.
var GearRatio : float[];
private var CurrentGear : int = 0;

// These variables are just for applying torque to the wheels and shifting gears.
// using the defined Max and Min Engine RPM, the script can determine what gear the
// car needs to be in.
 var EngineTorque : float = 3000.0;
private var MaxEngineRPM : float = 3000.0;
private var MinEngineRPM : float = 1000.0;
private var EngineRPM : float = 0.0;


private var a:int;
private var time:int;
//var textFont:Font;


//Vechile Time
private var min:int;
private var sec:int;
private var fraction:int;

private var gameFinished:boolean=false;

private var isPause:boolean=false;


///CheckPoint Variables
var checkPointArray : Transform[]; //Checkpoint GameObjects stored as an array
private static public var currentCheckpoint : int = 0; //Current checkpoint
private static public var currentLap : int = 0; //Current lap
private static public var startPos : Vector3; //Starting position
//private var point:checkPoints;
static  var carPositionIndicator:int=0;
static  var carPosition:int=0;

 var throttle:float=0.0;



//Variables for checking wheels are grounded or not

private var hasWheelContact = false;
var frontLeftWheel : Transform;
var frontRightWheel : Transform; 
var backLeftWheel : Transform; 
var backRightWheel : Transform; 


private var finishedTime;


//private var AICar:AI_Script;
//private var AICar2:AI_Script1;
//private var AICar3:AI_Script2;
//private var AICar4:AI_Script3;

static var gamePause:boolean=false;

private var checkNumber:float; 

///Distance Variables




///////// for GUI Scaling

private var originalWidth = 1024.0;  // define here the original resolution
private var originalHeight = 768.0;
private var scale: Vector3;






/////////////////////////////////////////////////////// Start Function ///////////////////////////////////////////////



function Start () 
{
	
	
	 //Go=false;
		 Go=true;
		  a=Time.time;
		  startPos = transform.position;
		 
	  	
	  		 
	
				
}






////////////////////////////////////////////////////// Update Function ////////////////////////////////////////////////

function Update () 
{
	 /*
	if(!CarCamera.gameRunning)
		{
			a=Time.time;
			return;
	
		}	
	else
		{
			if(!Go)	
			{
				time=Time.time-a;
				//Text.text=time.ToString();
			
				if(time==4)
					//Text.text="Go!";
				
				if(time==5)
					{
					//	Text.text="";
						Go=true;
						//Music.car_SoundPitch=0.0;
						//myMusic.SendMessage("adjustCarFX");
					}
			
			}*/
		
	if(Go)
		{
			
			//Timer Code
//			timeCount=Time.time-time-a;
//			min=(timeCount/60f);
//			sec=(timeCount%60f);
//			fraction=((timeCount*100)%100f);
		
		if(!gameFinished)
			{
				//timeCounter.text=String.Format("{0:00}:{1:00}:{2:00}",min,sec,fraction);
			}
			//else
				//timeCounter.text="";
		
			//End Timer Code
			var dir : Vector3 = Vector3.zero;
			
			//dir.x = -Input.acceleration.y;
		
			
			// Make it move 10 meters per second instead of 10 meters per frame...
			//dir *= Time.deltaTime;
			dir.x = Input.acceleration.x*40*Time.deltaTime/5;	
			
			//dir.x = Input.acceleration.x*Time.deltaTime;
			// This is to limith the maximum speed of the car, adjusting the drag probably isn't the best way of doing it,
			// but it's easy, and it doesn't interfere with the physics processing.
			rigidbody.drag = rigidbody.velocity.magnitude /250;
	
			// Compute the engine RPM based on the average RPM of the two wheels, then call the shift gear function
			EngineRPM = (FrontLeftWheel.rpm + FrontRightWheel.rpm)/2 * GearRatio[CurrentGear];
			ShiftGears();
			// set the audio pitch to the percentage of RPM to the maximum RPM plus one, this makes the sound play
			// up to twice it's pitch, where it will suddenly drop when it switches gears.
			
			
			// this line is just to ensure that the pitch does not reach a value higher than is desired.
//				if (Music.car_SoundPitch > 4.0 ) 
//				{
//					Music.car_SoundPitch = 4.0;
//					
//					}
					
			// finally, apply the values to the wheels.	The torque applied is divided by the current gear, and
			// multiplied by the user input variable.
			FrontLeftWheel.motorTorque = EngineTorque / GearRatio[CurrentGear] * .950;
			FrontRightWheel.motorTorque = EngineTorque / GearRatio[CurrentGear] * .950;
			
			// the steer angle is an arbitrary value multiplied by the user input.
			
			
			
			FrontLeftWheel.motorTorque = EngineTorque / GearRatio[CurrentGear] * Value;
			FrontRightWheel.motorTorque = EngineTorque / GearRatio[CurrentGear] *Value;

			// the steer angle is an arbitrary value multiplied by the user input.
			
		
			
//					if(Application.platform!=RuntimePlatform.IPhonePlayer)
//					{
//						
//					}
				}
			

		}
//	}
	
		




///////////////////////////////////////////////////   Shift Gears /////////////////////////////////////////////////////





function ShiftGears() 
{
	// this funciton shifts the gears of the vehcile, it loops through all the gears, checking which will make
	// the engine RPM fall within the desired range. The gear is then set to this "appropriate" value.
	if ( EngineRPM >= MaxEngineRPM ) 
	{
			var AppropriateGear : int = CurrentGear;
		
		for ( var i = 0; i < GearRatio.length; i ++ ) {
			if ( FrontLeftWheel.rpm * GearRatio[i] < MaxEngineRPM )
			 {
					AppropriateGear = i;
					break;
			}
		}
		
		CurrentGear = AppropriateGear;
	}
	
	if ( EngineRPM <= MinEngineRPM ) 
	{
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



///////////////////////////////////////////////////   On GUI  /////////////////////////////////////////////////////

function OnGUI()
{
	

}


function StopCar()
{
			 

			
}



///Check Weather wheels are grounded or Nor

function OnCollisionStay (collision : Collision) 
{ 
	
	for (var p : ContactPoint in collision.contacts)
 		{
			if (p.thisCollider.transform == frontLeftWheel) hasWheelContact = true;
			if (p.thisCollider.transform == frontRightWheel) hasWheelContact = true; 
			if (p.thisCollider.transform == backLeftWheel) hasWheelContact = true;
			if (p.thisCollider.transform == backRightWheel) hasWheelContact = true;
		
		}
				

}
function OnCollisionExit(collisionInfo : Collision)
{
	
	hasWheelContact=false;
	
}



	////// Corutine for Activity Indicator
	
function IEnumerator()
    {
       
         #if UNITY_IPHONE
            Handheld.SetActivityIndicatorStyle(iOSActivityIndicatorStyle.White);
        #elif UNITY_ANDROID
            Handheld.SetActivityIndicatorStyle(AndroidActivityIndicatorStyle.Small);
        #endif
        Handheld.StartActivityIndicator();
        yield WaitForSeconds(0);
        Application.LoadLevel(0);
       
     }
     
 



