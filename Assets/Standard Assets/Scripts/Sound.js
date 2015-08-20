#pragma strict
var menuSound : GUITexture;
var menuSound_tex : Texture2D[];
static var is_Sound_On : boolean = false;
var click : AudioClip;
function Start () {
if(PlayerPrefs.GetInt("is_first time game install ")== 0){
PlayerPrefs.SetInt("is_first time game install ",1);
PlayerPrefs.SetInt("sound",1);
is_Sound_On = true;
//PlayerPrefs.SetInt("totalCoins",50000);
	
	
}
if(PlayerPrefs.GetInt("sound")== 1){

	is_Sound_On = true;
GameObject.Find("Logo").SendMessage("MusicMenu");
}
else {
is_Sound_On = false;

}
if(is_Sound_On == true)
{

menuSound.texture = menuSound_tex[0];
}
else
{

menuSound.texture = menuSound_tex[1];
}

}

function Update () {

	if (Input.touchCount>0) {
    for (var touch : Touch in Input.touches) {
       if (touch.phase == TouchPhase.Began && menuSound.HitTest(touch.position)) {
       		if(is_Sound_On == true)
{
		is_Sound_On=false;
		GameObject.Find("Logo").SendMessage("StopMusic");
		menuSound.texture = menuSound_tex[1];
		
		audio.PlayOneShot(click);
	
	
	

		PlayerPrefs.SetInt("sound",0);
}
	else
	{
	is_Sound_On=true;
	menuSound.texture = menuSound_tex[0];
	PlayerPrefs.SetInt("sound",1);
	GameObject.Find("Logo").SendMessage("MusicMenu");
}
       		
       }
       
                 
             
       }
    }



}
//function OnMouseUp(){
//
//if(is_Sound_On == true)
//{
//is_Sound_On=false;
//GameObject.Find("Logo").SendMessage("StopMusic");
//menuSound.texture = menuSound_tex[1];
//
//	audio.PlayOneShot(click);
//	
//	
//PlayerPrefs.SetInt("sound",0);
//}
//else
//{
//is_Sound_On=true;
//menuSound.texture = menuSound_tex[0];
//PlayerPrefs.SetInt("sound",1);
//GameObject.Find("Logo").SendMessage("MusicMenu");
//}
//	
//	
//	
//	
//
//}