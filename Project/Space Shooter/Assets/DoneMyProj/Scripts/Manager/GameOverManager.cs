using UnityEngine;
using System.Collections;

public class GameOverManager : MonoBehaviour
{

    public bool isRestart;
    public bool isGameOver;

    private void Start()
    {
        Initialize();
    }

    public void GameOver()
    {
        DisplayTextManager.Instance.UpdText(textFields.gameover, DisplayTextManager.Instance._gameOverText);
        isGameOver = true;
    }

    public void Restart()
    {
        DisplayTextManager.Instance.UpdText(textFields.restart, DisplayTextManager.Instance._restartText);
        isRestart = true;
    }

    private void Initialize()
    {
        isRestart = false;
        isGameOver = false;
    }
}
