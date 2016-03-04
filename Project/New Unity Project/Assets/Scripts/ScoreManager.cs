using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	public static ScoreManager Instance { get; private set; }

	Text score_text;
	public int score;

	void Awake(){

		score_text = GetComponent<Text> ();

		score = 0;
		Instance = this;
	}

	void Update(){

		score_text.text = "SCORE: " + score;

	}


}
