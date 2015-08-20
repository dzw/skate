#pragma strict
var resume : GUITexture;
var reesume_tex : Texture2D[];

var clickSound 		   : AudioClip;
var pauseMenu : GameObject;
var pauseBtn : GameObject;
function Start () {

}

function Update () {

if (Input.touchCount>0) {
    for (var touch : Touch in Input.touches) {
    
    
     if (touch.phase == TouchPhase.Began){
     
     if (resume.HitTest(touch.position)) {
//       	replay.texture = replay_tex[1];
//       	   	if(Sound.is_Sound_On){
//		audio.PlayOneShot(click);
		ButtonSound();
		Time.timeScale = 1;
		pauseMenu.active = false;
		pauseBtn.active = true;
		
       	
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