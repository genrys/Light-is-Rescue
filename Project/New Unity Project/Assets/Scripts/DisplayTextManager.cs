using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayTextManager : MonoBehaviour {

    public static DisplayTextManager Instance { get; private set; }

    private string scoreText;
    private string bestScoreText;
    private string bestScoreKey;

    public Text _bestScore;
    public Text _score;
    public Text number;

    public int score;
    public int bestScore;
    

    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        TextOnDisplay();
    }

    private void TextOnDisplay()
    {
        number.text = "" + Flashlight.numOfBatterys;
        _score.text = scoreText + score;
        if (score > bestScore && PlayerHealth.isDead)
        {
            bestScore = score;
            PlayerPrefs.SetInt(bestScoreKey, bestScore);
        }
        _bestScore.text = bestScoreText + bestScore;
    }

    private void Initialize()
    {
        scoreText = "SCORE: ";
        bestScoreKey = "BEST SCORE";
        bestScoreText = "BEST SCORE: ";
        score = 0;
        bestScore = PlayerPrefs.GetInt(bestScoreKey);
        Instance = this;
    }
}
