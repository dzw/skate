#pragma strict

function Start () {
MusicMenu();
}

function Update () {

}
function MusicMenu(){
if(Sound.is_Sound_On){
audio.Play();
	
	}
	
}
function StopMusic(){

	audio.Stop();
	


}