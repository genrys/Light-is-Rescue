using UnityEngine;
using System.Collections;

public class PlayerCharachter : MonoBehaviour {

	float speed = 6f;
	Vector3 move;
	Rigidbody playerRB;
	int floorMask;
	float camRayLength = 100f;

	void Awake(){
		floorMask = LayerMask.GetMask ("Floor");

		playerRB = GetComponent <Rigidbody> ();

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

}
