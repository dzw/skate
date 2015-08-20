//var sound : AudioClip;
//var soundVolume : float;
//var explosion : GameObject;
//var Power : GameObject;
//var paren : GameObject;
//var powervalue = 40;
//private var powerbooster;
//private var exp;
// private var emite = false;
// private var ontri =true;
// private var a = 0.0;
//function Start()
//{
//soundVolume = PlayerPrefs.GetFloat("uni_mastervolume");
//powerbooster = Instantiate (Power, transform.position, transform.rotation);
//audio.volume =  PlayerPrefs.GetFloat("uni_mastervolume");
//if(paren)
//	powerbooster.transform.parent  = paren.transform;
//}
// function Update()
// {
//	
//	 if(emite && a >25.0)
//	 {
//		 powerbooster = Instantiate (Power, transform.position, transform.rotation);
//		a = 0.0;
//		emite = false;
//		ontri = true;
//		if(paren)
//			powerbooster.transform.parent  = paren.transform;
//	 }
//	 if(emite)
//	 a +=  2*Time.deltaTime;
// }
//function OnTriggerEnter (col : Collider) 
//{
//	var car : carbody = col.gameObject.GetComponent (carbody);
//	if(car)
//	{
//		if(ontri && PlayerPrefs.GetFloat("uni_PowerBoosters")!=powervalue)
//		{
//			emite = true;
//			exp= Instantiate (explosion , transform.position, transform.rotation);
//			Destroy(powerbooster);
//			ontri = false;
//			PlayerPrefs.SetFloat("uni_PowerBoosters", powervalue);
//			AudioSource.PlayClipAtPoint(sound, transform.position, PlayerPrefs.GetFloat("uni_mastervolume"));
//			audio.volume =  PlayerPrefs.GetFloat("uni_mastervolume");
//		}
//	}
//}
