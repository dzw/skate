#pragma strict
var resume : GUITexture;
var resume_tex : Texture2D[];
var pauseMenu : GameObject;
var click : AudioClip;

function Start () {

}

function Update () {

if (Input.touchCount>0) {
    for (var touch : Touch in Input.touches) {
    
    
     if (touch.phase == TouchPhase.Began){
     
     if (resume.HitTest(touch.position)) {
       	resume.texture = resume_tex[1];
       	   	if(Sound.is_Sound_On){
		audio.PlayOneShot(click);
	
	}
		Time.timeScale = 1;
		pauseMenu.active = false;
		GameObject.Find("Hummer").SendMessage("PlayMusic"); 
//		script = car.GetComponent("PlayerCar_Script");
//    	script.enabled = false;
       	
       }       
           	                                                                                                                                  
                                                                                                                                
                      
       }
      if (touch.phase == TouchPhase.Ended){
       	resume.texture = resume_tex[0]; 
         
     }  
      
        }
       
       
       }



}
//function OnMouseUp(){
//   	if(Sound.is_Sound_On){
//		audio.PlayOneShot(click);
//	
//	}
//GameObject.Find("Hummer").SendMessage("PlayMusic"); 
//Time.timeScale = 1;
//pauseMenu.active = false;
//}