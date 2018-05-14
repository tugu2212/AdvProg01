using UnityEngine;
using System.Collections;
using System.Collections.Generic;
class EnemySpawner : MonoBehaviour {
	GameObject[] enemies = new GameObject[2];
	int spawnPeriod;
	public int count;
	// Use this for initialization
	void Start () {
	//	timer = 0;
		count = 0;
		spawnPeriod = 5;
		for (int i = 0; i < enemies.Length; i++) {
			enemies[i] = Instantiate(Resources.Load<GameObject>("Cylinder"), gameObject.GetComponentInParent<Transform>().position, gameObject.GetComponentInParent<Transform>().rotation) as GameObject;
			enemies [i].SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if ((int)Time.realtimeSinceStartup % spawnPeriod == 0) {
			for (int i = 0; i < enemies.Length; i++) {
				if (!enemies [i].activeInHierarchy) {
					enemies [i].transform.position = transform.position;
					enemies [i].transform.rotation = transform.rotation;
					//reset gameobject
					Enemy em = enemies[i].GetComponent<Enemy>();
				//	em.Respawn();
					enemies [i].SetActive (true);
					count++;
					break;
				}				
			}
		}
	}
}
