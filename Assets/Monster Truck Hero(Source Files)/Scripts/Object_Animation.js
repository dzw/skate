
function Update () 
{
	transform.Rotate (0, 45 * Time.deltaTime, 0);
}
function OnBecameVisible()
{
	enabled = true;	
}

function OnBecameInvisible()
{
	enabled = false;	
}
 function OnTriggerEnter(collider:Collider){
 	Destroy(gameObject);
 	Debug.Log(collider.gameObject.name);
 }