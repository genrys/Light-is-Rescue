using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    private Animator enemyAnimation;
    private GameObject player;
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;

    private string playerTag;
    private string enemyAttackTrigger;

    private bool playerInRange;
    private float timer;

    public float timeBetweenAttacks = 0.5f;
	public int damageByAttack = 10;

    private void Awake()
    {
        Initialize();
	}

    private void Update()
    {
        Action();
    }

    private void Action()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.curHealthEnemy > 0)
            Attack();

        if (playerHealth.playerCurHealth <= 0)
        {
            playerHealth.Death();
            //EnemyMovement.Instance.SettingsForNavigationMeshAgent(false, true);
             GetComponent<NavMeshAgent>().enabled = false;
             GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        stateAttack(other.gameObject, true);
	}

    private void OnTriggerExit(Collider other)
    {
        stateAttack(other.gameObject, false);
	}

	private void Attack()
    {
		timer = 0f;
		if (playerHealth.playerCurHealth > 0)
			playerHealth.TakeDamageByEnemy (damageByAttack);
	}

    private void stateAttack(GameObject other,bool isUnderAttack)
    {
        if (other.gameObject == player)
        {
            playerInRange = isUnderAttack;
            enemyAnimation.SetBool(enemyAttackTrigger, isUnderAttack);
        }
    }

    private void Initialize()
    {
        playerTag = "Player";
        player = GameObject.FindGameObjectWithTag(playerTag);
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        enemyAnimation = GetComponent<Animator>();
        timeBetweenAttacks = 0.5f;
        damageByAttack = 10;
        enemyAttackTrigger = "Attack";
    }
}
