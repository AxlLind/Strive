using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnJumpScript : PlatformScript {

    protected bool notJumpedOn;

    protected new void Awake()
    {
        notJumpedOn = true;
        base.Awake();
    }


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
            
            if (notJumpedOn)
            {
                OnJump();
                notJumpedOn = false;
            }
        }

    }

    /**
     * Called the first time the player jumps on the platform
     */
    protected abstract void OnJump();

}
