using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverUIHandler : MonoBehaviour {
	public Text t_score;

	void Start()
	{
		t_score = GetComponent<Text>();
	}

	public void SetScore(int score)
	{
		t_score.text = "SCORE: " + score.ToString ();
	}
}
