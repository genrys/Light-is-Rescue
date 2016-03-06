using UnityEngine;
using System.Collections;

public class SafeZone : MonoBehaviour {

	GameObject playerSafeZone;
	EnemyMovement enemyMovement;
	void Awake() {
	
		playerSafeZone = GameObject.FindGameObjectWithTag("Player");
		enemyMovement = GetComponent<EnemyMovement> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other){

		if (other.gameObject == playerSafeZone) {
			Debug.Log ("IN SZ");
			enemyMovement.GetComponent <NavMeshAgent> ().enabled = false;

		}

	}
}
