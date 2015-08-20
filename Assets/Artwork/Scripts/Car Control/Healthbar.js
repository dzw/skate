#pragma strict
 
// JavaScript
var yellowTexture : Texture;
var redTexture : Texture;
var greenTexture : Texture;
var foregroundTexture : Texture;
var frameTexture : Texture;
var lowFuel : GameObject;

static var healthWidth: float = 199;
var healthHeight: int = 30;
 
var healthMarginLeft: int = 41;
var healthMarginTop: int = 38;
 
var frameWidth : int = 266;
var frameHeight: int = 65;
 
var frameMarginLeft : int = 10;
var frameMarginTop: int = 10;
var fuelOut : GameObject;

 //bar
private var guiHealth : GameObject;

 function Start(){
  //health bar 
	 guiHealth = GameObject.Find("GUI Health");
     
 	 
    // Set initial value of the health...
 
    // Uncomment the line below and call reduceHealth() in the Update() method to watch health decrease
    healthWidth = 199;
 
    // Uncomment the line below and call increaseHealth() in the Update() method to watch health increase
    // healthWidth = -8;
 }
function OnGUI () {
 
//    GUI.DrawTexture( Rect(frameMarginLeft,frameMarginTop, frameMarginLeft + frameWidth, frameMarginTop + frameHeight), redTexture, ScaleMode.ScaleToFit, true, 0 );
 
    GUI.DrawTexture( Rect(healthMarginLeft,healthMarginTop,healthWidth + healthMarginLeft, healthHeight), foregroundTexture, ScaleMode.ScaleAndCrop, true, 0 );
 
    GUI.DrawTexture( Rect(frameMarginLeft,frameMarginTop, frameMarginLeft + frameWidth,frameMarginTop + frameHeight), frameTexture, ScaleMode.ScaleToFit, true, 0 );
 
}

function Update () {
	reduceHealth();
	if(healthWidth>100){
		foregroundTexture = greenTexture;
		lowFuel.active = false;
	}
	if(healthWidth<100&& healthWidth >10){
	
			foregroundTexture = 	yellowTexture;	
			lowFuel.active = false;
			
	}
	if(healthWidth<10 && healthWidth>-8){
		
			foregroundTexture = 	redTexture;	
			lowFuel.active = true;
			
	}
	
  if(healthWidth<-8){
   
   			LowFuel();
  }
}

function LowFuel(){
				
				
    			yield WaitForSeconds(0.5);
        		Time.timeScale = 0.5;
        		yield WaitForSeconds(2);
        		if(healthWidth<-8){
	        		Time.timeScale = 0;
	        		fuelOut.active = true;
	    			lowFuel.active = false;
	    			Crash.game_over =true;
	    			GameObject.Find("GameOver").SendMessage("SetTitle","Fuel Out");
    			}else
    				Time.timeScale = 1;
    }
    
    /*
Only decrease the health bar if it's greater than the min width it should ever be;
because we do not want it decreased beyond the left of its frame.
*/
function reduceHealth() {
	
   if(!Crash.game_over && Time.timeScale>0){
	   if(healthWidth > -8) {
	   	  
	       healthWidth = healthWidth - 0.1;
	   }
	   if(healthWidth){
	   
	   	
	   }
   }
//   Debug.Log(healthWidth+"health");
    
}
 
/*
Only increase the health bar if it's less than the max width it should ever be;
because we do not want it stretched out beyond its frame.
*/
function increaseHealth() {
   if(healthWidth < 199) {
       healthWidth = healthWidth + 1;
   }
}