using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Flashlight : MonoBehaviour {

	public Light flashlight;
	public AudioClip flash_turn;
	public Slider flashLightPower;

	public float timer = 10f;

	bool turn = false;
	AudioSource flashLightAudio;

	void Awake(){
		
		flashLightAudio = GetComponent <AudioSource> ();

	}

	void Update(){
		
		if(turn)	
			timer -= Time.deltaTime;

		flashLightPower.value = timer;

		if (Input.GetKeyDown (KeyCode.F) && !turn) {




			flashlight.enabled = true;
			flashLightAudio.clip = flash_turn;
			flashLightAudio.Play ();
			turn = true;

		}
		else if(Input.GetKeyDown(KeyCode.F) && turn || timer <= 0){
			
			flashlight.enabled = false;
			flashLightAudio.clip = flash_turn;
			flashLightAudio.Play ();
			turn = false;

		}
	}
}
