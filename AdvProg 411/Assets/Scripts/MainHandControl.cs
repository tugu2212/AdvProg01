using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHandControl : MonoBehaviour {
	public Transform hand;
	GameObject sword;
	public List<GameObject> swords;
	string[] swordList;
	int swordListSize;
	int currentSword;
	// Use this for initialization
	void Start () {
		currentSword = 0;
		swordListSize = 3;
		swordList = new string[3];
		swordList [0] = "sword";
		swordList [1] = "holysword_noLOD";
		swordList [2] = "dragonslayer_noLOD";
		//TODO: put item names in ItemDatabase
	//	hand = GameObject.FindGameObjectWithTag ("RightHand").GetComponent<Transform> ();
		hand = gameObject.transform;
	//	transform.position = hand.position;
	//	transform.rotation = hand.rotation;
	//	Debug.Log (gameObject.GetComponentInParent<Transform>());
	//	Debug.Log (transform);
		for (int i = 0; i < swordListSize; i++) {
			swords.Add(GameObject.Instantiate(Resources.Load (swordList[i]), hand.position, hand.rotation) as GameObject);
			swords[i].SetActive (false);
		}
		swords [currentSword].SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("1")) {
			swords [currentSword].SetActive (false);
			currentSword = (currentSword + 1)%3;
			swords [currentSword].SetActive (true);
		}
	//	hand = GameObject.FindGameObjectWithTag ("RightHand").GetComponent<Transform> ();
		swords[currentSword].transform.position = hand.position;
		swords[currentSword].transform.rotation = hand.rotation;
		//transform.position = hand.position;
		//transform.rotation = hand.rotation;
	}
}
