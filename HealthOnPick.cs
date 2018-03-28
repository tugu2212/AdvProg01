using UnityEngine;
using System.Collections;

public class HealthOnPick : MonoBehaviour {
	PlayerControl pc;
	int hp;
	// Use this for initialization
	void Start () {
		hp = 10;
		Physics.IgnoreLayerCollision (9, 10);//later note: drops and enemies layers
		//(LayerMask.NameToLayer ("Enemies"), LayerMask.NameToLayer ("Drops"));

	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (0f, 0f, 2f));

	}

	public HealthOnPick(int health)//, Transform loc
	{
		hp = health;
	//	transform.position = loc.position;
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.CompareTag ("Player")) {
			pc = collision.gameObject.GetComponent<PlayerControl> ();
			pc.Heal (true, hp);
			//play particle effect
			Debug.Log (collision.collider.tag);
			gameObject.SetActive (false);//(gameObject);
		} 
		//else if (collision.transform.CompareTag ("Enemy")) {
		//	gameObject.GetComponent<Rigidbody> ().isKinematic = true;
		////	collision.rigidbody.isKinematic = true;
		//}
		return;
	}

	//void OnCollisionExit(Collision collision)
	//{
	////	if (collision.transform.CompareTag ("Enemy"))  collision.rigidbody.isKinematic = false;
	//	gameObject.GetComponent<Rigidbody> ().isKinematic = false;
	//}
}