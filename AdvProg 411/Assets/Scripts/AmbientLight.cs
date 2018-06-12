using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientLight : MonoBehaviour {

	public Light sun;
	public float threshold;
	// Use this for initialization
	void Start () {
		threshold = 0.7f;
		sun = GameObject.FindGameObjectWithTag ("Sun").GetComponent<Light>();
		gameObject.GetComponent<Light> ().intensity = threshold;
	}
	
	// Update is called once per frame
	void Update () {
		if (sun.intensity > 0) {
			gameObject.GetComponent<Light> ().intensity = threshold - sun.intensity;
		}
	}
}
