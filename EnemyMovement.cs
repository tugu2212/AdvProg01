using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
	Transform player;
	NavMeshAgent nav;
	private float rotSpeed = 5.0f;
	public int health = 100;
	EnemyGunControl egc;
//	PlayerControl pc;
	// los - line of sight
	Ray losRay;
	RaycastHit losHit;
//	int losMask;
//	float range = 50f;
//	LineRenderer losLine;
	Vector3 directionToTarget;
	int shootCounter = 0;
	GameObject drop;
	float dropChance = 0.5f;
	public Ground ground;//
	float gunRange;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		nav = GetComponent < NavMeshAgent > ();
		gunRange = nav.stoppingDistance + 1f;
	//	losLine = GetComponent<LineRenderer> ();
	//	losLine.enabled = true;
//		losMask = LayerMask.GetMask ("Floor");
		egc = GetComponentInChildren<EnemyGunControl> ();
		shootCounter = 30;
		ground = GameObject.FindGameObjectWithTag ("Wall").GetComponentInParent<Ground> ();
//		pc = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerControl>();
	}
	
	// Update is called once per frame
	void Update () {
		if (shootCounter <= 0) {
			shootCounter = 60;
		}
		directionToTarget = (player.position - transform.position);
		nav.SetDestination (player.position);
	//	losLine.SetPosition (0, transform.position);
		losRay.origin = transform.position;
		losRay.direction = player.position - transform.position;
	//	if(Physics.Raycast (losRay, out losHit, range , losMask))
		if (shootCounter == 60 && health > 0 && isInRange()) {//
			egc.ShootBullet(gunRange);
			//Shoot();
		}else if (health <= 0) {
			Die ();
		//	gameObject.SetActive (false);
		}
		shootCounter--;
	//	transform.Rotate (transform.position - player.position);
		if (isInRange()) {
			RotateTowards ();
		}
		//float angle = Vector3.Angle (transform.forward, directionToTarget);
		//if (Mathf.Abs (angle) > 90 && Mathf.Abs (angle) < 270) {	
		//	Debug.DrawLine (transform.position, player.position, Color.blue);
		//}
	//	Debug.DrawRay (transform.position, directionToTarget, Color.black);
		
	}

	bool isInRange()
	{
		float distance = Vector3.Distance (transform.position, player.position);
		return (distance < nav.stoppingDistance);
	}

	void RotateTowards()
	{
		Vector3 direction = (directionToTarget).normalized;
		Quaternion lookTo = Quaternion.LookRotation (new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp (transform.rotation, lookTo, Time.deltaTime * rotSpeed);
	}

	bool inFront()
	{
	//	Vector3 directionToTarget = (player.position - transform.position);
	//	float angle = Vector3.Angle (transform.forward, directionToTarget);
	//	if (Mathf.Abs (angle) > 90 && Mathf.Abs (angle) < 270) {	
	//		Debug.DrawLine (transform.position, player.position, Color.blue);
	//		return true;
	//	}
	//	Debug.DrawLine (transform.position, player.position, Color.blue);
		return false;
	}

	public void TakeDamage(int dmg, Vector3 hitPos)
	{
		health = health - dmg;
	}

	public void Die()
	{
	//	Debug.Log ("dead");
		//TODO: Run back to office room (spawning room)
		ground.addToList(transform, 1, dropChance);
		gameObject.SetActive (false);
	}

	public void Respawn()
	{
		health = 100;
		gameObject.GetComponent<Rigidbody> ().isKinematic = false;

	}
//	public void OnDisable()
//	{
//		//drop item with chance
//		//TODO sth wrong here
//		//	Instantiate(Resources.Load("HealthOnPick"), transform.position, transform.rotation);
//	}

//	void Shoot()
//	{
//		//if player is in lineOfSight
//		//if (Physics.Raycast (losRay, out losHit)) {
//		//	if (losHit.transform.CompareTag ("Player")) {
//		//		losLine.SetPosition (1, losHit.point);
//				egc.ShootBullet ();
//				//shoot
//				//TODO takedamage on guns - rays/bullets
//				//remove pc.TakeDamage and add to each Guns Specifically, cuz not all guns are rays, ranged etc
//				//pc.TakeDamage (egc.damagePerShot);
//		//	}
//		//
//	}
}
