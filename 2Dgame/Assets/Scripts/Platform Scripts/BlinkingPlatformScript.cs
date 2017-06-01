using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingPlatformScript : PlatformScript {

    private bool notJumpedOn;
    private GameControllerScript gcs;

    private new void Awake()
    {
        notJumpedOn = true;
        gcs = GameObject.Find("GameController").GetComponent<GameControllerScript>();
        base.Awake();
    }

    /**
     * Stops the blinking of this platform and blinks away
     * the next three platforms
     */
    private new void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.rigidbody;
        float playerHalf = rb.transform.localScale.x / 2;
        float dist = (rb.position.y - playerHalf) - this.transform.position.y;

        if (rb.velocity.y <= 0.1f && dist >= -0.12f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            if (GameControllerScript.soundOn)
            {
                audioSource.Play();
            }

            if (notJumpedOn)
            {
                GameObject[] blinkers = GameObject.FindGameObjectsWithTag("Blinker");
                StartCoroutine(PauseForSeconds(2));
                foreach (GameObject blinker in blinkers)
                {
                    StartCoroutine(Disappear(blinker.GetComponent<Renderer>()));
                }
                notJumpedOn = false;
            }
        }
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

    IEnumerator PauseForSeconds(float seconds)
    {
        gcs.PauseUnPauseGame();
        yield return new WaitForSeconds(seconds);
        gcs.PauseUnPauseGame();
    }
}
