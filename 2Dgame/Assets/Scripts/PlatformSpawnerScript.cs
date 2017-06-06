using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawnerScript : MonoBehaviour {

	float yPosAtLastSpawn = 5f;
	int lastXPos = 0;
    Queue prefabqueue;

    public float distanceBetween = 3.2f;
    public GameObject[] platformPrefabs;
	public Transform platformParent;

    private void Awake()
    {
        prefabqueue = new Queue();
    }

    void Update() {
		if (GameControllerScript.isPaused) {
			return;
		}

		if (yPosAtLastSpawn + distanceBetween < Camera.main.transform.position.y) {
			spawnPlatform ();
			yPosAtLastSpawn += distanceBetween;
		}
	}

	/**
	 * Spawns a random platform type at a random x-position.
     * Only queues if the queue is empty.
	 */
	void spawnPlatform() {
        if (prefabqueue.Count == 0)
        {
            choosePlatform();
        }
        GameObject platformType = (GameObject) prefabqueue.Dequeue();
        float yPos = yPosAtLastSpawn + distanceBetween + 15f;
        Vector2 pos = new Vector2(correctX(), yPos);
        GameObject go = Instantiate(platformType, pos, Quaternion.identity);
        go.transform.parent = platformParent;
        SpawnSecond(platformType, yPos);
	}

	/**
	 * Returns a random platform prefab type. Currently:
	 * 		80% chance normal
	 * 		10% chance trampoline
	 * 		10% chance moving
	 */ 
	void basicPlatformScheme() {
        PlatformScheme(80, 10, 10);
	}

    /**
     * Platformscheme with a variable amount of parameters.
     * 
     * Returns a platform prefab.
     */
    void PlatformScheme(params int[] percentages)
    {
        int randomInt = Random.Range(0, 100);
        int percentageCounter = 0;
        for (int i = 0; i < percentages.Length; i++)
        {
            percentageCounter += percentages[i];
            if (randomInt < percentageCounter)
            {
                prefabqueue.Enqueue(platformPrefabs[i]);
                if (i == 3)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        prefabqueue.Enqueue(platformPrefabs[0]);
                    }
                    prefabqueue.Enqueue(platformPrefabs[3]);
                }
                else if (i == 4)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        prefabqueue.Enqueue(platformPrefabs[5]);
                    }
                }
                return;
            }
        }
    }

    /**
     * Method for choosing platformscheme.
     * Is the game's primary progressionsystem.
     * Difficulty increases as score increases.
     */
    void choosePlatform()
    {
        if (this.transform.position.y < 200)
        {
            basicPlatformScheme();
        }
        else if (this.transform.position.y < 500)
        {
            PlatformScheme(45, 45, 10);
        }
        else if (this.transform.position.y < 700)
        {
            PlatformScheme(0, 100); // First all moving
        }
        else if (this.transform.position.y < 1000)
        {
            PlatformScheme(60, 20, 10, 10); // Flips
        }
        else if (this.transform.position.y < 1500)
        {
            PlatformScheme(30, 40, 5, 15, 10); // More hard stuff, first blink
        }
        else
        {
            PlatformScheme(0, 80, 5, 10, 5); // Fliptastic
        }
    }

	/**
	 * Returns a random x-position to spawn a platform at.
	 * Makes two platforms in the same position twice
	 * in a row less likely, though not impossible
	 */
	int correctX() {
        int x = 2 * Random.Range(-3, 4);
		if (x == lastXPos) {
            x = 2 * Random.Range(-3, 4);
		}
		lastXPos = x;
		return x;
	}

    /**
     * Spawns a second platform at the same yPostion if 
     * all the criterias are met.
     * 
     * Chance to spawn a second one decreases with score
     */
    void SpawnSecond(GameObject platform, float yPosition)
    {
        float yPos = Camera.main.transform.position.y;
        int chance = yPos < 250 ? 60 : 30;

        if (yPos < 400 && (platform.Equals(platformPrefabs[0]) || platform.Equals(platformPrefabs[2])) && Random.Range(0, 100) < chance)
        {
            Vector2 pos = new Vector2(Xvalue(lastXPos), yPosition);
            Instantiate(platformPrefabs[0], pos, Quaternion.identity);
        }
    }

    /**
     * Sets the spawnposition for the other platform.
     * There is always at least two world units between them.
     */
    int Xvalue(int otherX)
    {
        if (otherX < 0)
        {
            return 2 * Random.Range(otherX / 2 + 3, 4);
        }

        return 2 * Random.Range(-3, otherX / 2 - 2);
    }
}
