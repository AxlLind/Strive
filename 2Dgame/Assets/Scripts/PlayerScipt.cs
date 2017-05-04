using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScipt : MonoBehaviour {

	Rigidbody2D rb;
    AudioSource audioSource;

	float horizontalSpeed = 15f;
	float jumpSpeed = 20f;
    float trampolineMultiplier = 2f;
	float extraGravityDown = 1.01f;

	public Transform groundCheckLeft;
	public Transform groundCheckRight;

	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
        audioSource = GetComponent<AudioSource>();
	}

	void FixedUpdate () {
		if (GameControllerScript.isPaused) {
			return;
		}
		ExtraGravityDown ();
		Jump ();
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
	 * Makes the player automatically jump if the player is grounded
	 * i.e has hit a platform.
	 */
	void Jump() {
		if (rb.velocity.y <= 0 && isGrounded()) {
			rb.velocity = new Vector2 (rb.velocity.x , jumpSpeed);
            audioSource.Play();
		}
        else if (rb.velocity.y <= 0 && hitTrampoline())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed * trampolineMultiplier);
            // Play boing sound
        }
	}

	/**
	 * Checks if the player is standing on a trampoline-platform
	 */
    bool hitTrampoline()
    {
        return Physics2D.Linecast(rb.position, groundCheckLeft.position)
            || Physics2D.Linecast(rb.position, groundCheckRight.position);
    }

	/**
	 * Checks if the player is standing on a normal-platform
	 */
	bool isGrounded() {
		return Physics2D.Linecast (rb.position, groundCheckLeft.position, LayerMask.NameToLayer("Trampoline"))
			|| Physics2D.Linecast (rb.position, groundCheckRight.position, LayerMask.NameToLayer("Trampoline"));
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
