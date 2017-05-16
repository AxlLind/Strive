using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawnerScript : MonoBehaviour {

	float yPosAtLastSpawn;
	float distanceBetween = 3.2f;
	float lastXPos = 0;
    int flipCounter = 0;
    bool didWeFlip = false;

    public GameObject[] platformPrefabs;
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
        return PlatformScheme(percentNormal, percentTrampoline, percentMoving, 0);
    }

    /// <summary>
    /// With flipplatform
    /// </summary>
    /// <param name="percentNormal"></param>
    /// <param name="percentTrampoline"></param>
    /// <param name="percentMoving"></param>
    /// <param name="percentFlip"></param>
    /// <returns></returns>
    GameObject PlatformScheme(int percentNormal, int percentTrampoline, int percentMoving, int percentFlip)
    {
        if (didWeFlip)
        {
            flipCounter++;
            if (flipCounter % 6 == 0)
            {
                didWeFlip = false;
                return platformPrefabs[3];
            }
        }

        int randomInt = Random.Range(0, 100);
        if (randomInt < percentNormal)
        {
            return platformPrefabs[0];
        }
        else if (randomInt < percentNormal + percentMoving)
        {
            return platformPrefabs[1];
        }
        else if (randomInt < percentNormal + percentMoving + percentTrampoline)
        {
            return platformPrefabs[2];
        }
        else if (randomInt < percentNormal + percentMoving + percentTrampoline + percentFlip)
        {
            if (didWeFlip)
            {
                didWeFlip = false;
                flipCounter = 0;
            }
            else
            {
                didWeFlip = true;
                flipCounter++;
            }
            
            return platformPrefabs[3];
        }
        return platformPrefabs[0];
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
            return PlatformScheme(90, 0, 0, 10);
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
            return PlatformScheme(60, 20, 10, 10); // Flips
        }
        else if (this.transform.position.y < 5000)
        {
            return PlatformScheme(5, 5, 70, 20); // More hard stuff
        }
        else
        {
            return PlatformScheme(0, 0, 0, 100); // Fliptastic
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
