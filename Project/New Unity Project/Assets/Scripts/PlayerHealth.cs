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

	PlayerCharachter playerCharachter;
	bool isDead;
	bool damaged;


	void Awake(){
	
		playerCharachter = GetComponent<PlayerCharachter> ();

		curHealth = startHealth;

	}

	void Update(){

		if (damaged) {
			
			damageImage.color = flashColor;

		}
		else {

			flashColor = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;
	}

	public void TakeDamage (int amount)
	{

		damaged = true;

		curHealth -= amount;

		healthSlider.value = curHealth;

		if (curHealth <= 0 && !isDead) {

			Death ();

		}

	}

	void Death(){

		isDead = true;

		playerCharachter.enabled = false;

	}
}


