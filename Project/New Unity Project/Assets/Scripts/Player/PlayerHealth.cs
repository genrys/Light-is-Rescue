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
	Color colorPlayer;
	PlayerCharachter playerCharachter;
	bool isDead;
	bool damaged;
	public static bool enemyIdle;

	void Awake(){
	
		playerCharachter = GetComponent<PlayerCharachter> ();
		curHealth = startHealth;
		colorPlayer = GetComponent<Renderer> ().material.color;
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
		
		damaged = true;

		curHealth -= amount;
		healthSlider.value = curHealth;

		if (curHealth <= 0 && !isDead) {

			enemyIdle = true;
			Death ();

		}

	}

	public void Death(){

		isDead = true;
		playerCharachter.enabled = false;


		while(colorPlayer.a > 0) {

			colorPlayer.a -= 0.01f;
			GetComponent<Renderer> ().material.color = colorPlayer;

		} 


	}
}


