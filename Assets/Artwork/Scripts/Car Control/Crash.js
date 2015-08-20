#pragma strict
var gameOver : GameObject;
static var game_over = false;
var explossion : GameObject;



function Start () {
game_over = false;
}

function Update () {

}

function OnCollisionEnter(collision : Collision) {
	
	}
function OnTriggerEnter(collision: Collider){

	Debug.Log(collision.gameObject.tag);
	// Debug-draw all contact points and normals
	if(collision.gameObject.name == "_collider" ){
			
		GameOver();
		}
		
	
}
function GameOver(){
if(!game_over)
	
	game_over = true;
	explossion.active = true;
	
	yield WaitForSeconds(1);
	gameOver.active = true;
	GameObject.Find("GameOver").SendMessage("SetTitle","Game Over");
}