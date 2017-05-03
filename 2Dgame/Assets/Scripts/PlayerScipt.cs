using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScipt : MonoBehaviour {

	Rigidbody2D rb;
    private AudioSource audioSource;

	public float horizontalSpeed;
	public float jumpSpeed;
    public float trampolineMultiplier;
	public float extraGravityDown;
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


	void HorizontalMovement() {
		float h;
		#if (UNITY_ANDROID || UNITY_IOS)
		h = 3f * Input.acceleration.x;
		#else
		h = Input.GetAxis("Horizontal");
		#endif
		rb.velocity = new Vector2 (h * horizontalSpeed,rb.velocity.y);
		KeepOnScreen ();
	}

	void KeepOnScreen() {
		// TODO: Make this better maybe?
		float xMax = 7.5f;
        // rb.position = new Vector2(Mathf.Clamp(rb.position.x, -xMax, xMax), rb.position.y);
		SeamlessEdge(xMax);
	}

    // Method for making the player appear on the other half of the screen
    void SeamlessEdge(float xMax)
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
	void Jump() {
		//bool jumped = Input.GetButtonDown ("Jump"); Now Auto jumping
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

    bool hitTrampoline()
    {
        return Physics2D.Linecast(rb.position, groundCheckLeft.position)
            || Physics2D.Linecast(rb.position, groundCheckRight.position);
    }

	bool isGrounded() {
		return Physics2D.Linecast (rb.position, groundCheckLeft.position, LayerMask.NameToLayer("Trampoline"))
			|| Physics2D.Linecast (rb.position, groundCheckRight.position, LayerMask.NameToLayer("Trampoline"));
	}

	void ExtraGravityDown() {
		if (rb.velocity.y < 0) {
			rb.velocity = new Vector2 (rb.velocity.x, rb.velocity.y * extraGravityDown);
		}
	}
}
