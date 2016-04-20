using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance { get; private set; }
    private Color colorPlayer;
    private PlayerCharachter playerCharachter;

    private bool damaged;

    public Slider healthSlider;
    public Image damageImage;
    public Color flashColor;

    public int playerCurHealth;
	public int playerStartHealth;
    public bool plusMedChestHealth;
    public float flashSpeed;   

	public static bool enemyIdle;
	public static bool isDead;

	private void Awake()
    {
        Initialize();	
	}

    private void Update()
    {
        isDamage();
        HealthSetter();
    }

    private void isDamage()
    {
        if (damaged)
            damageImage.color = flashColor;
        else
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);

        damaged = false;
    }

	public void TakeDamageByEnemy (int healthByDamage)
	{
		damaged = true;
		playerCurHealth -= healthByDamage;
		healthSlider.value = playerCurHealth;

		if (playerCurHealth <= 0 && !isDead)
        {
			enemyIdle = true;
			Death ();
		}
	}

    private void HealthSetter()
    {
        if (plusMedChestHealth)
        {
            playerCurHealth = 100;
            healthSlider.value = playerCurHealth;
            plusMedChestHealth = false;
        }
    }

    public void Death()
    {
		isDead = true;
		playerCharachter.enabled = false;

		if(colorPlayer.a > 0)
        {
			colorPlayer.a -= 0.005f;
			GetComponent<Renderer> ().material.color = colorPlayer;
		} 
		else
			gameObject.SetActive (false);
	}

    private void Initialize()
    {
        playerCharachter = GetComponent<PlayerCharachter>();
        playerCurHealth = playerStartHealth;
        colorPlayer = GetComponent<Renderer>().material.color;
        flashColor = new Color(1f, 0f, 0f, 0.1f);
        playerStartHealth = 100;
        flashSpeed = 5f;
        plusMedChestHealth = false;
        Instance = this;
    }
}


