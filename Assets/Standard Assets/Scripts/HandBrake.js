#pragma strict
var handbrake : GUITexture;
var handbrake_tex : Texture2D[];
var click : AudioClip;
static var is_handbreak : boolean=false;

function Start () {
is_handbreak=false;

}



function OnMouseUp(){

if(is_handbreak==false)
{
handbrake.texture = handbrake_tex[1];
is_handbreak=true;
}
else
{
is_handbreak=false;
handbrake.texture = handbrake_tex[0];
}
if(Sound.is_Sound_On){
	audio.PlayOneShot(click);
	
	}



}


	
	
	
	

