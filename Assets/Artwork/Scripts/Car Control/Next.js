#pragma strict
var next: GUITexture;
var next_tex : Texture2D[];

var click : AudioClip;
var nextLevelNum : int;
function Start () {
//       var val = PlayerPrefs.GetInt("LevelUnlocked");
//		if(Gallery.scenIndex <val){
//    		 
//		 gameObject.active = false;
//		 }
//		 else
//		  gameObject.active = true;
//		  
//		  Debug.Log(""+"Level"+(Gallery.scenIndex+2));
}

function Update () {

if (Input.touchCount>0) {
    for (var touch : Touch in Input.touches) {
    
    
     if (touch.phase == TouchPhase.Began){
     
     if (next.HitTest(touch.position)) {
//       	replay.texture = replay_tex[1];
//       	   	if(Sound.is_Sound_On){
			Time.timeScale = 1;
			Application.LoadLevel("Level"+nextLevelNum);
		
       	
       }       
           	                                                                                                                                  
                                                                                                                                
                      
       }
      if (touch.phase == TouchPhase.Ended){
//       	replay.texture = replay_tex[0]; 
           
     }  
      
        }
       
       
       }



}
