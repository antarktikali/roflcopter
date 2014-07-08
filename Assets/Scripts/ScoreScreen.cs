using UnityEngine;
using System.Collections;

public class ScoreScreen : MonoBehaviour {
	public GUIText scoreText;
	public GUIText highScoreText;

	public GameObject highScoreTextObject;
	public GameObject highScoreNumberObject;
	public GameObject highScoreNoticeObject;
	
	void Start()
	{
		int lastScore = PlayerPrefs.GetInt("lastScore", 0);
		int highScore = PlayerPrefs.GetInt("highScore", 0);

		if (lastScore > highScore) {
			highScoreTextObject.SetActive(false);
			highScoreNumberObject.SetActive(false);
			highScoreNoticeObject.SetActive(true);
			scoreText.text = lastScore.ToString();

			PlayerPrefs.SetInt("highScore", lastScore);
		} else {
			scoreText.text = lastScore.ToString();
			highScoreText.text = highScore.ToString();
		}
	}

	[Signal]
	void ReturnToMenu()
	{
		Application.LoadLevel(0);
	}

	[Signal]
	void PlayAgain()
	{
		Application.LoadLevel(1);
	}
}
