using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	Rigidbody2D rb;

	float horizontalSpeed = 15f;
	float extraGravityDown = 1.01f;

	#if (UNITY_ANDROID || UNITY_IOS)
	float lastXMovement = 0f;
	#endif

	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate () {
		if (GameControllerScript.isPaused) {
			return;
		}
		ExtraGravityDown ();
		HorizontalMovement ();
		KeepOnScreen (7.5f);
	}

	/**
	 * Takes input from keyboard if on computer/editor
	 * and from the gyroscope if on android or iOS.
	 * Uses that input to move player in the x-direction.
	 */
	void HorizontalMovement() {
		#if (UNITY_ANDROID || UNITY_IOS)
		float newMovement = 8f * Input.acceleration.x - lastXMovement;
		float h = Mathf.Lerp (lastXMovement, newMovement, 0.1f);

		// Deadzone to prevent small movements
		h = Mathf.Abs (h) < 0.03f ? 0 : h;

		lastXMovement = h;
		#else
		float h = Input.GetAxis("Horizontal");
		#endif

		float flipped = (GameControllerScript.straight == false) ? -1 : 1;
		rb.velocity = new Vector2 (flipped * h * horizontalSpeed, rb.velocity.y);
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
