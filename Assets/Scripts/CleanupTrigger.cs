using UnityEngine;
using System.Collections;

public class CleanupTrigger : MonoBehaviour {
	public Signal cleanupTunnelBlock = new Signal(typeof(GameObject));
	public Signal cleanupObstacle = new Signal(typeof(GameObject));

	void OnTriggerEnter2D(Collider2D c)
	{
		if ("TunnelBlock" == c.gameObject.tag) {
			cleanupTunnelBlock.Invoke(c.gameObject);
		} else if ("Obstacle" == c.gameObject.tag) {
			cleanupObstacle.Invoke(c.gameObject);
		}
	}
}
