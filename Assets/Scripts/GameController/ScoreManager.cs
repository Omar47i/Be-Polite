using UnityEngine;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour {

    public int HP = 10;
    public int score;

	private bool bOneTime = false;

	public GameObject gameOverUI;
	public List<GameObject>Disable;

    public void IncHealth()
    {
        if (HP < 10)
        {
            HP++;
        }
        
        score += 102;
    }

    public void DecHealth()
    {
        HP -= 2;

        if (HP <= 0)
            GameOver();
    }

    public void GameOver()
    {
		if (!bOneTime) 
		{
			// on game over, display gameover panel and disable some scripts
			foreach (GameObject go in Disable)
			{
				go.SetActive (false);
			}

			bOneTime = true;
			gameOverUI.SetActive (true);
			gameOverUI.GetComponent<GameOverUIHandler>().SetScore(score);
		}
    }
}
