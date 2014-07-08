using UnityEngine;
using System.Collections;

public class GUIButton : MonoBehaviour {
	public Signal onClick;

	void OnMouseDown()
	{
		onClick.Invoke();
	}
}
