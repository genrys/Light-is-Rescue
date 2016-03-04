using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public int curHealth;
	public int startHealth = 100;  
	public int scoreValue = 10;

	Animator anim;
	CapsuleCollider capsuleCollider;
	public Transform hitParticle;
	Color _color;
	bool isDead; 
	bool isTransparent;

	void Awake(){

		anim = GetComponent<Animator> ();
		capsuleCollider = GetComponent<CapsuleCollider> ();
		curHealth = startHealth;
	
	}

	void Update(){

		/*if (isTransparent) {

			_color.a = gameObject.GetComponent<Renderer>().material.color;
			if(_color.a > 0){
				_color.a -= 2/100;
				gameObject.GetComponent<Renderer>().material.color = _color;
			}
		}*/
	}

	public void TakeDamage (int amount, Vector3 hitPoint)
	{

		if(isDead)
			return;


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
		anim.SetTrigger ("Dead");

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
