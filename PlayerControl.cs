using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public float speed = 7f;
	public bool isSprinting = false;
	Vector3 movement;
//	Animator anim;
	Rigidbody playerRigidbody;
	int floorMask;
	float camRayLength = 100f;
	public int health = 100;
	public bool isHealing;
	int healAmount = 0;
	bool walking;
	float regenerationSpeed = 1.0f;
	float defaultY;
	/*
	 * energy - stamina | regenSpeed | melee? dmg
	 * agility - movespeed | critmodifier
	 * endurance - sickness resist|duration | total health
	 * perception - ranged dmg
	 */



	void Awake()
	{
		floorMask = LayerMask.GetMask ("Floor");
//		anim = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
		isHealing = false;
		defaultY = 1.5f;
	}

	void FixedUpdate()
	{
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		if (Input.GetKey (KeyCode.LeftShift)) {
			if (speed >= 20f) {
				speed  = 20f;
			} else {
				speed += 0.1f;
				//drain energy true
			}
		} else {
			if (speed <= 7f) {
				speed = 7f;
			} else {
				speed -= 0.2f;
			}
		}
		Move (h, v, speed);
		//function calls
		Turning ();
//		Animating (h, v); 
		if (isHealing && Time.timeScale != 0) {
			StartCoroutine(Regenerate(isHealing, healAmount, regenerationSpeed));
		}
		transform.position = new Vector3 (transform.position.x, defaultY, transform.position.z);
	}

	void Move(float h, float v, float runSpeed)
	{
		movement.Set (h, 0f, v);
		movement = movement.normalized * runSpeed * Time.deltaTime;
		playerRigidbody.MovePosition (transform.position + movement);
	}

	void Turning()
	{
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;
		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) {
			Vector3 playerToMouse = floorHit.point - transform.position; //vector from player to mouse location
			//TODO: transform.position <- position of the player
			playerToMouse.y = 0f; //not to make player lean back
			Quaternion newRotation = Quaternion.LookRotation(playerToMouse); // Quaternion for rotation, not Vector3
			playerRigidbody.MoveRotation(newRotation);
		}
	}

	public void Heal(bool regenOn, int hp)
	{
		if (isHealing) {
			healAmount += hp;
		}
		isHealing = regenOn;
		healAmount = hp;
	}
	IEnumerator Regenerate(bool regenOn, int a, float regenSpeed)
	{
		isHealing = false;
		while( a > 0) {
			health = health + (int)regenSpeed; //(int)(regenSpeed * Time.deltaTime)
			yield return null;
			a--;
		}
	//	Debug.Log ("Regen complete");
		yield return null;
		CancelInvoke ();
	}

	void Animating(float h, float v)
	{
//		walking = (h != 0f || v != 0f);
//		anim.SetBool ("IsWalking", walking); //name of the animation in Animation Controller
	}

	public void TakeDamage(int dmg) //Vector3 Position
	{
		health -= dmg;
		if (health <= 0) {
			Die ();
		}
	}

	void Die()
	{
		gameObject.SetActive (false);
		//animation
		//sound
		//invoke game over after animation time
		return;
	}
}
