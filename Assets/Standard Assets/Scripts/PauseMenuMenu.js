#pragma strict
var menu : GUITexture;
var menu_tex : Texture2D[];
var hud : GameObject;
var menuItems : GameObject;
var script : PlayerCar_Script;
var hummer : GameObject;
static var is_pause : boolean = false;
var pauseMenu : GameObject;
var click : AudioClip;

function Start () {


}

function Update () {

if (Input.touchCount>0) {
    for (var touch : Touch in Input.touches) {
    
    
     if (touch.phase == TouchPhase.Began){
     
     if (menu.HitTest(touch.position)) {
       	menu.texture = menu_tex[1];
		is_pause = true;
		if(Sound.is_Sound_On){
		audio.PlayOneShot(click);
	
	}
		Application.LoadLevel(0);
	//	GameObject.Find("Aston2").SendMessage("PauseMusic"); 
//		script = car.GetComponent("PlayerCar_Script");
//    	script.enabled = false;
       	
       }       
           	                                                                                                                                  
                                                                                                                                
                      
       }
      if (touch.phase == TouchPhase.Ended){
       	menu.texture = menu_tex[0]; 
         
     }  
      
        }
       
       
       }



}
//function OnMouseUp(){
//is_pause = true;
//if(Sound.is_Sound_On){
//audio.PlayOneShot(click);
//	
//	}
//Application.LoadLevel(0);
//
//}