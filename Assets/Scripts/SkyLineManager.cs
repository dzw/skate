﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkyLineManager : MonoBehaviour {

	public Transform prefab;
	public int numberOfBlocks;
	public Vector3 startingPosition;
	public float recycleOffset;
	private Vector3 nextPosition;
	private Queue<Transform> objectQueue;

	public Vector3 minSize,maxSize;
	// Use this for initialization
	void Start () {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		objectQueue = new Queue<Transform> (numberOfBlocks);
		nextPosition = startingPosition;
		for (int i=0; i<numberOfBlocks; i++) {
		
			objectQueue.Enqueue((Transform)Instantiate(prefab));
		
		}

		for (int i=0; i<numberOfBlocks; i++) {
			Recycle();
		}
	
	}
	private void GameStart () {
		nextPosition = startingPosition;
		for(int i = 0; i < numberOfBlocks; i++){
			Recycle();
		}
		enabled = true;
	}
	
	private void GameOver () {
		enabled = false;
	}
	// Update is called once per frame
	void Update () {
		if (objectQueue.Peek ().localPosition.x + recycleOffset < Runner.distanceTraveled) {
			Recycle();

		}
	
	}
	public void Recycle()
	{
		Vector3 scale = new Vector3 (Random.Range(minSize.x,maxSize.x),
		                             Random.Range(minSize.y,maxSize.y),
		                             Random.Range(minSize.z,maxSize.z)
		                             );
		Vector3 position = nextPosition;
		position.x += scale.x * 0.5f;
		position.y += scale.y * 0.5f;

		Transform o = objectQueue.Dequeue ();
		o.localScale = scale;
		o.localPosition = position;
		nextPosition.x += scale.x;
		objectQueue.Enqueue (o);

	}
}