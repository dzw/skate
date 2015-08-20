#pragma strict
var more : GUITexture;
var more_tex : Texture2D[];
var click : AudioClip;

function Start () {

}

function Update () {

if (Input.touchCount>0) {
    for (var touch : Touch in Input.touches) {
    
    
     if (touch.phase == TouchPhase.Began){
     
     if (more.HitTest(touch.position)) {
       	more.texture = more_tex[1];
       	if(Sound.is_Sound_On){
		audio.PlayOneShot(click);
	
	}
		Application.OpenURL("https://play.google.com/store/apps/developer?id=FUNZUP+-+Best+Racing+%26+Running+Top+Games");

       	
       }       
           	                                                                                                                                  
                                                                                                                                
                      
       }
      if (touch.phase == TouchPhase.Ended){
       	more.texture = more_tex[0]; 
         
     }  
      
        }
       
       
       }



}
//function OnMouseUp(){
//if(Sound.is_Sound_On){
//audio.PlayOneShot(click);
//	
//	}
//Application.OpenURL("https://play.google.com/store/apps/developer?id=FUNZUP+-+Best+Racing+%26+Running+Top+Games");
//
//}