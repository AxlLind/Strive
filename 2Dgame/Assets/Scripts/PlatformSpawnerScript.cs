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
        return PlatformScheme(80, 10, 10);
	}

    /// <summary>
    /// Modifiable PlatformScheme
    /// Useful when scaling difficulty
    /// </summary>
    /// <param name="percentNormal"></param>
    /// <param name="percentTrampoline"></param>
    /// <param name="percentMoving"></param>
    /// <returns>Returns a platformsprefab</returns>
    GameObject PlatformScheme(int percentNormal, int percentTrampoline, int percentMoving)
    {
        int randomInt = Random.Range(0, 100);
        if (randomInt < percentTrampoline)
        {
            return trampolinePlatform;
        }
        else if (randomInt < percentMoving + percentTrampoline)
        {
            return movingPlatform;
        }
        return normalPlatform;
    }

    /**
     * Method for choosing platformscheme.
     * Is the game's primary progressionsystem.
     * Difficulty increases as score increases.
     */
    GameObject choosePlatform()
    {
        if (this.transform.position.y < 500)
        {
            return basicPlatformScheme();
        }
        else if (this.transform.position.y < 800)
        {
            return PlatformScheme(45, 10, 45);
        }
        else if (this.transform.position.y < 1000)
        {
            return PlatformScheme(0, 0, 100); // First all moving
        }
        else if (this.transform.position.y < 1200)
        {
            return PlatformScheme(60, 20, 20); // Bit of a breather
        }
        else if (this.transform.position.y < 5000)
        {
            return PlatformScheme(5, 5, 90); // More hard stuff
        }
        else
        {
            return PlatformScheme(5, 70, 25); // Ya did it
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
