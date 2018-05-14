using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	public float speed;
	public PlayerControl pc;
	public Camera cam;
	public float dx, dy;
	private Vector3 direction;
	private float distance;
	private Quaternion rotation;
	public Transform pp;
	// Use this for initialization
	void Start () {
		speed = 10f;	
		pc = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerControl> ();
		pp = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		cam = gameObject.GetComponent<Camera> ();
		dx = 0.0f;
		dy = 0.0f;
		distance = 20.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (2)) {
			dx += Input.GetAxisRaw ("Mouse X");
			dy += Input.GetAxisRaw ("Mouse Y");
		}

		direction = new Vector3 (0.0f, 0.0f, -distance);
		rotation = Quaternion.Euler (-dy, dx, 0);
		transform.position = pp.position + rotation * direction;

		transform.LookAt (pp);
	}
}
