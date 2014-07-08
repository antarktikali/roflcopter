using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public GameObject musicPrefab;

	void Start()
	{
		if (null == GameObject.FindGameObjectWithTag("Music")) {
			GameObject music = (GameObject)GameObject.Instantiate(musicPrefab);
			DontDestroyOnLoad(music);
		}
	}

	[Signal]
	void StartGame()
	{
		Application.LoadLevel(1);
	}

	[Signal]
	void SwitchMusic()
	{
		GameObject music = GameObject.FindGameObjectWithTag("Music");
		music.GetComponent<AudioSource>().mute = !music.GetComponent<AudioSource>().mute;
	}
}
