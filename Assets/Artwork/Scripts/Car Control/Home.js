#pragma strict
var home: GUITexture;
var home_tex : Texture2D[];

var clickSound 		   : AudioClip;

function Start () {
//AirScript.startAirSmartWallAd();
}

function Update () {

if (Input.touchCount>0) {
    for (var touch : Touch in Input.touches) {
    
    
     if (touch.phase == TouchPhase.Began){
     
     if (home.HitTest(touch.position)) {
//       	replay.texture = replay_tex[1];
//       	   	if(Sound.is_Sound_On){
			ButtonSound();
			Time.timeScale = 1;
			Application.LoadLevel("MainMenu");
		
       	
       }       
           	                                                                                                                                  
                                                                                                                                
                      
       }
      if (touch.phase == TouchPhase.Ended){
//       	replay.texture = replay_tex[0]; 
         
     }  
      
        }
       
       
       }



}
 function ButtonSound(){
		
	
		audio.PlayOneShot(clickSound);
		
	

}