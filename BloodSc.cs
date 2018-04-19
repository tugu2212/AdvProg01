using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodSc : MonoBehaviour {
	public Image blood;
	public PlayerControl pc;


	// Use this for initialization
	void Start () {
	//	Blood = GameObject.Find ("Canvas").GetComponentsInParent<Image> ();
		blood = gameObject.GetComponent<Image>();
		pc = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerControl> ();


	}

	
	// Update is called once per frame
	void Update () {
		
		pc = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerControl> ();



		if (pc.health < 50) {
			Invoke ("gg", 2f)
		}
		else{
			blood.enabled = false;
		}
		
	}

	void gg(){
		blood.enabled = true;
	}
}
