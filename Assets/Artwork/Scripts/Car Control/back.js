#pragma strict
var back: GUITexture;
var back_tex : Texture2D[];

var clickSound 		   : AudioClip;

function Start () {

}

function Update () {

if (Input.touchCount>0) {
    for (var touch : Touch in Input.touches) {
    
    
     if (touch.phase == TouchPhase.Began){
     
     if (back.HitTest(touch.position)) {
//       	replay.texture = replay_tex[1];
//       	   	if(Sound.is_Sound_On){
			ButtonSound();
			
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