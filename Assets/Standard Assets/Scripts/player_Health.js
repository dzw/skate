#pragma strict

	
	
		public var showGameOverScreen : boolean = true;
		public var isNotDead :boolean = true;
		public var maxHealth : float = 100;
		public static var curHealth : float = 100;
		
		var player	   		: GameObject;
		
		var bgImage    		: GUIStyle;
		var fgImage    		: GUIStyle;
		var lives 			: int = 3;
		var text			: GUIStyle;
		
		var blood 			: GUITexture;

		
		private var x: float ;
		private var y: float ;
		private var resolution :Vector2;
		
	
function Start () {

		   resolution = new Vector2(Screen.width, Screen.height);
	
			x=Screen.width/1366.0f; 
	
			y=Screen.height/768.0f;
		
			
}

function Update () {
	
	
	if(curHealth <= 0){
		
		lives = lives -1;
		curHealth = 100;

		if(lives < 1){
		
		curHealth = 0;
		
		player =GameObject.Find("Player");
		Destroy(gameObject);
	
			
			}		
	}
	
		
		if(Screen.width!=resolution.x || Screen.height!=resolution.y)
	   {
	       resolution=new Vector2(Screen.width, Screen.height);
	       x=resolution.x/1366.0f; 
	       y=resolution.y/768.0f;  
	   
	   }
	
}

function OnGUI(){

		 	GUI.Box(new Rect((Screen.width /2 -220)*x, 30*y, 570*x, 35*y),"", bgImage);
		 
			GUI.Box(new Rect((Screen.width /2 +220) *x, 2*y,(480/(maxHealth/curHealth))*x,50*y),
			 curHealth + "/" + maxHealth, fgImage);
			  
			GUI.Label (Rect ((Screen.width/2 +960)*x, 12*y, 100*x, 120*y), "" +lives, text);  

	
	}


public  function AddjustCurrentHealth( adj :float){
		
		
		curHealth += adj;

		if(curHealth > maxHealth){
			curHealth = maxHealth;
		}
}

function OnCollisionEnter(collision : Collision) {
	
	if(collision.gameObject.tag == "enemyLazer" ){
	
	
	AddjustCurrentHealth(-3);
	Destroy(collision.gameObject);
   	
   	blood.active =true;
   	yield WaitForSeconds(0.7);
	blood.active = false;
}
	
	if(collision.gameObject.tag == "bossLazer"){
	
	AddjustCurrentHealth(-6);
	Destroy(collision.gameObject);
	
	blood.active =true;
   	yield WaitForSeconds(0.1);
	blood.active = false;
	
	}
	
}

 
 

 
