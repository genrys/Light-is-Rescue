using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayTextManager : MonoBehaviour
{
    private int points;

    public Text countText;
    public Text winText;

    public string _winText;
    public string _countText;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        winText.text = "";
        _countText = "Count: ";
        _winText = "You WIN!";
    }
    /// <summary>
    /// Set and display text (score and etc.) in the game window
    /// </summary>
    /// <param name="addCount"></param>
    public void SetCount(int addCount)
    {
        points += addCount;
        countText.text = _countText + points;

        if (points >= 20)
        {
            winText.text = _winText;
        }
    }
}
