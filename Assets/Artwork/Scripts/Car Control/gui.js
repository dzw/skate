#pragma strict

var coinTex: Texture2D;

var coinPos: Vector2;



function OnGUI() {
	
	GUI.backgroundColor = Color.clear;
	GUI.DrawTexture(Rect(coinPos.x, coinPos.y, coinTex.width/2, coinTex.height/2), coinTex);
	
   


   
}
function Start () {

}

function Update () {
	
}
