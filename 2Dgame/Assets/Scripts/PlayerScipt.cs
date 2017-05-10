using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScipt : MonoBehaviour {

	Rigidbody2D rb;

	float horizontalSpeed = 15f;
	float extraGravityDown = 1.01f;


	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate () {
		if (GameControllerScript.isPaused) {
			return;
		}
		ExtraGravityDown ();
		HorizontalMovement ();
	}

	/**
	 * Takes input from keyboard if on computer/editor
	 * and from the gyroscope if on android or iOS.
	 * Uses that input to move player in the x-direction.
	 */
	void HorizontalMovement() {
		float h;
		#if (UNITY_ANDROID || UNITY_IOS)
		h = 3f * Input.acceleration.x;
		#else
		h = Input.GetAxis("Horizontal");
		#endif
		rb.velocity = new Vector2 (h * horizontalSpeed,rb.velocity.y);
		KeepOnScreen (7.5f);
	}

    /**
     * Method for making the player appear on the other half of the screen
     */
    void KeepOnScreen(float xMax)
    {
        float playerHalf = rb.transform.localScale.x / 2;

        if (rb.position.x > xMax + playerHalf)
        {
            rb.position = new Vector2((-xMax - playerHalf), rb.position.y);
        }
        else if (rb.position.x < -xMax - playerHalf)
        {
            rb.position = new Vector2((xMax + playerHalf), rb.position.y);
        }
    }


	/**
	 * Applys a bit extra velocity down when the player is falling downward.
	 * This creates a smoother jumping feel.
	 */
	void ExtraGravityDown() {
		if (rb.velocity.y < 0) {
			rb.velocity = new Vector2 (rb.velocity.x, rb.velocity.y * extraGravityDown);
		}
	}
}
