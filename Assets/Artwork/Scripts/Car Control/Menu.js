#pragma strict
var play 		: GUITexture;
var settings 	: GUITexture;
var credits  	: GUITexture;
var back  	 	: GUITexture;
var next  	 	: GUITexture;
var left  	 	: GUITexture;
var carLocked	: GUITexture;
var right 	 	: GUITexture;
var speaker	    : GUITexture;

var boardTexture: Texture2D[];
var cars 		: GameObject[];


static var carIndex   : int =0;
static var is_restart : boolean = false;

var carSelection 	  : GameObject;
var prices 			  : int[];
var priceText 		  : GUIText;
var coinTex			  : Texture2D;
var totalCoins        : GUIText;
var coinPos           : Vector2;
var notEnoughCoins    : GUIText;
var buyText 		  : Texture2D;
var nextText 		  : Texture2D;
var sound_muteTexture : Texture2D;
var soundTexture      : Texture2D;

var gallery 		   : Gallery;
 var clickSound 		   : AudioClip;
function OnGUI() {
	
	GUI.backgroundColor = Color.clear;
	GUI.DrawTexture(Rect(coinPos.x, coinPos.y, coinTex.width/2, coinTex.height/2), coinTex);
	totalCoins.text = ""+PlayerPrefs.GetInt("coins"); 
	  
}
function Start () {
//PlayerPrefs.SetInt("car1",0);
//PlayerPrefs.SetInt("car2",0);
//PlayerPrefs.SetInt("car3",0);
  carIndex = 0;
  is_restart = false;
//  play.color = Color.gray;
	if(AudioListener.pause)
	speaker.texture = sound_muteTexture;
  
}





function Update () {
if(Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor ){
	ForMouse();
}
else if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer ){
	ForTouch();

}



}

