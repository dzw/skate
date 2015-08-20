#pragma strict



var back  	 	: GUITexture;
var play  	 	: GUITexture;

var locked	    : GUITexture;

var board		: GUITexture;
var boardTexture: Texture2D[];
var bestTime    : GUIText;


static var is_restart      : boolean = false;
var click 			  	   : AudioClip;
var mainMenu 	  		   : GameObject;
var min : int ;
var sec : int;
var lockedTexture 		   : Texture2D;
var playTexture 		   : Texture2D;
var distance 			   : float = 50.0;
static var scenIndex			   : int = 0;
var clickSound 				: AudioClip;
function Start () {
	
	scenIndex = 0;
	min = PlayerPrefs.GetInt("min"+(scenIndex+1));
	sec = PlayerPrefs.GetInt("sec"+(scenIndex+1));
	Debug.Log(""+min+":"+sec);
	bestTime.fontStyle = FontStyle.Bold;
	bestTime.text  = "Best Time : "+(min).ToString("00")+":"+(sec).ToString("00");
}


function ShowButtons(){
	yield WaitForSeconds(2);
	back.active = true;
	play.active = true;
	board.active = true;
	bestTime.active = true;
}

function HideButtons(){
	back.active = false;
	play.active = false;
	bestTime.active = false;
}



function Update () {
	   if(Input.GetMouseButtonDown(0)){
			var hit : RaycastHit;
//			Debug.Log("Pressed left click.");
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			if (Physics.Raycast (ray, hit))
			
			{ 
				if(hit.collider.tag == "Right" && scenIndex<7){
					
					ButtonSound();
					board.texture = boardTexture[++scenIndex];
//					Debug.Log("scenIndex"+scenIndex+" pref="+PlayerPrefs.GetInt("LevelUnlocked"));
					GameObject.Find("Right").animation.Play("moveUpDown");
					if(scenIndex>PlayerPrefs.GetInt("LevelUnlocked")){
						
						locked.active = true;
						play.texture = lockedTexture;
					}
					min = PlayerPrefs.GetInt("min"+(scenIndex+1));
					sec = PlayerPrefs.GetInt("sec"+(scenIndex+1));
					bestTime.text  = "Best Time : "+(min).ToString("00")+":"+(sec).ToString("00");
					Debug.Log(""+min+":"+sec);
				}
				else if(hit.collider.tag == "Left" && scenIndex> 0){
					ButtonSound();
					board.texture = boardTexture[--scenIndex];
					Debug.Log("scenIndex"+scenIndex);
					GameObject.Find("Left").animation.Play("moveUpDown2");
					if(scenIndex<=PlayerPrefs.GetInt("LevelUnlocked")){
						
						locked.active = false;
						play.texture = playTexture;
					}
					min = PlayerPrefs.GetInt("min"+(scenIndex+1));
					sec = PlayerPrefs.GetInt("sec"+(scenIndex+1));
					bestTime.text  = "Best Time : "+(min).ToString("00")+":"+(sec).ToString("00");
					Debug.Log(""+min+":"+sec);
				}
			}
	}
	
if(Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor  ){
	ForMouse();
	
}
else if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer   ){
	ForTouch();

}

}

function ForMouse(){
	if(Input.GetMouseButton(0) && back.HitTest(Input.mousePosition))
	
	{
		ButtonSound();
		Application.LoadLevel("MainMenu");
	}
	
	else if(Input.GetMouseButton(0) && play.HitTest(Input.mousePosition))
	
	{
		ButtonSound();		  
		if(play.texture.name == "play"){
			HideButtons();
			Application.LoadLevel("Level"+(scenIndex+1));
		}
					
	} 
   
}
function ForTouch(){

/////////////// touch code
if (Input.touchCount>0) {
    for (var touch : Touch in Input.touches) {
    
    
     if (touch.phase == TouchPhase.Began){
     
	   
     if (back.HitTest(touch.position)) {
			back.color = Color.blue;
			Application.LoadLevel("MainMenu");
	
       }        
	     	                                                                                                                                  
	 else if (play.HitTest(touch.position)) {
			play.color = Color.blue;
			ButtonSound();		  
			if(play.texture.name == "play"){
				HideButtons();
				Application.LoadLevel("Level"+(scenIndex+1));
			}                                                                                               
                      
			}
   		 }      
	
      if (touch.phase == TouchPhase.Ended){
//       	play.texture = play_tex[0]; 
         play.color = Color.gray;
         back.color = Color.gray;
         
     }  
      
        }
       
       
       }

if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
}
function ShowMainMenu(){
	

}
function HideMainMenu(){
	
	
	
}

 function ButtonSound(){
		
	if(PlayerPrefs.GetInt("sound")==0){
		audio.PlayOneShot(clickSound);
		
	}

}
