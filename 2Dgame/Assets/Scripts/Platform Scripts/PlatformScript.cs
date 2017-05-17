using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour {

    protected float jumpSpeed = 20f;
    protected AudioSource audioSource;

    protected void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /**
	 * Destorys the platform if it has moved below the camera's view.
	 */
    protected void FixedUpdate () {
		if (GameControllerScript.isPaused) {
			return;
		}
		float diff = Camera.main.transform.position.y - this.transform.position.y;
		if (diff > 25f) {
			Destroy (this.gameObject);
		}
	}


    protected void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.rigidbody;
		float playerHalf = rb.transform.localScale.x / 2;
		float dist = (rb.position.y - playerHalf) - this.transform.position.y;

		if (rb.velocity.y <= 0 && dist >= -0.2f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
			if (GameControllerScript.soundOn) {
            	audioSource.Play();
			}
        }
        
    }

}
