#pragma strict
var replay : GUITexture;
var replay_tex : Texture2D[];
static var is_restart : boolean = false;
var click : AudioClip;



function Start () {

}

function Update () {

if (Input.touchCount>0) {
    for (var touch : Touch in Input.touches) {
    
    
     if (touch.phase == TouchPhase.Began){
     
     if (replay.HitTest(touch.position)) {
       	replay.texture = replay_tex[1];
       	   	if(Sound.is_Sound_On){
		audio.PlayOneShot(click);
	
	}
		Application.LoadLevel(0);
		is_restart = true;
	//	GameObject.Find("Aston2").SendMessage("PauseMusic"); 
//		script = car.GetComponent("PlayerCar_Script");
//    	script.enabled = false;
       	
       }       
           	                                                                                                                                  
                                                                                                                                
                      
       }
      if (touch.phase == TouchPhase.Ended){
       	replay.texture = replay_tex[0]; 
         
     }  
      
        }
       
       
       }



}
//function OnMouseUp(){
//   	if(Sound.is_Sound_On){
//		audio.PlayOneShot(click);
//	
//	}
//Application.LoadLevel(0);
//is_restart = true;
//}