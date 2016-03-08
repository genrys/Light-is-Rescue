using UnityEngine;
using System.Collections;

public class Battery : MonoBehaviour {


	GameObject player;
	//Singleton pattern
	public static Battery Instance { get; private set; }



	void Awake(){
	

		player = GameObject.FindGameObjectWithTag ("Player");
		//Singleton pattern
		Instance = this;
	}

	void OnTriggerEnter(Collider other){

		if (other.gameObject == player) {
			BatterySpawn.Instance.num++;
		}
	}

	void OnTriggerExit(Collider other){

		if (other.gameObject == player) {
			Deactivating ();
		}
	}
		
	void Update(){


	}

	public void Activating(){

		BatterySpawn.Instance.batSpawnPointIndex = Random.Range (0, BatterySpawn.Instance.batterySpawnPoint.Length);
		transform.position = BatterySpawn.Instance.batterySpawnPoint [BatterySpawn.Instance.batSpawnPointIndex].position;

	}

	void Deactivating(){
	
		this.gameObject.SetActive (false);

	}



}
