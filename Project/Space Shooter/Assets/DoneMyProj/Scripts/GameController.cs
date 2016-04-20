using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private GameObject gameOverManager;

	public GameObject hazard;
	public Vector3 spawnValues;

    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    private void Start ()
	{
        Initialize();
        StartCoroutine(SpawnWaves());
    }

    private void Update()
	{
        CheckRestart();
	}

    IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			if (gameOverManager.GetComponent<GameOverManager>().isGameOver) 
			{
                gameOverManager.GetComponent<GameOverManager>().Restart();
				break;
			}	
		}
	}

    private void CheckRestart()
    {
        if (gameOverManager.GetComponent<GameOverManager>().isRestart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    private void Initialize()
    {
        gameOverManager = GameObject.Find("GameOverManager");
    }
}