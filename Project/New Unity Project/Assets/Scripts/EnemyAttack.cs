using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

	public float timeBetweenAttacks = 0.5f;
	public int attackDamage = 10;

	Color colorPlayer;
	Animator anim;
	GameObject player;
	PlayerHealth playerHealth;
	EnemyHealth enemyHealth;
	bool playerInRange;

	float timer;


	void Awake(){

		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();
		enemyHealth = GetComponent <EnemyHealth> ();
		anim = GetComponent<Animator> ();


	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject == player) {
			
			playerInRange = true;
			anim.SetBool ("Attack", true);

		}

	}

	void OnTriggerExit(Collider other){
		

		if (other.gameObject == player) {

			playerInRange = false;
			anim.SetBool ("Attack", false);
		}

	}

	void Update(){

		timer += Time.deltaTime;

		if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.curHealth > 0 ) {
			
			Attack ();

		}
			

		if (playerHealth.curHealth <= 0) {

			anim.SetTrigger ("PlayerDead");
			/*
			colorPlayer = player.GetComponent<Renderer> ().material.color;

			if (colorPlayer.a > 0) {

				colorPlayer.a -= 0.05f;
				Debug.Log ("colorEnemy.a = " + colorPlayer.a);
				player.GetComponent<Renderer> ().material.color = colorPlayer;
			} else {
				GetComponent <Rigidbody> ().isKinematic = true;
				Destroy (player);
			}*/
		}

	}


	void Attack(){

		timer = 0f;
		if (playerHealth.curHealth > 0) {
		
			playerHealth.TakeDamage (attackDamage);


		}

	}
//exit
}
