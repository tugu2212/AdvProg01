using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodSc : MonoBehaviour {
	public Image blood;
	public PlayerControl pc;
	public float hitTime;
	public bool showing;
	public float timer;
	// Use this for initialization
	void Start () {
	//	Blood = GameObject.Find ("Canvas").GetComponentsInParent<Image> ();
		blood = gameObject.GetComponent<Image>();
		pc = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerControl> ();

		showing = false;
	}

	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		pc = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerControl> ();

		if (pc.health < 50) {
		//	showing = true;
		//	Invoke ("gg", 0f);
		}
		else{
			blood.enabled = false;
		}
		
	}

	public void TakeDamage(){
		if (!showing) {
			showing = !showing;
			gg ();
		}
	}

	void gg(){
		hitTime = Time.time;
		//while (showing) {
			
			if (hitTime - Time.time > 1f) {
				showing = false;
			}
		//}
		blood.fillAmount = pc.health;
		CancelInvoke ();
	}
}
