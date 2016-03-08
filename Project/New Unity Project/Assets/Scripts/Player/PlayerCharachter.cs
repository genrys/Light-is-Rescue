using UnityEngine;
using System.Collections;

public class PlayerCharachter : MonoBehaviour {

	Animator playerAnim;
	float speed = 6f;
	Vector3 move;
	Rigidbody playerRB;
	GameObject safeZone;
	int floorMask;
	float camRayLength = 100f;
	public static bool isSafeZone = false;

	void Awake(){
		
		playerAnim = GetComponent<Animator> ();
		floorMask = LayerMask.GetMask ("Floor");
		playerRB = GetComponent <Rigidbody> ();
		safeZone = GameObject.FindGameObjectWithTag("SafeZone");


	}

	void FixedUpdate(){


		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		Movement (h,v);
		Turning ();
		Animating (h,v);
	}

	void Movement(float h, float v){


		move.Set (h, 0, v);
		move = move.normalized * speed * Time.deltaTime;

		playerRB.MovePosition (transform.position + move);
	}

	void Turning(){
	
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit floorHit;

		if(Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)){

			Vector3 playerToMouse = floorHit.point - transform.position;

			playerToMouse.y= 0f;

			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);

			playerRB.MoveRotation (newRotation);
		}
			
	}

	void OnTriggerEnter(Collider other){
		
		if (other.gameObject == safeZone) {
			for(int i = 0; i < EnemySpawnPoints.Instance.enemysToCreate; i++)
				EnemySpawnPoints.Instance.enemy[i].GetComponent<NavMeshAgent> ().enabled = false;
					
			isSafeZone = true;
		}

	}

	void OnTriggerExit(Collider other){

		if (other.gameObject == safeZone) {

			for(int i = 0; i < EnemySpawnPoints.Instance.enemysToCreate; i++)
				EnemySpawnPoints.Instance.enemy[i].GetComponent<NavMeshAgent> ().enabled = true;
			
			isSafeZone = false;
		}

	}

	void Animating (float h, float v)
	{
		
		bool run = h != 0f || v != 0f;


		playerAnim.SetBool ("Run", run);
	}
//exit
}