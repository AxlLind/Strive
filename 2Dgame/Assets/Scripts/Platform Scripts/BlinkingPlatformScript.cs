using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingPlatformScript : OnJumpScript {

    private GameControllerScript gcs;
    private PlatformSpawnerScript spawnerScript;

    private new void Awake()
    {
        GameObject gameController = GameObject.Find("GameController");
        gcs = gameController.GetComponent<GameControllerScript>();
        spawnerScript = gameController.GetComponentInChildren<PlatformSpawnerScript>();
        base.Awake();
    }


    /**
     * Makes the GameObject blink for a bit and the disappear
     * 
     * parameter renderer: The renderer of the GameObject
     */
    IEnumerator Disappear(Renderer renderer)
    {
        yield return new WaitForSeconds(Random.Range(0f, 0.2f));
        bool boolean = false;
        for (int i = 0; i < 7; i++)
        {
            if (renderer != null)
            {
                renderer.enabled = boolean;
            }

            yield return new WaitForSeconds(0.1f);
            boolean = !boolean;
        }
    }

    /**
     * Blinks away the next three blinkers
     */
    protected override void OnJump()
    {
        GameObject[] blinkers = GameObject.FindGameObjectsWithTag("Blinker");
        StartCoroutine(gcs.PauseForSeconds(1.2f));
        foreach (GameObject blinker in blinkers)
        {
            float distance = blinker.transform.position.y - this.transform.position.y;
            if (distance <= 3 * spawnerScript.distanceBetween + 0.1 * spawnerScript.distanceBetween && distance > 0)
            {
                StartCoroutine(Disappear(blinker.GetComponent<Renderer>()));
            }
        }
    }
}
