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
        return PlatformScheme(60, 10, 10, 20);
	}

    /**
     * Platformscheme with a variable amount of parameters.
     * 
     * Returns a platform prefab.
     */
    GameObject PlatformScheme(params int[] percentages)
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
        int percentageCounter = 0;
        for (int i = 0; i < percentages.Length; i++)
        {
            percentageCounter += percentages[i];
            if (randomInt < percentageCounter)
            {
                if (i == 3)
                {
                    if (didWeFlip)
                    {
                        flipCounter = 0;
                    }
                    else
                    {
                        flipCounter++;
                    }
					didWeFlip = !didWeFlip;
                }

                return platformPrefabs[i];
            }
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
        if (this.transform.position.y < 200)
        {
            return basicPlatformScheme();
        }
        else if (this.transform.position.y < 500)
        {
            return PlatformScheme(45, 45, 10);
        }
        else if (this.transform.position.y < 700)
        {
            return PlatformScheme(0, 100); // First all moving
        }
        else if (this.transform.position.y < 1000)
        {
            return PlatformScheme(60, 20, 10, 10); // Flips
        }
        else if (this.transform.position.y < 1500)
        {
            return PlatformScheme(45, 40, 5, 10); // More hard stuff
        }
        else
        {
            return PlatformScheme(0, 85, 5, 10); // Fliptastic
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
