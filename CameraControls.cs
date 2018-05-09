using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour {
	Transform pc;
	float distanceThreshold;
	float angle;
	float dist;
	Vector3 camPos;
	// Use this for initialization
	void Start () {
		pc = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform>();
		distanceThreshold = 20;
		angle = 45f;
		camPos = pc.forward * (-1) * distanceThreshold;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.LookAt (pc);
		camPos = pc.forward * (-1) * distanceThreshold;
		dist = Vector2.Distance (new Vector2 (pc.position.x, pc.position.z), new Vector2(transform.position.x, transform.position.z));
	//	transform.position = camPos;
	///	gameObject.transform.position = new Vector3 (pc.position.x + Mathf.Cos(angle)* distanceThreshold, pc.position.y + distanceThreshold,
	///		pc.transform.position.z  + Mathf.Sin(angle)* distanceThreshold);
		gameObject.transform.position = new Vector3 (pc.position.x + camPos.x, pc.position.y + distanceThreshold,
			pc.transform.position.z  + camPos.z);
		if(Input.GetAxis("Mouse ScrollWheel") > 0){
		//	Debug.Log (Event.current);
			distanceThreshold--;
		} else if(Input.GetAxis("Mouse ScrollWheel") < 0){
		//	Debug.Log (Event.current);
			distanceThreshold++;
		}
	//	if (Input.GetKey (KeyCode.Q)) {
	//		transform.ro
	//	}
	//	if (Input.GetKeyDown (KeyCode.Mouse0)) {

	//	}

	//	if (e.type == EventType.mouseDrag && e.button == 0) {
	//		camLookDir = new Vector3 (0f, 1f, 0);
	//	}

	}
}
