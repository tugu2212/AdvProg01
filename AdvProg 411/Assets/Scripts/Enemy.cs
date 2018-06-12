using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public Transform target;
	public int Health = 100;
	public float moveSpeed = 1f;
	public float turnSpeed = 50f;
	const int MAXSPEED = 522;
	int AttackSpeed = 30;
	int Dmg = 0;
	float Range = 5f;
	float AttackRange = 3f;
	int Score = 10;

	bool TargetPlayer = false;
//	public float targetHP;

	float hitTime;
	DayNight sun;
	bool p; //game paused state
	public Transform home;
	float goBackDistance;
	Vector3[] patrolPoint;
	int goalIndex;//patrolPoint index
	// Use this for initialization
	public bool inAttackRange;
	Rigidbody rb;

	enum AIState {goingHome, patrolling, engaging};
	AIState curState;
	void Start () { 
		inAttackRange = false;
		rb = gameObject.GetComponent<Rigidbody> ();
		curState = AIState.patrolling;
		goBackDistance = 10f;
		hitTime = Time.realtimeSinceStartup;
		target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
	//	targetHP = target.gameObject.GetComponent<PlayerControl> ().health;
		gameObject.transform.Rotate (-90, 0, 0);

		sun = GameObject.FindGameObjectWithTag ("Sun").GetComponent<DayNight> ();

		p = GameObject.FindGameObjectWithTag ("Sun").GetComponent<DayNight> ().paused;

		patrolPoint = new Vector3[2];
		patrolPoint [0] = new Vector3 (home.position.x - Random.Range (-(goBackDistance-1f), (goBackDistance-1f)), 0f,
			home.position.z - Random.Range (-(goBackDistance-1f), (goBackDistance-1f)));
		patrolPoint [1] = new Vector3 (home.position.x + Random.Range (-(goBackDistance-1f), (goBackDistance-1f)), 0f, 
			home.position.z + Random.Range (-(goBackDistance-1f), (goBackDistance-1f)));
		while (Vector3.Distance (patrolPoint [0], patrolPoint [1]) < goBackDistance/2f) {
			patrolPoint [1] = new Vector3 (home.position.x + Random.Range (-(goBackDistance-1f), (goBackDistance-1f)), 0f, 
				home.position.z + Random.Range (-(goBackDistance-1f), (goBackDistance-1f)));
		}
		goalIndex = 0;
	}
	void OnEnable(){
		//Invoke ("Die", 30f);
		inAttackRange = false;
		curState = AIState.goingHome; 
	}
	// Update is called once per frame
	void Update () {

		p = GameObject.FindGameObjectWithTag ("Sun").GetComponent<DayNight> ().paused;
		//To move forward and back 
		//	transform .LookAt(target, new Vector3(0, 1, 0));//.LookAt(target);
	//	gameObject.GetComponent<Rigidbody> ().freezeRotation = p;
		if (p) {
			hitTime += Time.deltaTime;
			if (!rb.IsSleeping()) {
				rb.Sleep();
			}
			//gameObject.GetComponent<Rigidbody> ().freezeRotation = true;
		}
		else if (!p) {
			if (rb.IsSleeping()) {
				rb.WakeUp();
			}
			//gameObject.GetComponent<Rigidbody> ().freezeRotation = false;
			if (curState == AIState.patrolling) {
				if (inRange (patrolPoint [goalIndex], Range)) {
					goalIndex = (goalIndex + 1) % patrolPoint.Length;
				}

				transform.LookAt (patrolPoint[goalIndex]);
				transform.Translate (Vector3.forward * moveSpeed * 2 * Time.deltaTime);

				if (inRange (target.position, Range)) {
					curState = AIState.engaging;
				}

			} else if (curState == AIState.goingHome) {
				transform.LookAt (new Vector3 (home.position.x, 0, home.position.z));
				transform.Translate (Vector3.forward * moveSpeed * 2 * Time.deltaTime);
				if (inRange (home.position, Range)) {
					curState = AIState.patrolling;
				}
			} else if (curState == AIState.engaging) {
				transform.LookAt (new Vector3 (target.position.x, 0, target.position.z));
				if (!inRange (target.position, AttackRange)) {
					transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime);
					//patrol

				} else {
					Attack (target.gameObject.GetComponent<PlayerControl> ());
				}


			}

			if ((curState != AIState.goingHome) && (Vector3.Distance (transform.position, home.position) > goBackDistance)) {
			//	Debug.Log (gameObject.name + (": I'm too far away!"));
				curState = AIState.goingHome;
			}

			if (Health <= 0) {
				Die ();
			}
		} 
	} 
	void OnCollisionEnter(Collision other){
	//	if (other.gameObject.tag == "Player") {
	//		Debug.Log ("Trigger");
	//	}
	}

	bool inRange(Vector3 pt, float checkRange){
		if(Vector3.Distance(gameObject.transform.position, pt) < checkRange){
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

	void Die(){
		//give points
		sun.Score += Score;//(Score);
		//drop items
		gameObject.SetActive (false);
	//	if (Random.Range (0, 100) > chance) {
	//		dropItem ();
	//	}
	}

	public void TakeDamage(int dmg){
		Health -= dmg;
	}
}
