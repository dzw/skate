// ----------- CAR TUTORIAL SAMPLE PROJECT, ? Andrew Gotow 2009 -----------------

// Here's the basic car script described in my tutorial at www.gotow.net/andrew/blog.
// A Complete explaination of how this script works can be found at the link above, along
// with detailed instructions on how to write one of your own, and tips on what values to 
// assign to the script variables for it to work well for your application.

// Contact me at Maxwelldoggums@Gmail.com for more information.




// These variables allow the script to power the wheels of the car.
var FrontLeftWheel : WheelCollider;
var FrontRightWheel : WheelCollider;
var BackLeftWheel : WheelCollider;
var BackRightWheel : WheelCollider;
// These variables are for the gears, the array is the list of ratios. The script
// uses the defined gear ratios to determine how much torque to apply to the wheels.
var GearRatio : float[];
var CurrentGear : int = 0;

// These variables are just for applying torque to the wheels and shifting gears.
// using the defined Max and Min Engine RPM, the script can determine what gear the
// car needs to be in.
static var EngineTorque : float = 0;
static var MaxEngineRPM : float = 0;
static var MinEngineRPM : float = 0;
private var EngineRPM : float = 0.0;

var speed : float = 2;
var moveThreshold : float = 0.2;
 
private var movex : float; 
private var iPx : float; 

var score : GUIText;
var coins : GUIText;
var sound : AudioClip;
var fuel_sound : AudioClip;
var collectedCoins : int = 0;
var spark : GameObject;
var sparkDestroy : GameObject;
var levelCompleted : GameObject;
var cars : GameObject[];
var myTime:float ;
private var finish = false;
static var timer : float;
var timerText : GUIText;

var yMinLimit = -360;
var yMaxLimit = 360;
private var y = 0.0;
private var tiltSpeed = 6.0;
var airTime : GUIText; var inTheAir = false;
var jumpTime : float;
var goldCoins :GameObject[];
var bestMinute : int;
var bestSeconds: int;
var bestSeconds2Stars : int;
var noseSpark: Transform;
static var tiltValue : int ;  
function Start () {
	// I usually alter the center of mass to make the car more stable. I'ts less likely to flip this way.
	 timer = 0;
	 tiltValue = 7;
//	 EngineTorque = 0;
	 rigidbody.centerOfMass.x = -1.5;
	 rigidbody.centerOfMass.y = -1.5;
	 rigidbody.centerOfMass.z = -1.5;
	 audio.clip = sound;
	 collectedCoins =  PlayerPrefs.GetInt("coins");
	 coins.text = ""+collectedCoins;
	 Time.timeScale = 1;
	 for(var i =0; i<cars.length;i++){
	 	cars[i].active = false;
	 }
	cars[Menu.carIndex].active = true;
	Crash.game_over = false;
//	
//	Debug.Log(""+Application.loadedLevelName);
}
var isReady ;
function Update () {
	

//	Debug.Log(gameObject.transform.eulerAngles+"angle");
	Rotate();
	Timer();
	if(isReady){
		Nitrous ();
	}
	
	// This is to limith the maximum speed of the car, adjusting the drag probably isn't the best way of doing it,
	// but it's easy, and it doesn't interfere with the physics processing.
	rigidbody.drag = rigidbody.velocity.magnitude / 250;
	
	// Compute the engine RPM based on the average RPM of the two wheels, then call the shift gear function
	EngineRPM = (FrontLeftWheel.rpm + FrontRightWheel.rpm)/2 * GearRatio[CurrentGear];
	ShiftGears();
	
	// set the audio pitch to the percentage of RPM to the maximum RPM plus one, this makes the sound play
	// up to twice it's pitch, where it will suddenly drop when it switches gears.
//	audio.pitch = Mathf.Abs(EngineRPM / MaxEngineRPM) + 1.0 ;
	// this line is just to ensure that the pitch does not reach a value higher than is desired.
//	if ( audio.pitch > 2.0 ) {
//		audio.pitch = 2.0;
//	}

	// finally, apply the values to the wheels.	The torque applied is divided by the current gear, and
	// multiplied by the user input variable.
	if(!Crash.game_over){
//	Debug.Log("fin"+Crash.game_over);
		FrontLeftWheel.motorTorque = EngineTorque / GearRatio[CurrentGear] * Input.GetAxis("Vertical");
		FrontRightWheel.motorTorque = EngineTorque / GearRatio[CurrentGear] * Input.GetAxis("Vertical");
		// flip the car
	//	Debug.Log(FrontLeftWheel.rpm+"dd");
		Speedo.speed = FrontLeftWheel.rpm;
		
	}
	else {
	 	FrontLeftWheel.brakeTorque = 100;
	 	FrontRightWheel.brakeTorque = 100;
	 	}
			
	// the steer angle is an arbitrary value multiplied by the user input.
//	FrontLeftWheel.steerAngle = 10 * Input.GetAxis("Horizontal");
//	FrontRightWheel.steerAngle = 10 * Input.GetAxis("Horizontal");

//		if(accelerate){
//			FrontLeftWheel.motorTorque = EngineTorque / GearRatio[CurrentGear]*1 ;
//			FrontRightWheel.motorTorque = EngineTorque / GearRatio[CurrentGear]*1 ;
//		}
//		else if(back)
//		{
//			FrontLeftWheel.motorTorque = EngineTorque / GearRatio[CurrentGear]*-1 ;
//			FrontRightWheel.motorTorque = EngineTorque / GearRatio[CurrentGear]*-1 ;
//		}
			if(!Crash.game_over){
			if (Input.touchCount>0 && !finish) {
			  for (var touch : Touch in Input.touches) {
			  if(touch.phase==TouchPhase.Stationary){
			   
			    if (touch.position.x<Screen.width/2) {
				    FrontLeftWheel.motorTorque = EngineTorque / GearRatio[CurrentGear]*-1 ;
					FrontRightWheel.motorTorque = EngineTorque / GearRatio[CurrentGear]*-1 ;
			    }
			       else if(touch.position.x>Screen.width/2){
        			
					FrontLeftWheel.motorTorque = EngineTorque / GearRatio[CurrentGear]*1 ;
					FrontRightWheel.motorTorque = EngineTorque / GearRatio[CurrentGear]*1 ;
			       }
			       
			     
			  }
			  
			  
			      
			      }
			 }
			 }
			 else 
			 	EngineTorque = 0;
			//exit
			if(Input.GetKeyDown(KeyCode.Escape)){
				Application.LoadLevel("MainMenu");
			
			}
	InAirTime();
//	Debug.Log(""+EngineRPM );
}

