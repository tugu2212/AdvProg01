using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimapfollow : MonoBehaviour {
	Transform player;
	public float camHeight;
	public PlayerControl pc;
	RectTransform arrow;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		camHeight = 50f;
		pc = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerControl> ();
	}
	
	// Update is called once per frame
	void Update () {

		gameObject.transform.position = new Vector3(player.position.x, camHeight, player.position.z);

	}
}
