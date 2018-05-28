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
		blood.color = new Color(blood.color.r, blood.color.g, blood.color.b, 0f);
		showing = false;
	}

	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		pc = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerControl> ();

		//if (pc.health < 50) {
		////	showing = true;
		////	Invoke ("gg", 0f);
		//}
		//else{
		//	blood.enabled = false;
		//}

		if (blood.color.a > 0.05f) {
		//	Debug.Log (blood.color.a);
		//	Debug.Log ("B");
			blood.color = new Color(blood.color.r, blood.color.g, blood.color.b, blood.color.a * 0.95f);
		} else{
		//	Debug.Log (blood.color.a);
		//	Debug.Log ("C");
			blood.color = new Color(blood.color.r, blood.color.g, blood.color.b, 0f);
			showing = false;
		}
		
	}

	public void TakeDamage(){
		if (!showing) {
		//	Debug.Log (blood.color.a);
			showing = !showing;
			if (blood.color.a < 0.5f) {
				blood.color = new Color (blood.color.r, blood.color.g, blood.color.b, 1f);
			}
			//	gg ();
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
