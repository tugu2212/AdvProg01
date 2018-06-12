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
	bool p;
	// Use this for initialization
	void Start () {
		speed = 10f;	
		pc = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerControl> ();
		pp = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		cam = gameObject.GetComponent<Camera> ();
		dx = 0.0f;
		dy = 0.0f;
		distance = 20.0f;
		p = GameObject.FindGameObjectWithTag ("Sun").GetComponent<DayNight> ().paused;
	}

	// Update is called once per frame
	void Update () {
	//	p = GameObject.FindGameObjectWithTag ("Sun").GetComponent<DayNight> ().paused;
	//	if (!p) {
			if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
				//	Debug.Log (Event.current);
				distance--;
				distance = (distance < 5f) ?  5f : distance;
			} else if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
				//	Debug.Log (Event.current);
				distance++;
				distance = (distance > 30f) ? 30f : distance;
			}

			if (Input.GetMouseButton (2)) {
				dx += Input.GetAxisRaw ("Mouse X");
				dy += Input.GetAxisRaw ("Mouse Y");
			}

			direction = new Vector3 (0.0f, 0.0f, -distance);
			rotation = Quaternion.Euler (-dy, dx, 0);
			transform.position = pp.position + rotation * direction;
			//	pp.position = new Vector3 (pp.position.x, pp.position.y + 3f, pp.position.z);
			transform.LookAt (pp);
	//	}
	}
}