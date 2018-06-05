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
	int Score = 10;
	bool TargetPlayer = false;
	public float targetHP;
	float hitTime;
	DayNight sun;
	bool p;
	public bool hit = false;
//	public Transform myTransform; 
	public Enemy(){
			
	}


	// Use this for initialization
	void Start () { 
		hit = false;
		hitTime = Time.realtimeSinceStartup;
		target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		targetHP = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerControl> ().health;
		gameObject.transform.Rotate (-90, 0, 0);

		sun = GameObject.FindGameObjectWithTag ("Sun").GetComponent<DayNight> ();

		p = GameObject.FindGameObjectWithTag ("Sun").GetComponent<DayNight> ().paused;
	}
//	void OnEnable(){
//		Invoke ("Die", 30f);
//	}
	// Update is called once per frame
	void Update () {

		p = GameObject.FindGameObjectWithTag ("Sun").GetComponent<DayNight> ().paused;
		//To move forward and back 
	//	transform .LookAt(target, new Vector3(0, 1, 0));//.LookAt(target);
		if (p) {
			hitTime += Time.deltaTime;
			//gameObject.GetComponent<Rigidbody> ().freezeRotation = true;
		}
		else if (!p) {
			//gameObject.GetComponent<Rigidbody> ().freezeRotation = false;
			transform.LookAt (new Vector3 (target.position.x, 0, target.position.z));
			if (!inRange (target)) {
				transform.Translate (Vector3.forward * 1 * Time.deltaTime);
			} else {
				Attack (target.gameObject.GetComponent<PlayerControl> ());
			}
			if (Health <= 0) {
				Die ();
			}
		} 
	} 
	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Player") {
		//	Debug.Log ("Trigger");
		}
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

	public void Die(){
		//give points
		sun.Score += Score;//(Score);
		//drop items 
		 	
			gameObject.SetActive (false);
			FindObjectOfType<PlayerControl> ().enemyEntered = false;

	 
	//	if (Random.Range (0, 100) > chance) {
	//		dropItem ();
	//	}
	}
	public void takeDamage(int dmg){
		 
			Health -= dmg;
			if (Health <= 0) {
				Die ();
			}  

	}
	
}
