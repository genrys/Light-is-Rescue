using UnityEngine;
using System.Collections;
using System;

public enum textFields
{
    score,
    restart,
    gameover
};

public class DisplayTextManager : MonoBehaviour
{
    public static DisplayTextManager Instance { get; private set; }

    private string _scoreText;
    private int scoreValue;

    public GUIText scoreText;
    public GUIText restartText;
    public GUIText gameOverText;

    public string _gameOverText;
    public string _restartText;
    
    private void Start()
    {
        Initialize();
        UpdText(textFields.score, _scoreText);
    }
 
    public void UpdText(textFields fieldOfEnum, string someText)
    {
        switch (fieldOfEnum)
        {
            case textFields.score:
                scoreText.text = someText + scoreValue;
                break;
            case textFields.restart:
                restartText.text = someText;
                break;
            case textFields.gameover:
                gameOverText.text = someText;
                break;
        }
    }

    public void AddingScore(int addScore)
    {
        scoreValue += addScore;
        UpdText(textFields.score, _scoreText);
    }

    private void Initialize()
    {
        Instance = this;
        _scoreText = "Score: ";
        scoreValue = 0;
        restartText.text = "";
        gameOverText.text = "";
        _gameOverText = "GAME OVER";
        _restartText = "Press 'R' to restart";
    }
}
