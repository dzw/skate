using UnityEngine;
using System.Collections;

public class Stage : MonoBehaviour
{

		public string name;
		public Sprite background;

		public Stage (string name)
		{
				this.name = name;

				background = Resources.Load<Sprite> ("" + name);
		}
}
