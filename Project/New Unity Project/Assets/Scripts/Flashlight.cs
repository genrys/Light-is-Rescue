using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Flashlight : MonoBehaviour
{
    private Ray shootRay;
    private RaycastHit shootHit;
    private LineRenderer gunLine;
    private AudioSource flashLightAudio;

    private bool turn = false;
    private int shootableMask;
    private string shootLayerTag;

    public Light flashlight;
	public Light fpFlashlight;
	public AudioClip flashTurn;
	public Slider flashLightPower;

    public static int numOfBatterys;
    public static float timerFlashLight;
	public int damagePerShot;                  
	public float range;
    

    private void Awake()
    {
        Initialize();
	}

    private void Update()
    {
        FlashLightAction();
	}

    private void FlashLightAction()
    {
        if (Input.GetKeyDown(KeyCode.F) && !turn && timerFlashLight != 0)
        {
            if (!PlayerCharachter.Instance.firstPersonCamera)
                FlashLightTurner(true, false);
            else
                FlashLightTurner(false, true);

            flashLightAudio.PlayOneShot(flashTurn);
            turn = true;
        }
        else if (Input.GetKeyDown(KeyCode.F) && turn || timerFlashLight <= 0)
        {
            FlashLightTurner(false, false);
            turn = false;
        }

        if (turn)
        {
            timerFlashLight -= Time.deltaTime;
            flashLightPower.value = timerFlashLight;
        }

        if (numOfBatterys != 0 && timerFlashLight <= 0)
        {
            timerFlashLight = 10f;
            flashLightPower.value = timerFlashLight;
            numOfBatterys--;
        }

        if (flashlight.enabled)
            Shoot();
        else
            gunLine.enabled = false;
    }

    private void FlashLightTurner(bool firstFL, bool secondFL)
    {
        flashlight.enabled = firstFL;
        fpFlashlight.enabled = secondFL;
    }

    private void Shoot ()
    {
		gunLine.enabled = true;
		gunLine.SetPosition (0, transform.position);
		shootRay.origin = transform.position;
		shootRay.direction = transform.forward;

		if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
		{
			EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
			if(enemyHealth != null && !PlayerCharachter.Instance.isSafeZone)
			{
				enemyHealth.TakeDamageByPlayer (damagePerShot);
			}
			gunLine.SetPosition (1, shootHit.point);
		}
		else
		{
			gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
		}
	}

    private void Initialize()
    {
        shootLayerTag = "Shootable";
        shootableMask = LayerMask.GetMask(shootLayerTag);
        flashLightAudio = GetComponent<AudioSource>();
        gunLine = GetComponent<LineRenderer>();
        timerFlashLight = 10f;
        damagePerShot = 10;
        range = 5f;
        numOfBatterys = 0;
    }
}
