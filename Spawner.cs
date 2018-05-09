using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	List<GameObject> spawners;

	public int maxSpawners;
	// Use this for initialization
	void Start () {
		//	timer = 0;
		maxSpawners = 4;
		for (int i = 0; i < maxSpawners; i++) {
			GameObject tempSpawner = Instantiate(Resources.Load<GameObject>("Spawner"), transform.position, transform.rotation) as GameObject;
			tempSpawner.SetActive (true);
		}
	}

	// Update is called once per frame
	void Update () {
	}
}