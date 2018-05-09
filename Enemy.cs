using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public Transform target;
	public int Health = 100;
	public float moveSpeed = 1;
	public float turnSpeed = 50f;
	const int MAXSPEED = 522;
	int AttackSpeed = 30;
	int Dmg = 0;
	int Range = 15;
	bool TargetPlayer = false;
	public float targetHP;
	float hitTime;
//	public Transform myTransform; 
	public Enemy(){
			
	}


	// Use this for initialization
	void Start () { 
		hitTime = Time.realtimeSinceStartup;
		target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		targetHP = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerControl> ().health;
		gameObject.transform.Rotate (-90, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		//To move forward and back 
		transform .LookAt(target, new Vector3(0, 1, 0));//.LookAt(target);
		if (!inRange (target)) {
			transform.Translate (Vector3.forward * 1 * Time.deltaTime);
		} else {
			Attack (target.gameObject.GetComponent<PlayerControl>());
		}
		} 
	void OnTriggerEnter(Collider other){
		Debug.Log ("Trigger");
	}

	bool inRange(Transform pt){
		if(Vector3.Distance(gameObject.transform.position, pt.position) < 2f){
		//if(Vector3.Distance(transform.position){
			return true;
		}
			return false;
	}

	void Attack(PlayerControl pt){
		if (Mathf.Abs(hitTime - Time.realtimeSinceStartup) > 3) {
			pt.TakeDamage (10);	
			hitTime = Time.realtimeSinceStartup;
		}
	}
}