function ForMouse(){
	if(Input.GetMouseButton(0) && play.HitTest(Input.mousePosition))

{			
 			
			ButtonSound();
  			GameObject.Find("Board").renderer.material.mainTexture = boardTexture[1];
			HideMainMenu();
			ShowCarSeclection();

}
else if(Input.GetMouseButton(0) && back.HitTest(Input.mousePosition))

{
			ButtonSound();
  			carLocked.active = false;
  			carIndex = 0;
  			for(var n =0 ; n<4 ; n++)
				 	cars[n].active = false;
			cars[carIndex].active = true;
			priceText.text = "";
			HideCarSelection();
			ShowMainMenu();

}

else if(Input.GetMouseButton(0) && right.HitTest(Input.mousePosition))

{
	ButtonSound();
   if(carIndex<3){
				 for(var k =0 ; k<4 ; k++)
				 	cars[k].active = false;
		 		 cars[++carIndex].active = true;
				}
				
			if(PlayerPrefs.GetInt("car"+carIndex)==0){
				
				carLocked.active = true;
				next.texture = buyText;
				priceText.text = "$"+prices[carIndex-1];
			}
			else{
				next.texture = nextText;
				carLocked.active = false;
			}
   }
   
   
   else if(Input.GetMouseButton(0) && left.HitTest(Input.mousePosition))

{
			ButtonSound();
   			if(carIndex>0){
				 for(var i =0 ; i<4 ; i++)
				 	cars[i].active = false;
		 		 cars[--carIndex].active = true;
		 		 
				}
			if(PlayerPrefs.GetInt("car"+carIndex)==0 && carIndex >0 ){
				Debug.Log(PlayerPrefs.GetInt("car"+carIndex)+" index"+carIndex);
				carLocked.active = true;
				next.texture = buyText;
				priceText.text = "$"+prices[carIndex-1];
				if(carIndex==0) priceText.text = "";
			}
			else{
				Debug.Log("adsds "+PlayerPrefs.GetInt("car"+carIndex)+" index"+carIndex);
				next.texture = nextText;
				priceText.text = "";
				carLocked.active = false;
				
			}
   }
   
   
  else if(Input.GetMouseButton(0) && next.HitTest(Input.mousePosition))

{
				if(next.texture.name == "next"){
					ButtonSound();
					gallery.ShowButtons();
					GameObject.Find("Camera").animation.Play("cameraMove");;
					HideCarSelection();
	  				
//					Application.LoadLevel("Gallery");
				}
				else if(next.texture.name == "buy"){
					ButtonSound();
					if(PlayerPrefs.GetInt("coins")>=prices[carIndex-1]){
						
						var val1 : int  = PlayerPrefs.GetInt("coins");
						val1-= prices[carIndex-1];
						totalCoins.text = ""+val1;
						PlayerPrefs.SetInt("coins",val1);
						carLocked.active = false;
						priceText.active = false;
						next.texture = nextText;
						PlayerPrefs.SetInt("car"+carIndex,1);
						Debug.Log("Buy");
					
					}
				else
					NotEnoughCoins();
			
				 }   
				
   } 
   else if (Input.GetMouseButton(0) && speaker.HitTest(Input.mousePosition)){
   		ButtonSound();
   		if(speaker.texture.name=="sound"){
   			AudioListener.pause = true;
   			speaker.texture = sound_muteTexture;
   			}
   		else if(speaker.texture.name=="sound_mute"){
   			AudioListener.pause = false;
   			gameObject.audio.Play();
   			speaker.texture = soundTexture;
   			}
   }
    else if(Input.GetMouseButton(0) && credits.HitTest(Input.mousePosition)){
	  		
	  		ButtonSound();
	  		HideMainMenu();
	  		Application.LoadLevel("Credits");
	  }
   
}
function ForTouch(){

/////////////// touch code
if (Input.touchCount>0) {
    for (var touch : Touch in Input.touches) {
    
    
     if (touch.phase == TouchPhase.Began){
     
	     if (play.HitTest(touch.position)) {
			play.color = Color.blue;
			ButtonSound();
			GameObject.Find("Board").renderer.material.mainTexture = boardTexture[1];
			HideMainMenu();
			ShowCarSeclection();
	       }
      else if (settings.HitTest(touch.position)) {
			settings.color = Color.blue;
			ButtonSound();
			HideMainMenu();
			Application.OpenURL ("market://details?id=com.dimen.hillclimbracing");
	       }
	  else if(credits.HitTest(touch.position)){
	  	    credits.color = Color.blue;
	  		ButtonSound();
	  		HideMainMenu();
	  		Application.LoadLevel("Credits");
	  }
     else if (back.HitTest(touch.position)) {
            back.color = Color.blue;
			ButtonSound();
			GameObject.Find("Board").renderer.material.mainTexture = boardTexture[0];
			carLocked.active = false;
			carIndex = 0;
			for(var l =0 ; l<4 ; l++)
				 	cars[l].active = false;
			cars[carIndex].active = true;
			priceText.text = "";
			HideCarSelection();
			ShowMainMenu();
	
       }        
	  else if (left.HitTest(touch.position)) {
	  		
			ButtonSound();
			left.color = Color.blue;
			if(carIndex>0){
				 for(var i =0 ; i<4 ; i++)
				 	cars[i].active = false;
		 		 cars[--carIndex].active = true;
		 		 
				}
			if(PlayerPrefs.GetInt("car"+carIndex)==0 && carIndex >0 ){
				Debug.Log(PlayerPrefs.GetInt("car"+carIndex)+" index"+carIndex);
				carLocked.active = true;
				next.texture = buyText;
				priceText.text = "$"+prices[carIndex-1];
				if(carIndex==0) priceText.text = "";
			}
			else{
				Debug.Log("adsds "+PlayerPrefs.GetInt("car"+carIndex)+" index"+carIndex);
				next.texture = nextText;
				priceText.text = "";
				carLocked.active = false;
				
			}
	   }
	  else if (right.HitTest(touch.position)) {
	 		right.color = Color.blue;
			ButtonSound();
			if(carIndex<3){
				 for(var k =0 ; k<4 ; k++)
				 	cars[k].active = false;
		 		 cars[++carIndex].active = true;
				}
				
			if(PlayerPrefs.GetInt("car"+carIndex)==0){
				
				carLocked.active = true;
				next.texture = buyText;
				priceText.text = "$"+prices[carIndex-1];
			}
			else{
				next.texture = nextText;
				carLocked.active = false;
				priceText.text = "";
			}
	 	  }      	                                                                                                                                  
	 else if (next.HitTest(touch.position)) {

		
				if(next.texture.name == "next"){
	  				ButtonSound();
	  				gallery.ShowButtons();
					GameObject.Find("Camera").animation.Play("cameraMove");;
					HideCarSelection();
				}
				else if(next.texture.name == "buy"){
					if(PlayerPrefs.GetInt("coins")>=prices[carIndex-1]){
					// 0 for purchases
					var val : int  = PlayerPrefs.GetInt("coins");
					val-= prices[carIndex-1];
					totalCoins.text = ""+val;
					PlayerPrefs.SetInt("coins",val);
					PlayerPrefs.SetInt("car"+carIndex,1);
					carLocked.active = false;
					priceText.active = false;
					next.texture = nextText;
					Debug.Log("Buy");
					
					}
				else
					NotEnoughCoins();
			
				 }                                                                                                                
                      
			}
		else if(speaker.HitTest(touch.position)){
			ButtonSound();
	   		if(speaker.texture.name=="sound"){
	   			AudioListener.pause = true;
	   			speaker.texture = sound_muteTexture;
	   			}
	   		else if(speaker.texture.name=="sound_mute"){
	   			AudioListener.pause = false;
	   			gameObject.audio.Play();
	   			speaker.texture = soundTexture;
	   			}
		}
   		 }      
	
      if (touch.phase == TouchPhase.Ended){
//       	play.texture = play_tex[0]; 
         play.color = Color.gray;
         settings.color = Color.gray;
         credits.color = Color.gray;
         left.color = Color.gray;
         right.color = Color.gray;
     }  
      
        }
       
       
       }

if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
}
function ShowMainMenu(){
	Debug.Log("");
	yield WaitForSeconds(1);
	play.animation.Play("PlayButton");
	settings.animation.Play("settings");
	credits.animation.Play("credits");

}
function HideMainMenu(){
	
	play.animation.Play("PlayButton2");
	settings.animation.Play("settings2");
	credits.animation.Play("credits2");
	
	
}

function ShowCarSeclection(){
	
	Debug.Log("");
	yield WaitForSeconds(2);
	carSelection.active = true;
	left.animation.Play("left");
	right.animation.Play("right");
	next.animation.Play("next");
	back.animation.Play("back");
}
function HideCarSelection(){
	
	left.animation.Play("left2");
	right.animation.Play("right2");
	next.animation.Play("next2");
	back.animation.Play("back2");
	
	
}
function NotEnoughCoins(){
	
		notEnoughCoins.active = true;
		yield WaitForSeconds(2);
		notEnoughCoins.active= false;
}
 function ButtonSound(){
		
	
		audio.PlayOneShot(clickSound);
		
	

}

