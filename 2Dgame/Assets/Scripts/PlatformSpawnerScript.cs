using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawnerScript : MonoBehaviour {

	float yPosAtLastSpawn;
	float distanceBetween = 3.2f;
	float lastXPos = 0;

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

	/**
	 * Spawns a random platform type at a random x-position.
	 */
	void spawnPlatform() {
		GameObject platformType = choosePlatform ();
		Vector2 pos = new Vector2(correctX(), transform.position.y);
		GameObject go = Instantiate (platformType, pos, Quaternion.identity);
		go.transform.parent = platformParent;
	}

	/**
	 * Returns a random platform prefab type. Currently:
	 * 		80% chance normal
	 * 		10% chance trampoline
	 * 		10% chance moving
	 */ 
	GameObject basicPlatformScheme() {
		int randomInt = Random.Range(0, 10);
		if (randomInt == 9)
		{
			return trampolinePlatform;
		}
		else if (randomInt == 8)
		{
			return movingPlatform;
		}
		return normalPlatform;
	}

    /**
     * Method for choosing platformscheme. At the moment holds basicPLatformScheme
     * and all moving.
     * Returns a platform prefab
     * 
     * Should be expanded upon.
     */
    GameObject choosePlatform()
    {
        if (this.transform.position.y > 1800 && this.transform.position.y < 2000)
        {
            return movingPlatform;
        }
        else
        {
            return basicPlatformScheme();
        }
    }

	/**
	 * Returns a random x-position to spawn a platform at.
	 * Makes two platforms in the same position twice in a row
	 * in a row less likely, though not impossible
	 */
	int correctX() {
		int x = 2 * Random.Range (-3, 4);
		if (x == lastXPos) {
			x = 2 * Random.Range (-3, 4);
		}
		lastXPos = x;
		return x;
	}
}
