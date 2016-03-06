using UnityEngine;
using System.Collections;

public class PlayerCharachter : MonoBehaviour {

	float speed = 6f;
	Vector3 move;
	Rigidbody playerRB;
	GameObject safeZone;
	GameObject player;
	GameObject enemy;
	int floorMask;
	float camRayLength = 100f;
	public static bool isSafeZone = false;

	void Awake(){
		floorMask = LayerMask.GetMask ("Floor");
		playerRB = GetComponent <Rigidbody> ();
		safeZone = GameObject.FindGameObjectWithTag("SafeZone");
		player = GameObject.FindGameObjectWithTag("Player");
		enemy = GameObject.FindGameObjectWithTag("Enemy");

	}

	void FixedUpdate(){
		
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		Movement (h,v);
		Turning ();
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

			enemy.GetComponent<NavMeshAgent> ().enabled = false;
			isSafeZone = true;
		}

	}

	void OnTriggerExit(Collider other){

		if (other.gameObject == safeZone) {

			enemy.GetComponent<NavMeshAgent> ().enabled = true;
			isSafeZone = false;
		}

	}
}
