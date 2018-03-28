using UnityEngine;

public class PlayerShootRay : MonoBehaviour
{
	public int damagePerShot = 20;
	public float timeBetweenBullets = 0.15f;
	public float range = 10f;
	public Transform gunPoint;
	Transform gunHand;

	float timer;
	Ray shootRay;
	RaycastHit shootHit;
//	int shootableMask;
	//	ParticleSystem gunParticles;
	LineRenderer gunLine;
	//	AudioSource gunAudio;
	Light gunLight;
	float effectsDisplayTime = 0.2f;
		
	void Awake ()
	{
		gunHand = GameObject.FindGameObjectWithTag ("GunHand").GetComponent<Transform> ();
//		shootableMask = LayerMask.GetMask ("Floor");//("Shootable");
		//	gunParticles = GetComponent<ParticleSystem> ();
		gunLine = GetComponent <LineRenderer> ();
		//		gunAudio = GetComponent<AudioSource> ();
		gunLight = GetComponent<Light> ();
	}

//	void OnEnable()
//	{
//		transform.position = gunHand.position;
//		MeshRenderer mr = GetComponent<MeshRenderer> ();
//		Debug.Log (mr.enabled);
//	}

	void Update ()
	{
		transform.position = gunHand.position;
		transform.rotation = gunHand.rotation;
		timer += Time.deltaTime;

		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
		{
			Shoot();
		}

		if(timer >= timeBetweenBullets * effectsDisplayTime)
		{
			DisableEffects ();
		}
	}


	public void DisableEffects ()
	{
		gunLine.enabled = false;
		gunLight.enabled = false;
	}


	void Shoot()
	{
		timer = 0f;

		//		gunAudio.Play ();
	//	gunLight.transform.position = gunPoint.position;
		gunLight.enabled = true;

		//	gunParticles.Stop ();
		//	gunParticles.Play ();

		gunLine.enabled = true;
		//	gunLine.SetPosition (0, transform.position);
		//	shootRay.origin = transform.position;
		//	shootRay.direction = transform.forward;
		gunLine.SetPosition (0, gunPoint.position);
		shootRay.origin = gunPoint.position;
		shootRay.direction = gunPoint.forward;
		//	
		if(Physics.Raycast (shootRay, out shootHit, range))
		{
			EnemyMovement enemyHealth = shootHit.collider.GetComponent <EnemyMovement> ();
			//if(enemyHealth != null)
				//	if(shootHit.collider.CompareTag("Enemy"))
			//if (losHit.transform.CompareTag ("Player"))
			if(shootHit.transform.CompareTag("Enemy") && enemyHealth != null)
			{
				Debug.Log ("hit an enemy");
				enemyHealth.TakeDamage (damagePerShot, shootHit.point);			
			}
			gunLine.SetPosition (1, shootHit.point);
		}
		else
		{
			gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
		}
	}
}
