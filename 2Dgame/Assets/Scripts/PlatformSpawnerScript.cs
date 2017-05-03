using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawnerScript : MonoBehaviour {

	float yPosAtLastSpawn;
	float distanceBetween = 3.2f;
	bool lastWasHard = false;

	public GameObject normalPlatform;
    public GameObject movingPlatform;
    public GameObject trampolinePlatform;
	public Transform platformParent;

	void Update() {
		if (GameControllerScript.isPaused) {
			return;
		}

		float y = Camera.main.transform.position.y;
		if (y - yPosAtLastSpawn > distanceBetween) {
			spawnPlatform ();
			yPosAtLastSpawn = y;
		}
	}

	void spawnPlatform() {
		Vector2 pos = new Vector2(correctX(), transform.position.y);

        GameObject platformPrefab = normalPlatform;
        int randomInt = Random.Range(0, 10);
        if (randomInt <= 7)
        {
            platformPrefab = normalPlatform;
        }
        else if (randomInt == 8)
        {
            platformPrefab = movingPlatform;
        }
        else if (randomInt == 9)
        {
            platformPrefab = trampolinePlatform;
        }

		GameObject go = Instantiate (platformPrefab, pos, Quaternion.identity);
		go.transform.parent = platformParent;
	}

	int correctX() {
		// Makes sure that two platforms cant be at opposite edge in a row
		int x = 2 * Random.Range (-3, 4);
		if (x == -6 || x == 6) {
			if (lastWasHard) {
				x = 2 * Random.Range (-2, 3);
				lastWasHard = false;
			} else {
				lastWasHard = true;
			}
		}
		return x;
	}
}
