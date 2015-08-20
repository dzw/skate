#pragma strict
var ExplosionPrefab : Transform;
var ExplosionPrefab2 : Transform;
var sound : AudioClip;
var car : GameObject;
var carTop : GameObject;
var gameOver : GameObject;
var exp: boolean = false;
var explossion : GameObject;
function Start () {

}

function Update () {
	if(Crash.game_over && exp)
		ThrowUp();

}
function OnCollisionEnter(collision : Collision)
{	
	Debug.Log(collision.gameObject.tag);
//	if(collision.gameObject.tag == "PlayerCar"){
	    Instantiate (ExplosionPrefab,transform.position,transform.rotation);
	    Instantiate (ExplosionPrefab2,transform.position,transform.rotation);
	    audio.PlayOneShot(sound);
	    car.rigidbody.AddForce(transform.forward*100, ForceMode.Acceleration);
//		 GameObject.Find("Collider1").SendMessage("GameOver",SendMessageOptions.DontRequireReceiver);
		
//		carTop.SendMessageUpwards("GameOver");
		Crash.game_over = true;
		exp = true;
		explossion.active = true;
		Debug.Log(Crash.game_over+"fddfdffdsfdsf");
	    yield WaitForSeconds(0.75);
	    
	    gameOver.active = true;
	    Destroy (gameObject);
//    }
    
}
function ThrowUp(){
	car.rigidbody.AddForce(transform.forward * 25, ForceMode.Acceleration);
}