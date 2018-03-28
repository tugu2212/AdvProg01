using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ground : MonoBehaviour {
	public	List<GameObject> dropList = new List<GameObject>();
	public int listSize = 2;
	public int spareSlot;
	GameObject obj;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < listSize; i++) {
			GameObject o1 = new GameObject ();
			o1.SetActive (false);
			dropList.Add (o1);
		}
		spareSlot = 0;
	}
	
	// Update is called once per frame
	void Update () {

	}
	//TODO: dropChance - random drop chance
	public void addToList(Transform tform, int type, float dropChance)//GameObject obj, 
	{
		float r = Random.Range(0.0f, 1.0f);
	//	obj = Instantiate (Resources.Load ("HealthOnPick"), tform.position, tform.rotation) as GameObject;
	//	obj.transform.position = tform.position;
	//	obj.SetActive (false);
		if (r < dropChance) {
			dropList [spareSlot].SetActive (false);
			Destroy (dropList [spareSlot]);
			dropList [spareSlot] = Instantiate (Resources.Load ("HealthOnPick"), tform.position, tform.rotation) as GameObject;
			dropList [spareSlot].transform.position = tform.position;
		//	if (dropList [spareSlot].activeInHierarchy) {
			dropList [spareSlot].SetActive (true);
			spareSlot++;
			if (spareSlot == listSize) {
				spareSlot = 0;
			}
		//	}
		//	dropList [spareSlot].SetActive (true);
		}
		Debug.Log ("Dropped!");
	}
}
