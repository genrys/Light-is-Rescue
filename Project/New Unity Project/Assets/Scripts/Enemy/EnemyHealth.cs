using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
	private Animator enemyAnimation;
	private CapsuleCollider capsuleCollider;
	private AudioSource enemyGetDamage;
    private Color colorEnemy;
    private Color colorBeforeСrossSZ;
    private NavMeshAgent enemyNavigationAgent;
    private Rigidbody rigidBodyKinematicComponent;

    private bool isDeadEnemy;
    private bool isTransparentEnemy;
    private bool enemyUnderAttack;
    private string deathEnemyTrigger;
    private string idleEnemyTrigger;

    public Transform hitParticle;
	public Light enemyLighting;

    public int curHealthEnemy;
    public int startHealthEnemy;
    public int scoreValue;

    private void Awake()
    {
        Initialize();
	}

    private void Update()
    {
        EnemyState();
	}

    private void EnemyState()
    {
        if (PlayerHealth.enemyIdle || PlayerCharachter.Instance.isSafeZone)
            enemyAnimation.SetBool(idleEnemyTrigger, true);
        else
            enemyAnimation.SetBool(idleEnemyTrigger, false);

        if (isTransparentEnemy || PlayerCharachter.Instance.isSafeZone)
        {
            enemyLighting.enabled = false;
            colorEnemy = gameObject.GetComponent<Renderer>().material.color;

            if (colorEnemy.a > 0)
            {
                colorEnemy.a -= 0.05f;
                gameObject.GetComponent<Renderer>().material.color = colorEnemy;
            }
            if (isDeadEnemy && colorEnemy.a <= 0)
                DeactivateEnemy();
        }
        else if (!PlayerCharachter.Instance.isSafeZone)
        {
            gameObject.GetComponent<Renderer>().material.color = colorBeforeСrossSZ;
            enemyLighting.enabled = true;
        }
    }

	public void ActivateEnemy()
    {
        EnemySpawnPoints.Instance.spawnPointIndex = Random.Range (0, EnemySpawnPoints.Instance.spawnPoints.Length);
		transform.position = EnemySpawnPoints.Instance.spawnPoints [EnemySpawnPoints.Instance.spawnPointIndex].position;
		Awaking ();
	}

	public void DeactivateEnemy()
    {
		gameObject.SetActive (false);
	}

	public void TakeDamageByPlayer (int amount)
	{
		if(enemyUnderAttack && curHealthEnemy > 0)
			enemyGetDamage.Play ();

		curHealthEnemy -= amount;

        if (curHealthEnemy > 0)
            Instantiate(hitParticle, transform.position, transform.rotation);
        else
            Death();
	}

    private void Death ()
	{
		if (!isDeadEnemy)
            DisplayTextManager.Instance.score += scoreValue;
		isDeadEnemy = true;
		capsuleCollider.isTrigger = true;
		enemyAnimation.SetTrigger (deathEnemyTrigger);
	}

    private void Awaking()
    {
		curHealthEnemy = 100;
		isDeadEnemy = false;
		isTransparentEnemy = false;
		gameObject.GetComponent<Renderer> ().material.color = colorBeforeСrossSZ;
        //EnemyMovement.Instance.SettingsForNavigationMeshAgent(true, false);
        enemyNavigationAgent.enabled = true;
        rigidBodyKinematicComponent.isKinematic = false;
        capsuleCollider.isTrigger = false;
	}

	public void StartTransparent ()
	{
        //EnemyMovement.Instance.SettingsForNavigationMeshAgent(false, true);
        enemyNavigationAgent.enabled = false;
        rigidBodyKinematicComponent.isKinematic = true;
		isTransparentEnemy = true;
	}

    public void Initialize()
    {
        enemyAnimation = GetComponent<Animator>();
        enemyGetDamage = GetComponent<AudioSource>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        enemyNavigationAgent = GetComponent<NavMeshAgent>();
        rigidBodyKinematicComponent = GetComponent<Rigidbody>();
        colorBeforeСrossSZ = gameObject.GetComponent<Renderer>().material.color; 
        startHealthEnemy = 100;
        curHealthEnemy = startHealthEnemy;
        deathEnemyTrigger = "Death";
        idleEnemyTrigger = "Idle";
        scoreValue = 10;
        enemyUnderAttack = true;
    } 
}
