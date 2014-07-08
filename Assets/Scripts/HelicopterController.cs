using UnityEngine;
using System.Collections;

public class HelicopterController : MonoBehaviour {
	public float freezeTime;
	public float force;
	public GameObject roflcopterSprite;
	public ScoreHandler scoreHandler;
	public GameObject countdown;

	private float velocity;
	private bool isFrozen;

	void Start()
	{
		isFrozen = true;
		velocity = 0f;
		StartCoroutine(FreezeVertical());
	}

	void Update()
	{
		if (!isFrozen) {
			if (Input.GetMouseButton (0)) {
				velocity += force * Time.deltaTime;
			} else {
				velocity -= force * Time.deltaTime;
			}
		} else {
			countdown.GetComponent<GUIText>().text = ((int)(freezeTime - Time.timeSinceLevelLoad) + 1).ToString();
		}

		gameObject.transform.Translate(0f, velocity, 0f);
		roflcopterSprite.transform.rotation = Quaternion.Euler(0f, 0f, velocity * 50f);
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		PlayerPrefs.SetInt("lastScore", scoreHandler.getScore());
		Application.LoadLevel(2);
	}

	IEnumerator FreezeVertical()
	{
		yield return new WaitForSeconds(freezeTime);
		isFrozen = false;
		countdown.SetActive(false);
	}
}
