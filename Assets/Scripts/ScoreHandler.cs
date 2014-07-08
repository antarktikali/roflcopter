using UnityEngine;
using System.Collections;

public class ScoreHandler : MonoBehaviour {
	public GUIText scoreGuiText;
	private int score;

	void Start()
	{
		PlayerPrefs.DeleteKey("lastScore");
	}

	void Update()
	{
		score = (int)(Time.timeSinceLevelLoad * 10f);
		scoreGuiText.text = score.ToString();
	}

	public int getScore()
	{
		return score;
	}
}
