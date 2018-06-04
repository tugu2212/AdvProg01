using System.Collections.Generic;
 using UnityEngine;
 
public class Spawner : MonoBehaviour {

    public GameObject enemy;                // The enemy prefab to be spawned.    
	public float spawnTime;          // How long between each spawn.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
    public List<GameObject> enemyPool;
    public int poolSize;
    float spawnPeriod;
	bool p;
    void Start()
    {
		spawnTime = Time.realtimeSinceStartup;
        spawnPeriod = 1;
        poolSize = 5;
        Init();
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
		p = GameObject.FindGameObjectWithTag("Sun").GetComponent<DayNight>().paused;
		InvokeRepeating("Spawn", 0f, spawnPeriod);
    }


    void Spawn()
	{
		p = GameObject.FindGameObjectWithTag("Sun").GetComponent<DayNight>().paused;
		if (p) {
			spawnTime += Time.deltaTime;
		} else if (!p) {
//			Debug.Log (spawnTime - Time.realtimeSinceStartup);
			if (Mathf.Abs(spawnTime - Time.realtimeSinceStartup) > spawnPeriod) {
//				Debug.Log ("Spawning");
				for (int i = 0; i < enemyPool.Count; i++) {
					if (!enemyPool [i].activeInHierarchy) {
						enemyPool [i].transform.position = transform.position;
						enemyPool [i].transform.rotation = transform.rotation;
						//reset gameobject
						Enemy em = enemyPool [i].GetComponent<Enemy> ();
						//	em.Respawn();
						enemyPool [i].SetActive (true);
						em.Health = 100;
						spawnTime = Time.realtimeSinceStartup;
						break;
					}
				}
			}
		}  
        	// Find a random index between zero and one less than the number of spawn points.
   		  //   int spawnPointIndex = Random.Range(0, spawnPoints.Length);
   		  //   int spawnPointIndex = Random.Range(0, spawnPoints.Length);
   		
   		     // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
   		 //    Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
   		 //   enemyPool.Add(Instantiate(Resources.Load<GameObject>("Cylinder"), spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation));
   		  //   if (enemyPool.Count > 5)
   		  //   {
   		     //    CancelInvoke("Spawn");
   		
   		  //   }
    }

    private void Init()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        for (int i = 0; i < poolSize; i++)
        {
            GameObject tempEnemy = Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            tempEnemy.SetActive(false);
            enemyPool.Add(tempEnemy);
        }
    }
}