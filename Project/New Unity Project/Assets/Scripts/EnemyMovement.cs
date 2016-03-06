using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	Transform player;
	NavMeshAgent nav;
	PlayerHealth playerHealth;
	EnemyHealth enemyHealth;


	void Awake(){
	
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent<PlayerHealth> ();
		enemyHealth = GetComponent<EnemyHealth> ();
		nav = GetComponent <NavMeshAgent> ();
	}

	void Update(){

		if (enemyHealth.curHealth > 0 && playerHealth.curHealth > 0 && !PlayerCharachter.isSafeZone) {
			nav.SetDestination (player.position);
		}
		else {
			nav.enabled = false;

		}
	}
}
