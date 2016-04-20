using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public static EnemyMovement Instance { get; private set; }

	private Transform player;
    private NavMeshAgent enemyNavigationAgent;
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;

    private string playerTag;

    private void Awake()
    {
        Initialize();	
	}

	private void Update()
    {
        NavigationEnemy();
	}

    private void NavigationEnemy()
    {
        if (enemyHealth.curHealthEnemy > 0 && playerHealth.playerCurHealth > 0 && !PlayerCharachter.Instance.isSafeZone)
            enemyNavigationAgent.SetDestination(player.position);
        else
            enemyNavigationAgent.enabled = false;
    }

    /*
    public void SettingsForNavigationMeshAgent(bool isNavMeshActive, bool isKinematicActive)
    {
        for (int i = 0; i < EnemySpawnPoints.Instance.enemysToCreate; i++)
        {
            if (EnemySpawnPoints.Instance.enemy[i].GetComponent<NavMeshAgent>().enabled == isNavMeshActive)
                EnemySpawnPoints.Instance.enemy[i].GetComponent<NavMeshAgent>().enabled = !isNavMeshActive;
            else
                EnemySpawnPoints.Instance.enemy[i].GetComponent<NavMeshAgent>().enabled = isNavMeshActive;

            if (EnemySpawnPoints.Instance.enemy[i].GetComponent<Rigidbody>().isKinematic == isNavMeshActive)
                EnemySpawnPoints.Instance.enemy[i].GetComponent<Rigidbody>().isKinematic = !isKinematicActive;
            else
                EnemySpawnPoints.Instance.enemy[i].GetComponent<Rigidbody>().isKinematic = isKinematicActive;
        }
    }*/

    public void NavigationActivating(bool isActivate)
    {
        for (int i = 0; i < EnemySpawnPoints.Instance.enemysToCreate; i++)
            EnemySpawnPoints.Instance.enemy[i].GetComponent<NavMeshAgent>().enabled = isActivate;
    }

    private void Initialize()
    {
        playerTag = "Player";
        player = GameObject.FindGameObjectWithTag(playerTag).transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        enemyNavigationAgent = GetComponent<NavMeshAgent>();
        Instance = this;
    }
}
