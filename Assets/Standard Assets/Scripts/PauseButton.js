#pragma strict
var pausebutton : GUITexture;
var pausebutton_tex : Texture2D[];
var script : PlayerCar_Script;
var car : GameObject;
var pauseMenu : GameObject;
var click : AudioClip;

function Start () {

}

function Update () {


if (Input.touchCount>0) {
    for (var touch : Touch in Input.touches) {
    
    
     if (touch.phase == TouchPhase.Began){
     
     if (pausebutton.HitTest(touch.position)) {
       	pausebutton.texture = pausebutton_tex[1];
       		if(Sound.is_Sound_On){
		audio.PlayOneShot(click);
	
	}	pauseMenu.active = true;
		Time.timeScale = 0;
		GameObject.Find("Hummer").SendMessage("StopMusic"); 
//		script = car.GetComponent("PlayerCar_Script");
//    	script.enabled = false;
       	
       }       
           	                                                                                                                                  
                                                                                                                                
                      
       }
      if (touch.phase == TouchPhase.Ended){
       	pausebutton.texture = pausebutton_tex[0]; 
         
     }  
      
        }
       
       
       }
       
       if(Application.platform == RuntimePlatform.Android)

        {
            if (Input.GetKey(KeyCode.Escape))

            {	if(play.is_play){
      			pauseMenu.active = true;
				Time.timeScale = 0;
				GameObject.Find("Hummer").SendMessage("StopMusic");
				} 

                return;

            }
                       
            


        }

}

//function OnMouseUp(){
//	if(Sound.is_Sound_On){
//		audio.PlayOneShot(click);
//	
//	}
//GameObject.Find("Hummer").SendMessage("StopMusic");
//pauseMenu.active = true;
//Time.timeScale = 0;
////script = car.GetComponent("PlayerCar1_Script");
////script.enabled = false;  
//   
//}