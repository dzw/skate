#pragma strict
var exit : GUITexture;
var exit_tex : Texture2D[];
var click : AudioClip;

function Start () {

}

function Update () {

if (Input.touchCount>0) {
    for (var touch : Touch in Input.touches) {
    
    
     if (touch.phase == TouchPhase.Began){
     
     if (exit.HitTest(touch.position)) {
       	exit.texture = exit_tex[1];
       		if(Sound.is_Sound_On){
		audio.PlayOneShot(click);
	
	}
		Application.Quit();

       	
       }       
           	                                                                                                                                  
                                                                                                                                
                      
       }
      if (touch.phase == TouchPhase.Ended){
       	exit.texture = exit_tex[0]; 
         
     }  
      
        }
       
       
       }



}
//function OnMouseUp(){
//	if(Sound.is_Sound_On){
//		audio.PlayOneShot(click);
//	
//	}
//Application.Quit();
//
//}