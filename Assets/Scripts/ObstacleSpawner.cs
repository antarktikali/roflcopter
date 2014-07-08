using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour {
	public GameObject obstacle;
	public float initialDelay;
	public float minHeight;
	public float maxHeight;
	public float minDelay;
	public float maxDelay;
	public int obstaclePoolSize;

	private Stack obstaclePool = new Stack();
	
	void Start()
	{
		for (int i = 0; i < obstaclePoolSize; i++) {
			obstaclePool.Push(GameObject.Instantiate(obstacle));
		}

		foreach (GameObject g in obstaclePool) {
			g.SetActive(false);
		}

		StartCoroutine(SpawnObstacles());
	}
	
	IEnumerator SpawnObstacles()
	{
		yield return new WaitForSeconds(initialDelay);
		while (true)
		{
			yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

			spawnObstacle(new Vector3(gameObject.transform.position.x,
			                          Random.Range(minHeight, maxHeight) * Mathf.Pow(-1f, Random.Range(1,2)),
			                          gameObject.transform.position.z));
		}
	}

	private void spawnObstacle(Vector3 spawnPosition)
	{
		if (1 > obstaclePool.Count) {
			Debug.LogError("Object pool is empty! Increase the pool size.");
		}

		GameObject newObstacle = (GameObject)obstaclePool.Pop();
		newObstacle.SetActive(true);
		newObstacle.transform.position = spawnPosition;
	}

	[Signal]
	void PushObstacleToPool(GameObject g)
	{
		g.SetActive(false);
		obstaclePool.Push(g);
	}
}
