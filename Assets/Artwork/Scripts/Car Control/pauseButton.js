#pragma strict
var pause : GUITexture;
var pause_tex : Texture2D[];

var click : AudioClip;
var pauseMenu : GameObject;



function Start () {

}

function Update () {
// if(Input.GetMouseButtonDown(0))
//    {
//      Pause();
//       
//    }
if (Input.touchCount>0) {
    for (var touch : Touch in Input.touches) {
    
    
     if (touch.phase == TouchPhase.Began){
     
     if (pause.HitTest(touch.position)) {
//       	replay.texture = replay_tex[1];
//       	   	if(Sound.is_Sound_On){
//		audio.PlayOneShot(click);
		Pause();
       	
       }       
           	                                                                                                                                  
                                                                                                                                
                      
       }
      if (touch.phase == TouchPhase.Ended){
//       	replay.texture = replay_tex[0]; 
         
     }  
      
        }
       
       
       }



}
function Pause(){
	if(!Crash.game_over){
   	pauseMenu.active = true;
	yield WaitForSeconds(0.5);
	Time.timeScale =0;
	gameObject.active = false;
	}
	}
//Application.LoadLevel(levelno);
//is_restart = true;
//}