// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class MainHandControl : MonoBehaviour {
// 	Transform hand;
// 	GameObject sword;
// 	// Use this for initialization
// 	void Start () {
// 		hand = GameObject.FindGameObjectWithTag ("RightHand").GetComponent<Transform> ();
// 	//	transform.position = hand.position;
// 	//	transform.rotation = hand.rotation;
// 		sword = GameObject.Instantiate(Resources.Load ("sword"), hand.position, hand.rotation) as GameObject;
// 	//	Debug.Log (gameObject.GetComponentInParent<Transform>());
// 	//	Debug.Log (transform);
// 	}
	
// 	// Update is called once per frame
// 	void Update () {
// 		//transform.position = hand.position;
// 		//transform.rotation = hand.rotation;
// 		sword.transform.position = hand.position;
// 		sword.transform.rotation = hand.rotation;
// 	}
// }
