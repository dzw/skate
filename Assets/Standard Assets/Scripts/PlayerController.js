

// the PickupController component of the 'PickupSpawnPoints' GameObject
private var pickupController:PickupController;

var PickupEffect : GameObject;

var Counter : int = 0;

var ScoreText : GUIText;
 
function Awake()
{
    // retrieve the PickupSpawnPoints gameObject
    var pickupSpawnPoints:GameObject = gameObject.Find("PickupSpawnPoints");
 
    // and then retreive the PickupController Component of the above PickupSpawnPoints gameObject
    pickupController = pickupSpawnPoints.GetComponent("PickupController");
}
 
function OnTriggerEnter (other : Collider)
{
    if (other.gameObject.tag == "Pickup")
{
		Counter++;
		
		ScoreText.text = "SCORE: " +Counter *120;
		
        pickupController.Collected(other.gameObject);
		
		Instantiate (PickupEffect, transform.position, transform.rotation);
		}
    }
