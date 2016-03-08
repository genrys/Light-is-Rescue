using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Flashlight : MonoBehaviour {

	public Light flashlight;
	public AudioClip flashTurn;
	public Slider flashLightPower;
	public static float timerFlashLight = 10f;
	bool turn = false;
	AudioSource flashLightAudio;

	//
	public int damagePerShot = 1;                  
	public float range = 5f;                      
	Ray shootRay;                                  
	RaycastHit shootHit;  
	LineRenderer gunLine;
	int shootableMask;                              


	void Awake(){
		shootableMask = LayerMask.GetMask ("Shootable");
		flashLightAudio = GetComponent <AudioSource> ();
		gunLine = GetComponent <LineRenderer> ();
	}

	void Update(){
		
		if (turn) {
			
			timerFlashLight -= Time.deltaTime;
			flashLightPower.value = timerFlashLight;

		}


		if (Input.GetKeyDown (KeyCode.F) && !turn && timerFlashLight !=0) {

			flashlight.enabled = true;
			flashLightAudio.clip = flashTurn;
			flashLightAudio.Play ();
			turn = true;

		}
		else if(Input.GetKeyDown(KeyCode.F) && turn || timerFlashLight <= 0){
			
			flashlight.enabled = false;
			flashLightAudio.clip = flashTurn;
			flashLightAudio.Play ();
			turn = false;

		}

		if (BatterySpawn.Instance.num != 0 && timerFlashLight <= 0) {
			
			timerFlashLight = 10f;
			flashLightPower.value = timerFlashLight;
			BatterySpawn.Instance.num--;
		}

		if (flashlight.enabled) {
			Shoot ();
		} else {
			gunLine.enabled = false;
		}

	}

	void Shoot (){

		gunLine.enabled = true;
		gunLine.SetPosition (0, transform.position);

		shootRay.origin = transform.position;
		shootRay.direction = transform.forward;

		if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
		{
			EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
			if(enemyHealth != null)
			{

				enemyHealth.TakeDamage (damagePerShot, shootHit.point);
			}
			gunLine.SetPosition (1, shootHit.point);
		}
		else
		{
			gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
		}


	}





}
