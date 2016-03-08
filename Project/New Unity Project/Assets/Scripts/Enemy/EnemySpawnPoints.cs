using UnityEngine;
using System.Collections;

public class EnemySpawnPoints : MonoBehaviour {

	public static EnemySpawnPoints Instance{ get; private set;}

	public PlayerHealth playerHealth;
	public GameObject[] enemy;
	public Transform[] spawnPoints;
	public int enemysToCreate;
	public int spawnPointIndex;


	void Start () {

		enemy = new GameObject[enemysToCreate];
		InstatntiateEnemys ();
		Instance = this;


	}

	void Update(){
	
		//ActivateEnemys ();

	}

	private void ActivateEnemys(){


		for (int i = 0; i < enemysToCreate; i++) {
			if (enemy [i].activeInHierarchy == false) {
				enemy [i].SetActive (true);
				enemy [i].GetComponent<EnemyHealth>().Activate ();
			}
		}

	}

	private void InstatntiateEnemys () {
		
		if(playerHealth.curHealth <= 0f){
			return;
		}
		
		for (int i = 0; i < enemysToCreate; i++) {
						
            spawnPointIndex = Random.Range (0, spawnPoints.Length);
			enemy[i] = (GameObject)Instantiate (Resources.Load("Prefabs/Enemy"), spawnPoints [spawnPointIndex].position, spawnPoints [spawnPointIndex].rotation);
			//enemy[i] = GameObject.FindGameObjectWithTag ("Enemy");
			enemy[i].SetActive (false);
			
		}
	} 

//exit
}
