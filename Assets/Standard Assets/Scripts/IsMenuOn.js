#pragma strict
var menuItems : GameObject;
var hud : GameObject;
var script : PlayerCar_Script;
var hummer : GameObject;


function Start () {
menuItems.active = true;
hud.active = false;
script = hummer.GetComponent("PlayerCar_Script");
script.enabled = false;
if(Replay.is_restart){

menuItems.active = false;
hud.active = true;
script = hummer.GetComponent("PlayerCar_Script");
script.enabled = true;

}

if(PauseMenuMenu.is_pause){

menuItems.active = true;
hud.active = false;
script = hummer.GetComponent("PlayerCar_Script");
script.enabled = false;
PauseMenuMenu.is_pause = false;



}

}

function Update () {

}