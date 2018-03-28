using UnityEngine;

public class EnemyGunControl : MonoBehaviour
{
	public int damagePerShot = 20;
	public float timeBetweenBullets = 2f;
	public float range = 10f;
	public Transform gunPoint;

	float timer;
	Ray shootRay;
	RaycastHit shootHit;
	int shootableMask;
	//	ParticleSystem gunParticles;
	LineRenderer gunLine;
	//	AudioSource gunAudio;
	Light gunLight;
	float effectsDisplayTime = 0.2f;
	PlayerControl pc;
	GameObject player;

	void Awake ()
	{
		shootableMask = LayerMask.GetMask ("Floor");//("Shootable");
		//	gunParticles = GetComponent<ParticleSystem> ();
		gunLine = GetComponent <LineRenderer> ();
		//		gunAudio = GetComponent<AudioSource> ();
		gunLight = GetComponent<Light> ();
	//	pc = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerControl>();
		player = GameObject.FindGameObjectWithTag("Player");
		pc = player.GetComponent <PlayerControl> ();
	}

	void Update ()
	{
		timer += Time.deltaTime;

	//	if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
	//	{
	//		ShootBullet ();
	//	}
	//
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


	public void ShootBullet(float gunRange)
	{
		if (timer >= timeBetweenBullets && Time.timeScale != 0) {
			timer = 0f;
			//		gunAudio.Play ();
			gunLight.enabled = true;
			//	gunParticles.Stop ();
			//	gunParticles.Play ();
			gunLine.enabled = true;
			//	gunLine.SetPosition (0, transform.position);
			//	shootRay.origin = transform.position;
			//	shootRay.direction = transform.forward;
			gunLine.SetPosition (0, gunPoint.position);
			shootRay.origin = gunPoint.position;
			shootRay.direction = (player.transform.position - transform.position).normalized;// gunPoint.forward;
			//	
			if(Physics.Raycast (shootRay, out shootHit, gunRange))//, shootableMask //gunRange = range TODO: range on gun or enemy?
			{
			//	PlayerHealth playerhealth = shootHit.collider.GetComponent <EnemyHealth> ();
			//	if(enemyHealth != null)
			//	{
			//		enemyHealth.TakeDamage (damagePerShot, shootHit.point);
			//	}
			//	gunLine.SetPosition (1, shootHit.point);
				if(shootHit.transform.CompareTag("Player") && pc != null)
				{
				//	Debug.Log ("hit the Player by" + transform.parent.ToString());
					pc.TakeDamage (damagePerShot);			//, shootHit.point - later for particles
				}
			//	Debug.Log(pc);
			//	Debug.Log (shootHit.transform.tag);
				gunLine.SetPosition (1, shootHit.point);
			}
			else
			{
				gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
			}
		}

	}
}
