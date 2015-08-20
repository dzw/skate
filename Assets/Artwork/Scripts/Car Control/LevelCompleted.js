#pragma strict
static var star = 0;

var gui : GameObject;
var yourTime : GUIText;
var bestTimeTxt : GUIText;
var bestTimeThisLevel : String;
var playerScript : PlayerCar_Script;
var currLevel : int ;
function Start () {
	gui.active = false;
	CalculateBestTime();
	
	
	yield WaitForSeconds(0.5);
//	AirScript.startAirSmartWallAd();
	Time.timeScale = 0;
//	Debug.Log(""+Gallery.scenIndex+2);
	
	
}

function Update () {

}
function CalculateBestTime(){
	var bestTime;
	var min = PlayerPrefs.GetInt("min"+currLevel);
	var sec = PlayerPrefs.GetInt("sec"+currLevel);
	
	var currentMin : int = Mathf.Floor(PlayerCar_Script.timer / 60);
	var currentSec : int = PlayerCar_Script.timer % 60;
	
//	Debug.Log("currentMin="+min+" currentSec="+currentSec);
	if((min==0 && sec == 0) || currentMin< min ){
		PlayerPrefs.SetInt("min"+currLevel,currentMin);
		PlayerPrefs.SetInt("sec"+currLevel,currentSec);
		min = currentMin;
		sec = currentSec;
	}
	else if(currentMin== min && currentSec<=sec){
		PlayerPrefs.SetInt("min"+currLevel,currentMin);
		PlayerPrefs.SetInt("sec"+currLevel,currentSec);
		min = currentMin;
		sec = currentSec;
	}
	else if(currentMin<min && currentSec<=sec){
		PlayerPrefs.SetInt("min"+currLevel,currentMin);
		PlayerPrefs.SetInt("sec"+currLevel,currentSec);
		min = currentMin;
		sec = currentSec;
	}
   	
   	bestTime  = (min).ToString("00")+":"+(sec).ToString("00");
   	var curTime = (currentMin).ToString("00")+":"+(currentSec).ToString("00");
   	
   	bestTimeTxt.text = "Best Time              "+bestTime;   
	yourTime.text = "Your Time             "+curTime;
	
//	Debug.Log("currentMin="+min+" currentSec="+bestTime);
	 
	
}
