#pragma strict
var gui : GameObject;
var titleText : GUIText;
function Start () {
gui.active = false;
//AirScript.startAirSmartWallAd();
}
function SetTitle(title : String){
titleText.text = title;
titleText.fontSize = 32; 
}
function Update () {

}