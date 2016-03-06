using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public int curHealth;
	public int startHealth = 100;  
	public int scoreValue = 10;

	Animator anim;
	CapsuleCollider capsuleCollider;
	AudioSource enemyGetDamage;
	public Transform hitParticle;
	Color colorEnemy;
	Color colorBeforeСrossSZ;
	bool isDead; 
	bool isTransparent;
	bool attack = true;




	void Awake(){

		anim = GetComponent<Animator> ();
		enemyGetDamage = GetComponent<AudioSource> ();
		capsuleCollider = GetComponent<CapsuleCollider> ();
		curHealth = startHealth;
		colorBeforeСrossSZ = gameObject.GetComponent<Renderer> ().material.color;
	}

	void Update(){

		if (PlayerHealth.enemyIdle || PlayerCharachter.isSafeZone)
			anim.SetBool ("Idle", true);
		else
			anim.SetBool ("Idle", false);


		if (isTransparent || PlayerCharachter.isSafeZone) {
			
			colorEnemy = gameObject.GetComponent<Renderer> ().material.color;

			if (colorEnemy.a > 0) {
				
				colorEnemy.a -= 0.05f;
				gameObject.GetComponent<Renderer> ().material.color = colorEnemy;
			}
		} else
			gameObject.GetComponent<Renderer> ().material.color = colorBeforeСrossSZ;
	}

	public void TakeDamage (int amount, Vector3 hitPoint)
	{
		/*
		if (firstAttack) {
			enemyGetDamage.Play ();
			firstAttack = false;
		} else if (!firstAttack && firstPause) {
			enemyGetDamage.Pause();
			firstPause = false;
		}
		else {
			enemyGetDamage.UnPause();
			firstPause = true;
			firstAttack = false;
		}
		*/
		if(attack)
			enemyGetDamage.Play ();


		curHealth -= amount;
		Instantiate (hitParticle, transform.position, transform.rotation);

		if(curHealth <= 0)
		{
			Death ();
		}
	}

	void Death ()
	{

		isDead = true;
		capsuleCollider.isTrigger = true;
		anim.SetTrigger ("Death");

	}

	public void StartTransparent ()
	{

		GetComponent <NavMeshAgent> ().enabled = false;
		GetComponent <Rigidbody> ().isKinematic = true;

		isTransparent = true;
		ScoreManager.Instance.score += scoreValue;
		Destroy (gameObject, 2f);

	}
}
