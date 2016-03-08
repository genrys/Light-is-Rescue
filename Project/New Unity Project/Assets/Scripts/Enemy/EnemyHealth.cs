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
	public Light enemyLighting;
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

			enemyLighting.enabled = false;		
			colorEnemy = gameObject.GetComponent<Renderer> ().material.color;

			if (colorEnemy.a > 0) {
				colorEnemy.a -= 0.05f;
				gameObject.GetComponent<Renderer> ().material.color = colorEnemy;
			}
			if(isDead && colorEnemy.a <= 0)
				Deactivate ();
		} else if(!PlayerCharachter.isSafeZone){
			gameObject.GetComponent<Renderer> ().material.color = colorBeforeСrossSZ;
			enemyLighting.enabled = true;	
		}
	}


	public void Activate(){

		EnemySpawnPoints.Instance.spawnPointIndex = Random.Range (0, EnemySpawnPoints.Instance.spawnPoints.Length);
		transform.position = EnemySpawnPoints.Instance.spawnPoints [EnemySpawnPoints.Instance.spawnPointIndex].position;
		Awaking ();
	}

	public void Deactivate(){

		this.gameObject.SetActive (false);

	}


	public void TakeDamage (int amount, Vector3 hitPoint)
	{

		if(attack && curHealth > 0)
			enemyGetDamage.Play ();

		curHealth -= amount;
		if(curHealth > 0)
			Instantiate (hitParticle, transform.position, transform.rotation);

		if(curHealth <= 0)
		{
			Death ();
		}
	}

	void Death ()
	{
		if (!isDead) {
			ScoreManager.Instance.score += scoreValue;
		}
		
		isDead = true;
		capsuleCollider.isTrigger = true;
		anim.SetTrigger ("Death");

	}

	void Awaking(){
		
		curHealth = 100;
		isDead = false;
		isTransparent = false;
		gameObject.GetComponent<Renderer> ().material.color = colorBeforeСrossSZ;
		GetComponent <NavMeshAgent> ().enabled = true;
		GetComponent <Rigidbody> ().isKinematic = false;
		capsuleCollider.isTrigger = false;

	}

	public void StartTransparent ()
	{

		GetComponent <NavMeshAgent> ().enabled = false;
		GetComponent <Rigidbody> ().isKinematic = true;
		isTransparent = true;

		//Destroy (gameObject, 2f);
		//if(gameObject == null)
	}
}
