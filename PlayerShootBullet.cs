using UnityEngine;
using System.Collections.Generic;

public class PlayerShootBullet : MonoBehaviour
{
	public int damage = 20;
	public float timeBetweenBullets = 1f;
	public float range = 10f;
	public Transform gunPoint;
	Transform gunHand;
	public int bulletAmount = 5;
	public List<GameObject> bullets;
	public GameObject bullet;
	float timer;
	float reloadTime = 1.5f;
	int currentBullet = 0;
	List<bool> isFired = new List<bool>();//size = bulletamount
	bool isReloading = false;
	//  int shootableMask;
	//	ParticleSystem gunParticles;
	//	AudioSource gunAudio;
	Light gunLight;
	float effectsDisplayTime = 0.2f;

	void Awake ()
	{
		gunHand = GameObject.FindGameObjectWithTag ("GunHand").GetComponent<Transform> ();
		//	shootableMask = LayerMask.GetMask ("Floor");//("Shootable");
		//	gunParticles = GetComponent<ParticleSystem> ();
		//		gunAudio = GetComponent<AudioSource> ();
		gunLight = GetComponent<Light> ();
		bullets = new List<GameObject> ();
		for (int i = 0; i < bulletAmount; i++) {
		//	GameObject obj = Instantiate(Resources.Load("Bullet"), gunPoint.position, gunPoint.rotation) as GameObject;
			GameObject obj = (GameObject)Instantiate(bullet);
			obj.SetActive (false);
			bullets.Add (obj);
			isFired.Add(false);
		}
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
		if(Input.GetKeyDown("r")){
			if(!isReloading)
			{
				isReloading = true;
				//call reload animation
				Invoke ("Reload", reloadTime);
			//	Debug.Log ("reloading");
			}
		}
		if(Input.GetButton ("Fire2") && timer >= timeBetweenBullets && Time.timeScale != 0)	{
			if(!isReloading)
			Shoot();
		}
		if(timer >= timeBetweenBullets * effectsDisplayTime){
			DisableEffects ();
		}
	}


	public void DisableEffects ()
	{
		gunLight.enabled = false;
	}


	void Shoot()
	{
		timer = 0f;
		//		gunAudio.Play ();
	//	gunLight.transform.position = gunPoint.position;

		//	gunParticles.Stop ();
		//	gunParticles.Play ();
		//for (int i = bullets.Count; i < 0; i--) {
		//	if (!bullets [i].activeInHierarchy && curBullet > 0 && isFired[i] == false) {
		//		bullets [i].transform.position = gunHand.position;
		//		bullets [i].transform.rotation = gunHand.rotation;
		//		bullets [i].SetActive (true);
		//		gunLight.enabled = true;
		//		isFired [i] = true;
		//		break;
		//	} else {
		//		Invoke ("Reload", 1.5f);
		//	}
		//}
		if ((currentBullet > bulletAmount - 1) && !isReloading) {
			isReloading = true;
			//call reload animation
			Invoke ("Reload", reloadTime);
		//	Debug.Log ("reloading");
		}
		else if (!bullets [currentBullet].activeInHierarchy && isFired[currentBullet] == false) {
			bullets [currentBullet].transform.position = gunHand.position;
			bullets [currentBullet].transform.rotation = gunHand.rotation;
			bullets [currentBullet].SetActive (true);
			gunLight.enabled = true;
			isFired [currentBullet] = true;
		}
		currentBullet++;
	}

	void Reload()
	{
		currentBullet = 0;
		for (int i = 0; i < isFired.Count; i++) {
			isFired [i] = false;
		}
		isReloading = false;
	}
}
