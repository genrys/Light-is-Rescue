using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
    private string boundaryNameOfGameObject;
    private string playerNameOfGameObject;

    public GameObject asteroidExplosion;
	public GameObject playerExplosion;
    public GameObject gameOverManager;


    public int scoreValue;

	private void Start()
	{
        Initialize();
	}

	private void OnTriggerEnter(Collider other) 
	{
		if (other.tag == boundaryNameOfGameObject)
			return;
		
		Instantiate(asteroidExplosion, transform.position, transform.rotation);

		if (other.tag == playerNameOfGameObject)
		{
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameOverManager.GetComponent<GameOverManager>().GameOver();
		}

		Destroy(other.gameObject);
		DisplayTextManager.Instance.AddingScore (scoreValue);
		Destroy(gameObject);
	}

    private void Initialize()
    {
        playerNameOfGameObject = "Player";
        boundaryNameOfGameObject = "Boundary";
        gameOverManager = GameObject.Find("GameOverManager");
    }
}
