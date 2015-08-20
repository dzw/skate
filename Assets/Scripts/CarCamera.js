#pragma strict

private var car: Transform;
private var centerPoint:Transform;
var distance: float= 6.4;
var height: float = 1.4;
var rotationDamping: float = 3.0;
var heightDamping: float = 2.0;
var zoomRacio : float =0.5;
var defaultFov	:float=60;
private var pauseGame:boolean=false;
private var rotationVector : Vector3;
static var gameRunning:boolean=false;
private var startGame=false;

var GOList:GameObject[];

function Start () 
{
	gameRunning=false;
	makeTrue();
	setCarCamera();
	
}

function LateUpdate () 
{
	
	if(!startGame)
	{
		transform.LookAt(car.transform);
	
    transform.Translate(Vector3.right * Time.deltaTime);
    
    }
    else
    {
	
	if(!pauseGame)
	{
	var wantedAngel  =	rotationVector.y;
	var wantedHeight =	car.transform.position.y+height;
	var myAngel  =	transform.eulerAngles.y;
	var myHeight =	transform.position.y;
	
	myAngel	= 	Mathf.LerpAngle(myAngel,wantedAngel,rotationDamping*Time.deltaTime);
	myHeight	=	Mathf.Lerp(myHeight,wantedHeight,heightDamping*Time.deltaTime);
	
	var currentRotation	=	Quaternion.Euler(0,myAngel,0);
	transform.position 	=	car.position;
	transform.position -=	currentRotation*Vector3.forward*distance;
	transform.position.y=myHeight;
	transform.LookAt(centerPoint);
	
	}
	}
	
	

}


function FixedUpdate()
{

var localVilocity=car.InverseTransformDirection(car.rigidbody.velocity);
if(localVilocity.z<-0.5)
{
	//rotationVector.y=car.eulerAngles.y+180;
	
}
else
{
	rotationVector.y=car.eulerAngles.y;

}
		var acc	=  car.rigidbody.velocity.magnitude;
		camera.fieldOfView=defaultFov+acc*zoomRacio;
}

function Pause()
{
	pauseGame=true;
}

function Resume()
{
	pauseGame=false;
}

function makeTrue()
{
	yield WaitForSeconds(40);
	startGame=true;
	gameRunning=true;
}

function Update()
{
	if(Input.GetMouseButtonDown(0))
	{
		StopCoroutine("makeTrue");
		startGame=true;
		gameRunning=true;
	}
	
} 

function setCarCamera()
{
	
	switch(PlayerPrefs.GetInt("CarNumber"))
	{
		case 0:
				car=GOList[0].transform;
				distance=7;
				height=2.4;
				centerPoint=GOList[0].transform.FindChild("Focus Point");
				break;
		case 1:
				car=GOList[1].transform;
				distance=8.2;
				height=3.5;
				centerPoint=GOList[1].transform.FindChild("Focus Point");
				break;
		case 2:
				car=GOList[2].transform;
				distance=8;
				height=3.2;
				centerPoint=GOList[2].transform.FindChild("Focus Point");
				break;
		case 3:
				car=GOList[3].transform;
				distance=8;
				height=2.5;
				centerPoint=GOList[3].transform.FindChild("Focus Point");
				break;
		case 4:
				car=GOList[4].transform;
				distance=8;
				height=2.6;
				centerPoint=GOList[4].transform.FindChild("Focus Point");
				break;
		case 5:
				
				car=GOList[5].transform;
				distance=8;
				height=2.6;
				centerPoint=GOList[5].transform.FindChild("Focus Point");
				break;
		
	
	}
	
	
}
