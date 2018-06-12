using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour {
	Transform pc;
	float distanceThreshold;
	float angle;
	float dist;
	Vector3 camPos;
	Vector3 mousePos;
	// Use this for initialization
	void Start () {
		pc = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform>();
		distanceThreshold = 20;
		angle = 45f;
		camPos = pc.forward * (-1) * distanceThreshold;
	}
	
	// Update is called once per frame
	void Update () {
	//	gameObject.transform.LookAt (pc);
	//	camPos = pc.forward * (-1) * distanceThreshold;
	//	dist = Vector2.Distance (new Vector2 (pc.position.x, pc.position.z), new Vector2(transform.position.x, transform.position.z));
	//	transform.position = camPos;
	///	gameObject.transform.position = new Vector3 (pc.position.x + Mathf.Cos(angle)* distanceThreshold, pc.position.y + distanceThreshold,
	///		pc.transform.position.z  + Mathf.Sin(angle)* distanceThreshold);

	//	if(Input.GetAxis("Mouse ScrollWheel") > 0){
	//	//	Debug.Log (Event.current);
	//		distanceThreshold--;
	//	} else if(Input.GetAxis("Mouse ScrollWheel") < 0){
	//	//	Debug.Log (Event.current);
	//		distanceThreshold++;
	//	}
//		if (Input.GetMouseButton (0)) {
//			Debug.Log (Input.mousePosition);
//			Debug.Log (Input.GetAxis ("Mouse X"));
//	//		gameObject.transform.RotateAround (pc.position, new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z), 2f);
//			gameObject.transform.RotateAround (pc.position, new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z), 2f);
//		}
	//	gameObject.transform.position = new Vector3 (pc.position.x + camPos.x, pc.position.y + distanceThreshold,
	//		pc.transform.position.z  + camPos.z);
	//	gameObject.transform.position = new Vector3 (
	//		gameObject.transform.position.x, 
	//		pc.position.y + distanceThreshold,
	//		gameObject.transform.position.z);
	}
}
