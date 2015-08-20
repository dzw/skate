using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Platformer : MonoBehaviour
{
		public GameObject prefabPlatform, enemyPrefab;
		public int numberOfBlocks;
		public Vector2 startingPosition;
		public float recycleOffset;
		private Vector2 nextPosition;
		private Queue<GameObject> gameObjectQueue, enemyQueue, staticEnemyQueue, coinQueue, staticObjectQueue;
		public Vector2 minSize, maxSize, minGap, maxGap;
		public float minY, maxY;
		public Material[] materials;
		public Sprite[] sprites, staticEnemeySprites, staticObjectSprites ;
		public PhysicsMaterial2D[] physicsMaterials;
		public GameObject coinPrefab;
		public int coinNumber, coinShowMin, coinShowMax;
		public float lowerEnemy = 6f;
		public float upperEnemy = 11.0f;
		private static int recycleCounter;
		private  double count;
		private Animator anim;
		GameObject en;
		public GameObject staticObjectPrefab;
		// Use this for initialization
		void Awake ()
		{
				GameEventManager.GameStart += GameStart;
				GameEventManager.GameOver += GameOver;
				gameObjectQueue = new Queue<GameObject> (numberOfBlocks);
				enemyQueue = new Queue<GameObject> (numberOfBlocks);
				staticEnemyQueue = new Queue<GameObject> (numberOfBlocks);
				staticObjectQueue = new Queue<GameObject> (numberOfBlocks);
				coinQueue = new Queue<GameObject> (25);
				nextPosition = startingPosition;
				count = 5f;
				print ("Start in platformer");
				recycleCounter = 0;

		}

		void Start ()
		{

		}

		void OnDestroy ()
		{
				GameEventManager.GameStart -= GameStart;
				GameEventManager.GameOver -= GameOver;
		}

		private void GameOver ()
		{

				print ("game over ");
				DestroyEnemyAndPlatform ();
				gameObjectQueue.Clear ();
				enemyQueue.Clear ();
				staticEnemyQueue.Clear ();
				staticObjectQueue.Clear ();
				coinQueue.Clear ();
				CancelInvoke ();
				recycleCounter = 0;
				GameObject.Find ("Platform Manager").GetComponent<Platformer> ().enabled = false;
				
		
				
		}

		private void GameStart ()
		{
				print ("Game Start in platform");

				recycleCounter = 0;
				count = 5f;
				gameObjectQueue = new Queue<GameObject> (numberOfBlocks);
				enemyQueue = new Queue<GameObject> (numberOfBlocks);
				staticEnemyQueue = new Queue<GameObject> (numberOfBlocks);
				coinQueue = new Queue<GameObject> (numberOfBlocks * numberOfBlocks);
				staticObjectQueue = new Queue<GameObject> (numberOfBlocks);
				nextPosition = startingPosition;
				
				EnemyAndPlatformCreate ();
				StaticEnemyShow ();
				StaticObjectCreate ();
				//create coin and show it 
				CoinCreate ();
				for (int i = 0; i < numberOfBlocks; i++) {
						Recycle ();
				}
				GameObject.Find ("Platform Manager").GetComponent<Platformer> ().enabled = true;
				
				InvokeRepeating ("CustomUpdate", 0, 0.01f);
		}

		void CustomUpdate ()
		{
				if (gameObjectQueue.Peek ().transform.localPosition.x + recycleOffset < Runner.distanceTraveled) {
						Recycle ();
			
				}
		}

		private void StaticEnemyShow ()
		{
				GameObject en;
				for (int i=0; i<numberOfBlocks; i++) {
						if (i % 3 == 0) {
								en = GameObject.Instantiate (Resources.Load ("StaticEnemy")) as GameObject;
								int ran = (int)Random.Range (0, 4);
								en.GetComponent<SpriteRenderer> ().sprite = staticEnemeySprites [ran];
								en.tag = "Enemy7";
								staticEnemyQueue.Enqueue (en);
						}
				}
		}

		private void StaticObjectCreate ()
		{
				GameObject sObject;
				for (int i=0; i<numberOfBlocks*numberOfBlocks; i++) {
						{
								sObject = GameObject.Instantiate (staticObjectPrefab) as GameObject;
								if (PlayerPrefs.GetInt ("Stage") == 2 || PlayerPrefs.GetInt ("Stage") == 3) {
										sObject.GetComponent<SpriteRenderer> ().sprite = staticObjectSprites [1];
								} else if (PlayerPrefs.GetInt ("Stage") == 5 || PlayerPrefs.GetInt ("Stage") == 4 || PlayerPrefs.GetInt ("Stage") == 6) {
										sObject.GetComponent<SpriteRenderer> ().sprite = staticObjectSprites [2];
								} else
										sObject.GetComponent<SpriteRenderer> ().sprite = staticObjectSprites [0];
								staticObjectQueue.Enqueue (sObject);
						}
				}
		}

		private void StaticObjectRecycle (Vector2 position)
		{
				for (int i=0; i<(int)Random.Range(0,4); i++) {
						GameObject sObject;
						sObject = staticObjectQueue.Dequeue ();
						sObject.SetActive (true);
						Vector3 enPos = position;
						enPos.x += -30f + 5 * i;
						enPos.y += 12f;
		
						enPos.z = -2; 
						sObject.transform.localPosition = enPos;
						staticObjectQueue.Enqueue (sObject);
				}
		}

		private void StaticEnemyRecycle (Vector2 position)
		{
		
				GameObject en = staticEnemyQueue.Dequeue ();
		
				en.SetActive (true);
				Vector3 enPos = position;
				enPos.x += -78f;
				enPos.y += 3f;
				
				enPos.z = -4; 
		
				en.transform.localPosition = enPos;
		

		
				staticEnemyQueue.Enqueue (en);
		}

		private void DestroyEnemyAndPlatform ()
		{
				for (int i=0; i<gameObjectQueue.Count; i++) {
						DestroyImmediate (gameObjectQueue.Dequeue ());
				}
				for (int i=0; i<numberOfBlocks; i++) {
						DestroyImmediate (enemyQueue.Dequeue ());
				}
				for (int i=0; i<staticEnemyQueue.Count; i++) {
						if (i % 3 == 0)
								DestroyImmediate (staticEnemyQueue.Dequeue ());

				}
				for (int i=0; i<staticObjectQueue.Count; i++) {
						DestroyImmediate (staticObjectQueue.Dequeue ());
				}
				for (int i=0; i<staticEnemyQueue.Count; i++) 
						DestroyImmediate (coinQueue.Dequeue ());

		}

		private void EnemyAndPlatformCreate ()
		{
				for (int i=0; i<numberOfBlocks; i++) {
						GameObject gb = GameObject.Instantiate (prefabPlatform) as GameObject;
						gb.tag = "Platform";
						gameObjectQueue.Enqueue (gb);
			
						switch (PlayerPrefs.GetInt ("Stage")) {
						case 1:
								en = GameObject.Instantiate (Resources.Load ("Enemy")) as GameObject;
								en.tag = "Enemy";
								break;
						case 2:
								en = GameObject.Instantiate (Resources.Load ("Enemy2")) as GameObject;
								en.tag = "Enemy2";
								break;
						case 3:
								en = GameObject.Instantiate (Resources.Load ("Enemy3")) as GameObject;
								en.tag = "Enemy3";
								break;
						case 4:
								en = GameObject.Instantiate (Resources.Load ("Enemy4")) as GameObject;
								en.tag = "Enemy4";
								break;
						case 5:
								en = GameObject.Instantiate (Resources.Load ("Enemy5")) as GameObject;
								en.tag = "Enemy5";
								break;
						case 6:
								en = GameObject.Instantiate (Resources.Load ("Enemy6")) as GameObject;
								en.tag = "Enemy6";
								break;
						default:
								en = GameObject.Instantiate (Resources.Load ("Enemy")) as GameObject;
								en.tag = "Enemy";
								break;
				
						}
			
			
						enemyQueue.Enqueue (en);
			
			
			
				}
		}

		private void CoinCreate ()
		{
				for (int i=0; i<coinNumber; i++) {
						GameObject cn = GameObject.Instantiate (coinPrefab) as GameObject;
						cn.tag = "Coin";
						Vector2 cnPos = new Vector2 (-100f, -100f);
						cn.transform.localPosition = cnPos; 
						coinQueue.Enqueue (cn);
				}
		}

		// Update is called once per frame
		void Update ()
		{
				
		
		}

		private void EnemyShow (Vector2 position)
		{

				GameObject en = enemyQueue.Dequeue ();

				en.SetActive (true);
				Vector3 enPos = position;
				enPos.x += Random.Range (-22f, 22f);
				if ((int)Random.Range (0, 2) == 1)
						enPos.y += upperEnemy;
				else 
						enPos.y += lowerEnemy;
				enPos.z = -2; 
				
				en.transform.localPosition = enPos;

	
				if (count < 10f) {
						iTween.MoveTo (en, new Vector3 (GameObject.FindGameObjectWithTag ("Player").transform.position.x, enPos.y, 0), (float)count);
						count ++;
				} else 
						iTween.MoveTo (en, new Vector3 (GameObject.FindGameObjectWithTag ("Player").transform.position.x, enPos.y, 0), 5);
		
				enemyQueue.Enqueue (en);
		}

		private void CoinRecycle (Vector2 position)
		{
				for (int i=0; i<(int)Random.Range(coinShowMin,coinShowMax); i++) {
						GameObject cn = coinQueue.Dequeue ();
						cn.SetActive (true);
						Vector2 cnPos = position;
						cnPos.x += 2 * i;
						cnPos.y += Random.Range (15f, 17f);
						cn.transform.localPosition = cnPos;
						coinQueue.Enqueue (cn);
				}
		}

		public void Recycle ()
		{
				recycleCounter++;
		
				Vector2 scale = new Vector2 (Random.Range (minSize.x, maxSize.x),
		                             Random.Range (minSize.y, maxSize.y)
				);
				Vector2 position = nextPosition;
				position.x += scale.x;
				position.y += startingPosition.y - 10f;


				GameObject gb = gameObjectQueue.Dequeue ();
				gb.transform.localScale = scale;
				gb.transform.localPosition = position;
				
				int materialIndex = Random.Range (0, materials.Length);
				gb.GetComponent<SpriteRenderer> ().sprite = sprites [materialIndex];

				gb.collider2D.sharedMaterial = physicsMaterials [materialIndex];
				gameObjectQueue.Enqueue (gb);

				
				
				if (recycleCounter > 6) {
						//Coin Recycling
						CoinRecycle (position);
						//Enemy create and show
						EnemyShow (position);
						StaticEnemyRecycle (position);
				}
				StaticObjectRecycle (position);
				nextPosition += new Vector2 (
					Random.Range (minGap.x, maxGap.x),
					Random.Range (minGap.y, maxGap.y)
				);

		}
}