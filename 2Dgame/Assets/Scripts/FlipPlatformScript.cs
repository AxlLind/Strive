using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipPlatformScript : PlatformScript {

    public GameControllerScript gcs;

    private bool didWeFlip;

    private new void Awake()
    {
        didWeFlip = false;
        gcs = GameObject.Find("GameController").GetComponent<GameControllerScript>();
        base.Awake();
    }

    private new void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.rigidbody;
        float playerHalf = rb.transform.localScale.x / 2;
        float dist = (rb.position.y - playerHalf) - this.transform.position.y;

        if (rb.velocity.y <= 0 && dist >= 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
			if (GameControllerScript.soundOn)
			{
				audioSource.Play ();
			}

            if (!didWeFlip)
            {
                gcs.StartCoroutine("RotateCameraSmooth");
                didWeFlip = true;
            }
        }
    }
}
