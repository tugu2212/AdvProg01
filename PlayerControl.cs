﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	public float speed;
	public float health;
	float h;
	float v;
	Vector3 movement;
	Vector3 camLookDir;
	Rigidbody playerRB;
	BloodSc bs;
	// Use this for initialization
	void Start () {
		speed  = 10.0f;
		health = 100.0f;
		playerRB = gameObject.GetComponent<Rigidbody> ();
	//	Cursor.lockState = CursorLockMode.Locked;	
		bs = GameObject.FindGameObjectWithTag("Canvas").GetComponentInChildren<BloodSc>();
	}
	// Update is called once per frame
	void Update () {
		Event e = Event.current;
	//	h = Input.GetAxisRaw ("Horizontal");
	//	v = Input.GetAxisRaw ("Vertical");
		if (Input.GetKey (KeyCode.LeftShift)) {
			if (speed >= 20f) {
				speed  = 20f;
			} else {
				speed += 0.1f;
				//drain energy true
			}
		} else {
			if (speed <= 7f) {
				speed = 7f;
			} else {
				speed -= 0.2f;
			}
		}

		if (Input.GetKey (KeyCode.Q)) {
			//	transform.RotateAround (transform.position, new Vector3 (0, 1, 0), 1);
			transform.Rotate (new Vector3 (0, -1, 0));//, Space.World);
		} else if (Input.GetKey (KeyCode.E)) {
			//	transform.RotateAround (transform.position, new Vector3 (0, 1, 0), -1);
			transform.Rotate (new Vector3 (0, 1, 0));//, Space.World);
		}

		if (Input.GetKey (KeyCode.W)) {
			transform.position += transform.forward.normalized * speed * 0.01f;
		} else if (Input.GetKey (KeyCode.S)) {
			transform.position += transform.forward.normalized * (-1) * speed * 0.01f;
		}
		if (Input.GetKey (KeyCode.A)) {
			transform.position += transform.right.normalized * (-1) * speed * 0.01f;
		} else if (Input.GetKey (KeyCode.D)) {
			transform.position += transform.right.normalized * speed * 0.01f;
		}
		if (Input.GetKeyDown (KeyCode.F)) {
			bs.TakeDamage ();
			health -= 1f;
		}
		//save 
		//gameObject.transform.RotateAround(playerRB.transform.position, )

	//	Move (h, v, speed);

	}

	void Move(float h, float v, float runSpeed)
	{
		movement.Set (h, 0f, v);
		movement = movement.normalized * runSpeed * Time.deltaTime;
	//	transform.position = transform.position + movement;
	//	transform.forward
		transform.position = transform.forward.normalized * speed;
	//	playerRB.MovePosition (transform.position + movement);
	}
	public void TakeDamage(int dmg){
		health -= dmg;
		bs.TakeDamage ();
	}
}