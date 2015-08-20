using UnityEngine;
using System.Collections;

public class ParallaxScrol : MonoBehaviour
{
		public Renderer background;
		public Renderer foreground;
		public float backgroundSpeed ;
		public float foregroundSpeed ;
		private Texture tex;
		// Use this for initialization
		void Start ()
		{

//		Material mat = Resources.Load<Material>("Classic3");

				switch (PlayerPrefs.GetInt ("World")) {

				case 1:

						tex = Resources.Load<Texture> ("Stage1") as Texture;
						break;
				case 2:

						tex = Resources.Load<Texture> ("Stage2") as Texture;
						break;
				case 3:

						tex = Resources.Load<Texture> ("Stage3") as Texture;
						break;
				case 4:

						tex = Resources.Load<Texture> ("Stage4") as Texture;
						break;
				case 5:

						tex = Resources.Load<Texture> ("Stage5") as Texture;
						break;
				case 6:

						tex = Resources.Load<Texture> ("Stage6") as Texture;
						break;
				case 7:
			
						tex = Resources.Load<Texture> ("Stage7") as Texture;
						break;
				case 8:
			
						tex = Resources.Load<Texture> ("Stage8") as Texture;
						break;
				case 9:
			
						tex = Resources.Load<Texture> ("Stage9") as Texture;
						break;
				case 10:
			
						tex = Resources.Load<Texture> ("Stage10") as Texture;
						break;
				case 11:
			
						tex = Resources.Load<Texture> ("Stage11") as Texture;
						break;
				case 12:
			
						tex = Resources.Load<Texture> ("Stage12") as Texture;
						break;
				case 13:
			
						tex = Resources.Load<Texture> ("Stage13") as Texture;
						break;
				case 14:
			
						tex = Resources.Load<Texture> ("Stage14") as Texture;
						break;
				case 15:
			
						tex = Resources.Load<Texture> ("Stage15") as Texture;
						break;
				case 16:
			
						tex = Resources.Load<Texture> ("Stage16") as Texture;
						break;
				case 17:
			
						tex = Resources.Load<Texture> ("Stage17") as Texture;
						break;
				case 18:
			
						tex = Resources.Load<Texture> ("Stage18") as Texture;
						break;
				default:

						tex = Resources.Load<Texture> ("Stage" + (int)Random.Range (1, 18)) as Texture;
//						tex = Resources.Load<Texture> ("Stage" + 18) as Texture;
			
			
						break;
				}

		
				GameObject.Find ("parallaxBackground").GetComponent<MeshRenderer> ().material.mainTexture = tex;
				GameObject.Find ("parallaxForground").GetComponent<MeshRenderer> ().material.mainTexture = tex;


		}
	
		// Update is called once per frame
		void Update ()
		{
				float backgroundOffset = Time.timeSinceLevelLoad * backgroundSpeed;
				float foregroundOffset = Time.timeSinceLevelLoad * foregroundSpeed;
		
				background.material.mainTextureOffset = new Vector3 (backgroundOffset, 0,0);

				foreground.material.mainTextureOffset = new Vector3 (foregroundOffset, 0,0);

		}
}
