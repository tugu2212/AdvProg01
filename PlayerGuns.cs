using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerGuns : MonoBehaviour {
	List<GameObject> guns = new List<GameObject>();
	public int equippedWeapon;
	public Transform gunHand;
	public bool[] showGuns;
	GameObject tempGun;
//	List <GameObject> myGuns = new List <GameObject>();
	//make a list and instantiate guns into it
	// Use this for initialization
	void Awake () {
		string resourceName = "Gun";
		tempGun = new GameObject ();
		showGuns = new bool[2];
	//	gunHand = GameObject.FindGameObjectWithTag ("GunHand").transform;
		for (int i = 0; i < 2; i++) {
			tempGun = Instantiate (Resources.Load(resourceName+(i+1)), gunHand.position, gunHand.rotation, gunHand) as GameObject;
			tempGun.SetActive (false);
			guns.Add (tempGun);
		//	guns.Add(new GameObject());
		//	guns [i] = tempGun;
			guns [i].transform.rotation = gunHand.rotation;
			guns [i].transform.position = gunHand.position;
			guns [i].SetActive (false);
			showGuns [i] = false;
		}

		equippedWeapon = 0;
		showGuns [equippedWeapon] = true;
		guns [0].SetActive (true);
	//	SwapWeapon (equippedWeapon);
	//	SwapWeapon (equippedWeapon, (equippedWeapon + 1)%guns.Count);
	}
	
	// Update is called once per frame
	void Update () {
		guns[equippedWeapon].transform.position = gunHand.position;
		guns[equippedWeapon].transform.rotation = gunHand.rotation;
		if (Input.GetKeyDown(KeyCode.Q)) {//"SwapWeapon"
			SwapWeapon(equippedWeapon);
		}
	//	Debug.Log (guns [equippedWeapon].transform.position);
	}

	void SwapWeapon(int weaponSlot)
	{
		int next = (weaponSlot + 1) % guns.Count;
		//set current weapon slot false
	//	int size = guns.Length;
		showGuns[weaponSlot] = false;
		guns [weaponSlot].SetActive (false);
		//set next in slot to true
		equippedWeapon = next;
		showGuns[equippedWeapon] = true;
		guns [equippedWeapon].SetActive (true);
	}
}
