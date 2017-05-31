using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkerScript : PlatformScript {

    new Renderer renderer;

    private new void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    /**
     * Enables the renderer when jumped on
     */
    protected new void OnCollisionEnter2D(Collision2D collision)
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
            renderer.enabled = true;
        }
    }

}
