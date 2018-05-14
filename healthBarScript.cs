using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBarScript : MonoBehaviour {

	Image HealthBar;
	public PlayerControl pc;
	float maxHealth=100f;

	// Use this for initialization
	void Start () {
		HealthBar = GameObject.FindGameObjectWithTag ("HealthBar").GetComponent<Image> ();
		pc = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerControl> ();
	}
	
	// Update is called once per frame
	void Update () {
		pc = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerControl> ();
		HealthBar.fillAmount = pc.health / maxHealth;
	}
}
