var dialTex: Texture2D;
var needleTex: Texture2D;
var dialPos: Vector2;
var topSpeed: float;
var stopAngle: float;
var topSpeedAngle: float;
static var speed: float;

function Start(){

dialPos.x = Screen.width/2- dialTex.width/2;
dialPos.y = Screen.height - dialTex.height;

//dialTex.height = dialTex.width  = 128;
////dialTex.height = 256;
//Debug.Log(Screen.currentResolution.width+"res");

}

function OnGUI() {
	GUI.DrawTexture(Rect(dialPos.x, dialPos.y, dialTex.width, dialTex.height), dialTex);
	var centre = Vector2(dialPos.x + dialTex.width / 2, dialPos.y + dialTex.height / 2);
	var savedMatrix = GUI.matrix;
	var speedFraction = speed / topSpeed;
	var needleAngle = Mathf.Lerp(stopAngle, topSpeedAngle, speedFraction);
	GUIUtility.RotateAroundPivot(needleAngle, centre);
	GUI.DrawTexture(Rect(centre.x, centre.y - needleTex.height / 2, needleTex.width, needleTex.height), needleTex);
	GUI.matrix = savedMatrix;
}