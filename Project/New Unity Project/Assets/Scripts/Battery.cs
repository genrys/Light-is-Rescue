using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Battery : MonoBehaviour {

	public Text number;
	public int num;
	GameObject player;
	//Singleton pattern
	public static Battery Instance { get; private set; }


	void Awake(){
	

		player = GameObject.FindGameObjectWithTag ("Player");

		num = 0;
		//Singleton pattern
		Instance = this;
	}

	void OnTriggerEnter(Collider other){
	
		if (other.gameObject == player) {
			
			num++;

		}
	}


	void Update(){
		
		number.text = "" + num;

	}
		
}
