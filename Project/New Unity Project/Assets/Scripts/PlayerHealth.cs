using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public int curHealth;
	public int startHealth = 100;
	public Slider healthSlider; 
	public Image damageImage;
	public float flashSpeed = 5f;   
	public Color flashColor = new Color(1f, 0f, 0f, 0.1f);  

	Animator anim;
	PlayerCharachter playerCharachter;
	bool isDead;
	bool damaged;


	void Awake(){
	
		playerCharachter = GetComponent<PlayerCharachter> ();
		anim = GetComponent <Animator> ();
		curHealth = startHealth;

	}

	void Update(){
		
		if (damaged) {
			
			damageImage.color = flashColor;

		}
		else {

			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;
	}

	public void TakeDamage (int amount)
	{
		Debug.Log ("IN");
		damaged = true;

		curHealth -= amount;
		Debug.Log ("" + curHealth);
		healthSlider.value = curHealth;

		if (curHealth <= 0 && !isDead) {

			Death ();

		}

	}

	void Death(){

		isDead = true;

		anim.SetTrigger ("Die");

		playerCharachter.enabled = false;

	}
}


