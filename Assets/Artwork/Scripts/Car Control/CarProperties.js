#pragma strict

var body : GameObject[];
var wheels : WheelCollider[];

var wheelsBody : GameObject[];
function Start () {
	yield WaitForSeconds(0.1);
	if(body[0].active){
		
		FirstCar();
	}
	else if(body[1].active){
		SecondCar();
	}
	else if(body[2].active){
		ThirdCar();
	}
	else if(body[3].active){
		FourthCar();
	}
}

function Update () {

}
function Awake(){


}

function FirstCar(){
	gameObject.rigidbody.mass = 3500;
	PlayerCar_Script.EngineTorque = 140;
	PlayerCar_Script.MaxEngineRPM = 4000;
	PlayerCar_Script.MinEngineRPM = 2000;
	for(var i=0; i<wheels.length;i++){
		wheels[i].suspensionDistance = 0.9;
		wheels[i].suspensionSpring.damper = 500;
		
		}
	gameObject.rigidbody.angularDrag = 0.1;
}

function SecondCar(){
// balance , speed, and suspension inproved
	gameObject.rigidbody.mass = 5000;
	PlayerCar_Script.EngineTorque = 150;
	PlayerCar_Script.MaxEngineRPM = 4000;
	PlayerCar_Script.MinEngineRPM = 1000;

	for(var i=0; i<wheels.length;i++){
		wheels[i].suspensionDistance = 0.8;
		wheels[i].suspensionSpring.damper = 500;
//		wheels[i].radius = 0.8;
//		wheelsBody[i].transform.localScale.x = 0.75;
//		wheelsBody[i].transform.localScale.y = 0.75;
//		wheelsBody[i].transform.localScale.z = 0.75;
		}
	
	gameObject.rigidbody.angularDrag = 0.2;
//	Debug.Log("fdsfsdfffsdfsffdsdsfsdfdsf");
}
function ThirdCar(){

	gameObject.rigidbody.mass = 5500;
	PlayerCar_Script.EngineTorque = 165;
	PlayerCar_Script.MaxEngineRPM = 4000;
	PlayerCar_Script.MinEngineRPM = 1000;
	for(var i=0; i<wheels.length;i++){
		wheels[i].suspensionDistance = 0.7;
		wheels[i].suspensionSpring.damper = 800;
		wheels[i].radius = 0.6;
		wheelsBody[i].transform.localScale.x = 0.6;
		wheelsBody[i].transform.localScale.y = 0.6;
		wheelsBody[i].transform.localScale.z = 0.6;
		}
	
	gameObject.rigidbody.angularDrag = 0.1;
}
function FourthCar(){

	gameObject.rigidbody.mass = 6500;
	PlayerCar_Script.EngineTorque = 175;
	for(var i=0; i<wheels.length;i++){
		wheels[i].suspensionDistance = 0.7;
		wheels[i].suspensionSpring.damper = 800;
		wheels[i].radius = 0.65;
		wheelsBody[i].transform.localScale.x = 0.65;
		wheelsBody[i].transform.localScale.y = 0.65;
		wheelsBody[i].transform.localScale.z = 0.65;
		
		}
	
	gameObject.rigidbody.angularDrag = 0.4;
}