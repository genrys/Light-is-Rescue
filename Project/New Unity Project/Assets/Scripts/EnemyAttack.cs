﻿using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

	public float timeBetweenAttacks = 0.5f;
	public int attackDamage = 10;

	Animator anim;
	GameObject player;
	PlayerHealth playerHealth;

	bool playerInRange;
	float timer;


	void Awake(){

		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <PlayerHealth> ();
		anim = GetComponent<Animator> ();

	}

	void OnTriggerEnter(Collider other){
		Debug.Log ("Stolknylic'");
		if (other.gameObject == player) {
			
			playerInRange = true;
			Debug.Log (playerInRange);
		}

	}

	void OnTriggerExit(Collider other){
		

		if (other.gameObject == player) {

			playerInRange = false;

		}

	}

	void Update(){

		timer += Time.deltaTime;
		Debug.Log (playerInRange);

		if (timer >= timeBetweenAttacks && playerInRange) {

			Attack ();

		}

		if (playerHealth.curHealth <= 0) {

			anim.SetTrigger ("PlayerDead");

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
