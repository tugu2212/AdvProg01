using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	Transform target;
	public int Health = 100;
	public float moveSpeed = 1;
	public float turnSpeed = 50f;
	const int MAXSPEED = 522;
	int AttackSpeed = 30;
	int Dmg = 0;
	int Range = 15;
	bool TargetPlayer = false;
	public Transform myTransform; 
	public Enemy(){
			
	}


	// Use this for initialization
	void Start () { 
		target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		Debug.Log (target);
		Debug.Log (target.position);
	}
	
	// Update is called once per frame
	void Update () {
		//To move forward and back 
		transform.LookAt(target);
		transform.Translate (Vector3.forward * 1 * Time.deltaTime);
		 
		} 
	void Move(){
	}
	void ChangeRed(){
			gameObject.GetComponent<Renderer> ().material.color = Color.red;
	 
	}
	void MoveForward(){
		gameObject.transform.Translate (Vector3.right * moveSpeed * Time.deltaTime);
	}
	void MoveBackward(){
		gameObject.transform.Translate (Vector3.left * moveSpeed * Time.deltaTime);
	}
	void Rotate(){
		gameObject.transform.Rotate (Vector3.up, turnSpeed * Time.deltaTime);
	}

		
}
