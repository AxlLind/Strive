using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipPlatformScript : PlatformScript {

    private bool didWeFlip;
    private Transform cameraTransform;

    private new void Awake()
    {
        didWeFlip = false;
        cameraTransform = Camera.main.transform;
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
            audioSource.Play();

            if (!didWeFlip)
            {
                cameraTransform.Rotate(0, 0, 180);
                didWeFlip = true;
            }
        }
    }
}
