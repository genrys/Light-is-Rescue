using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
	private Animator animationGameOver;
	private AudioSource audioSource;

    private string gameOverTrigger;

    public AudioClip gameOverAudio;

	private void Awake ()
    {
        Initialize();
	}

	private void Update ()
    {
        IsPlayerDead();
	}

    private void IsPlayerDead()
    {
        if (PlayerHealth.isDead)
        {
            audioSource.PlayOneShot(gameOverAudio);
            StartCoroutine(playGameOverAnim());
            StartCoroutine(restartScene());
        }
    }

	IEnumerator playGameOverAnim()
    {
		yield return new WaitForSeconds (1f);
			animationGameOver.SetTrigger (gameOverTrigger);	
	}

	IEnumerator restartScene()
    {
		yield return new WaitForSeconds (5f);
        SceneManager.LoadScene(0);
		PlayerHealth.isDead = false;
		PlayerHealth.enemyIdle = false;
		Flashlight.timerFlashLight = 10f;
	}
    private void Initialize()
    {
        animationGameOver = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        gameOverTrigger = "GameOver";
    }
}
