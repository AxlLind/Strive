using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawnerScript : MonoBehaviour {

	float spawnDelay = 1f;
	public GameObject platform;

	// Use this for initialization
	void Start () {
		StartCoroutine( spawnPlatform ());
	}

	IEnumerator spawnPlatform() {
		while (true) {
			Vector2 cameraPos = Camera.main.transform.position;
			Vector2 pos = new Vector2 (cameraPos.x + Random.Range(-8.5f, 8.5f), cameraPos.y + 8);
			Instantiate (platform, pos, Quaternion.identity);
			yield return new WaitForSeconds (spawnDelay);
		}
	}
}
