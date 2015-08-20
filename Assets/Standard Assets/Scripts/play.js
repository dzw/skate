#pragma strict
var play : GUITexture;
var play_tex : Texture2D[];
var hud : GameObject;
var menuItems : GameObject;
var script : PlayerCar_Script;
var hummer : GameObject;
var click : AudioClip;
static var is_play : boolean = false;

function Start () {

}

function Update () {

if (Input.touchCount>0) {
    for (var touch : Touch in Input.touches) {
    
    
     if (touch.phase == TouchPhase.Began){
     
     if (play.HitTest(touch.position)) {
       	play.texture = play_tex[1];
       	if(Sound.is_Sound_On){
		audio.PlayOneShot(click);
	
	}	is_play = true;
		hud.active = true;
		menuItems.active = false;
		script = hummer.GetComponent("PlayerCar_Script");
		script.enabled = true;
	//	GameObject.Find("Aston2").SendMessage("PauseMusic"); 
//		script = car.GetComponent("PlayerCar_Script");
//    	script.enabled = false;
       	
       }       
           	                                                                                                                                  
                                                                                                                                
                      
       }
      if (touch.phase == TouchPhase.Ended){
       	play.texture = play_tex[0]; 
         
     }  
      
        }
       
       
       }



}
//function OnMouseUp(){
////if(Sound.is_Sound_On){
//audio.PlayOneShot(click);
//	
////	}
//hud.active = true;
//menuItems.active = false;
//script = hummer.GetComponent("PlayerCar_Script");
//script.enabled = true;
//}