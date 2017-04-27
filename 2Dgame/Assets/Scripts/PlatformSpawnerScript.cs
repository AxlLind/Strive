using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawnerScript : MonoBehaviour {

	float yPosAtLastSpawn;
	float distanceBetween = 3f;
	public GameObject platformPrefab;
	public Transform platformParent;

	// Use this for initialization
	void Start () {
	}

	void Update() {
		float y = Camera.main.transform.position.y;
		if (y - yPosAtLastSpawn > distanceBetween) {
			spawnPlatform ();
			yPosAtLastSpawn = y;
		}
	}

	void spawnPlatform() {
		Vector2 pos = new Vector2(transform.position.x + Random.Range(-7f, 7f), transform.position.y);
		GameObject go = Instantiate (platformPrefab, pos, Quaternion.identity);
		go.transform.parent = platformParent;
	}
}
