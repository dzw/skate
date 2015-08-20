#pragma strict
 var speed : int ;
function Start () {

}

function Update () {
   transform.Rotate(Vector3.up * Time.deltaTime *speed, Space.World);
   
}