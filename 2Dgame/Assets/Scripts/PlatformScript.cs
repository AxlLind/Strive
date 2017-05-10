using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour {

    protected float jumpSpeed = 20f;
    protected AudioSource audioSource;

    private void Awake()
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

        if (rb.velocity.y <= 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            audioSource.Play();
        }
        
    }

}
