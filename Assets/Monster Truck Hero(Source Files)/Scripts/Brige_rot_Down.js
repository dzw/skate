var one =false;
var two= false;
function Update () {
		if(one && !two)
		{
			animation.Play("Level5_BrigeAnimation1");
			audio.Play();
			two = true;
		}
}