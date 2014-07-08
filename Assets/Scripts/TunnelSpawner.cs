using UnityEngine;
using System.Collections;

public class TunnelSpawner : MonoBehaviour {
	public float spawnDelay;
	public int minSlopeDistance;
	public int maxSlopeDistance;
	public float minSlopeHeight;
	public float maxSlopeHeight;
	public float tunnelHeight;

	public int initialBlockCount;
	public float initialBlockMargin;

	public GameObject tunnelBlock;
	public int tunnelBlockPoolSize;
	private Stack tunnelBlockPool = new Stack();

	private int slopeDistance;
	private int verticalPosition;
	private float currentSlopeHeight;
	private float previousSlopeHeight;
	
	void Start()
	{
		for (int i = 0; i < tunnelBlockPoolSize; i++) {
			tunnelBlockPool.Push(GameObject.Instantiate(tunnelBlock));
		}

		foreach (GameObject g in tunnelBlockPool) {
			g.SetActive(false);
		}

		previousSlopeHeight = 0;
		slopeDistance = Random.Range(minSlopeDistance, maxSlopeDistance);
		currentSlopeHeight = Random.Range(minSlopeHeight, maxSlopeHeight);
		verticalPosition = 1;

		StartCoroutine(SpawnBlocks());
	}

	IEnumerator SpawnBlocks()
	{
		for (int k = initialBlockCount; k > 0; k--) {
			spawnTunnelBlock(new Vector3(gameObject.transform.position.x - (initialBlockMargin * k),
			                             tunnelHeight,
			                             gameObject.transform.position.z));
			spawnTunnelBlock(new Vector3(gameObject.transform.position.x - (initialBlockMargin * k),
			                             tunnelHeight * -1f,
			                             gameObject.transform.position.z));
		}

		while (true) {
			if(verticalPosition >= slopeDistance) {
				previousSlopeHeight = currentSlopeHeight;
				slopeDistance = Random.Range(minSlopeDistance, maxSlopeDistance);
				currentSlopeHeight = Random.Range(minSlopeHeight, maxSlopeHeight);
				verticalPosition = 1;
			}

			float localBlockPosition = ((currentSlopeHeight - previousSlopeHeight) / slopeDistance)
															* verticalPosition + previousSlopeHeight;

			spawnTunnelBlock(new Vector3(gameObject.transform.position.x,
			                             localBlockPosition + tunnelHeight,
			                             gameObject.transform.position.z));

			spawnTunnelBlock(new Vector3(gameObject.transform.position.x,
			                             localBlockPosition + ((tunnelHeight) * -1f),
			                             gameObject.transform.position.z));

			verticalPosition++;
			yield return new WaitForSeconds(spawnDelay);
		}
	}

	private void spawnTunnelBlock(Vector3 spawnPosition)
	{
		if (1 > tunnelBlockPool.Count) {
			Debug.LogError("Object pool is empty! Increase the pool size.");
		}

		GameObject newBlock = (GameObject)tunnelBlockPool.Pop();
		newBlock.SetActive(true);
		newBlock.transform.position = spawnPosition;
	}

	[Signal]
	void PushBlockToPool(GameObject g)
	{
		g.SetActive(false);
		tunnelBlockPool.Push(g);
	}
}