function Rotate(){
//    var iphonez = Input.acceleration.x;
//    transform.Rotate(Vector3.right*iphonez*10);
//
	if( !Crash.game_over && Time.timeScale> 0){	
	    iPx = iPhoneInput.acceleration.x;
	
	  if (Mathf.Abs(iPx) > moveThreshold){
	 	var tiltSides = Input.acceleration.x;
	    tiltSides = Mathf.Clamp(tiltSides, yMinLimit, yMaxLimit);
	    transform.Rotate(Vector3.right * tiltSides *tiltValue );
//	    Debug.Log(Vector3.right * tiltSides * 20+"xxx");
	    }
	    }

}

function Nitrous () {
//    audio.PlayOneShot(activateSound, 1);
//    isReady = false;x
    gameObject.rigidbody.AddForce(transform.forward * 500, ForceMode.Acceleration);
    
    yield WaitForSeconds(0.09);
    isReady = false;
    
 
}
function FixedUpdate () {
//		rigidbody.AddTorque (Vector3.up * 10);
	
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
function OnTriggerEnter (col : Collider) {
		
		var _time:float  = 0.75;
		if(!Crash.game_over){
		if(col.gameObject.tag == "5"||col.gameObject.tag == "25"||col.gameObject.tag == "100"||col.gameObject.tag == "500"){
			col.gameObject.rigidbody.AddForce(0,300,0);
			col.gameObject.collider.enabled  = false;
	        Destroy(col.gameObject,0.75);
	        sparkDestroy = Instantiate(spark,col.transform.position,Quaternion.identity);
	        Destroy(sparkDestroy,1);
	       	collectedCoins +=int.Parse(col.gameObject.tag);
	       	coins.text = ""+collectedCoins;
	       	score.text = "+"+col.gameObject.tag;
	       	PlayerPrefs.SetInt("coins",collectedCoins);
	        audio.PlayClipAtPoint(sound, Vector3 (5, 1, 2)); 
	        if(score.active){
	        	score.active = false;
	        	yield WaitForSeconds(0.1);
	        	score.active = true;
	        	yield WaitForSeconds(1.5);
	        	score.active = false;
	        	}
	        	
	        else{
		        score.active = true;
	        	yield WaitForSeconds(1);
	        	score.active = false;
		        }
	        	
        	}
        	
        else if(col.gameObject.tag == "finish"){
        		finish = true;
        		var val = PlayerPrefs.GetInt("LevelUnlocked");
        		if(Gallery.scenIndex==val){
	        		 val +=1;
	        		 PlayerPrefs.SetInt("LevelUnlocked",val);
        		 
        		 }
        	    Crash.game_over = true;		
        		levelCompleted.active = true;
//        		var minutes = Mathf.Floor(timer / 60);
//				var seconds = (timer % 60);
//        		if(minutes<bestMinute || minutes==bestMinute && seconds<=bestSeconds){
//        			LevelCompleted.star =3;
//        		}
//        		else if(minutes==bestMinute && seconds>bestSeconds && seconds<=bestSeconds2Stars){
//					LevelCompleted.star =2;
//        		}
//        		else 
//        			LevelCompleted.star =1;
        		
        	}
        	
        else if(col.gameObject.tag == "fuel"){
        		Healthbar.healthWidth = 199;
        		Time.timeScale =1;
        		Destroy(col.gameObject);
        		audio.PlayClipAtPoint(fuel_sound, Vector3 (5, 1, 2));
        	
        	}
    	else if(col.gameObject.tag == "booster" ){
				isReady = true;
			  	Instantiate(noseSpark,col.gameObject.transform.position,Quaternion.identity);
			  	audio.PlayClipAtPoint(fuel_sound, Vector3 (5, 1, 2));
			}
	}

    }
  

    function Timer(){
    			
    			timer += Time.deltaTime;	
		    	 var minutes = Mathf.Floor(timer / 60).ToString("00");
				 var seconds = (timer % 60).ToString("00");
			   timerText.text = minutes+":"+seconds;
//			   Debug.Log(""+(timer % 60).ToString("00"));
    }
  
function OnCollisionEnter(col: Collision){

//	Debug.Log(col.gameObject.name);
}
function InAirTime(){
	if(!inTheAir){
		if(!FrontLeftWheel.isGrounded && !FrontRightWheel.isGrounded && !BackLeftWheel.isGrounded && !BackRightWheel.isGrounded){
					
						inTheAir = true;
						
						jumpTime = timer % 60;
//						Debug.Log(jumpTime+"=TIme1");
					}
					
				
	}
	 if(inTheAir){
	 	if(FrontLeftWheel.isGrounded && FrontRightWheel.isGrounded && BackLeftWheel.isGrounded && BackRightWheel.isGrounded){
	 			inTheAir = false;
//	 			Debug.Log(timer % 60+"=TIme2");
	 			jumpTime = timer%60 - jumpTime;
//	 			Debug.Log(jumpTime+"=TIme3");
//	 			var temp : int = jumpTime;
				
	 			if(jumpTime>1.5 && jumpTime <2){
	 				ShowAirTime();
	 				airTime.text = "Air Time\n+500";
	 				collectedCoins+= 500;
 					PlayerPrefs.SetInt("coins",collectedCoins);
 					audio.PlayClipAtPoint(fuel_sound, Vector3 (5, 1, 2));
 					
	 				}
 				else if(jumpTime>=2 && jumpTime<2.5){
 					ShowAirTime();
 					airTime.text = "Air Time\n+275";
 					collectedCoins+= 275;
					PlayerPrefs.SetInt("coins",collectedCoins);
					audio.PlayClipAtPoint(fuel_sound, Vector3 (5, 1, 2));
 					}
				else if(jumpTime>=2.5){
					ShowAirTime();
 					airTime.text = "Air Time\n+100";
 					collectedCoins+= 150;
					PlayerPrefs.SetInt("coins",collectedCoins);
					audio.PlayClipAtPoint(fuel_sound, Vector3 (5, 1, 2));
 					}	
 				
	 			
	 			
				
	 				
	 	}
	 }
	
}
    
function ShowAirTime(){
			
		 			airTime.active = true;
					yield WaitForSeconds(1.5);
					airTime.active = false;
			

}