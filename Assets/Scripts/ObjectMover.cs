using UnityEngine;
using System.Collections;

public class ObjectMover : MonoBehaviour {
	public float speed;

	void Update()
	{
		gameObject.transform.Translate(speed * Time.deltaTime, 0f, 0f);
	}
}
