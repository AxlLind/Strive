using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public float gravConst;
	public float jumpHeight;
	public float fallMultiplier;
	public float speed;
	public float maxSpeed;
	public float maxFallSpeed; // negative
	public float drag;

	private Rigidbody2D rb;
	private SpriteRenderer sr;
	private BoxCollider2D bc;
	private bool grounded;

	public Transform groundCheck;


	// Use this for initialization
	void Awake () {
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
		bc = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update() {
        MovementX();
    }

	void FixedUpdate() {
        Gravity();
        grounded = IsGrounded();
        if (grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
        Jump();
    }

	void Gravity() {
		if (rb.velocity.y < maxFallSpeed) {
			rb.velocity = new Vector2 (rb.velocity.x, maxFallSpeed);
			return;
		}
		rb.velocity += new Vector2 (0, -gravConst);
		if (rb.velocity.y < 0) {
			rb.velocity = new Vector2 (rb.velocity.x, rb.velocity.y * fallMultiplier);
		}
	}

	bool IsGrounded() {
		return Physics2D.Linecast (rb.position, groundCheck.position);
	}

	void Jump() {
        
        if (Input.GetButtonDown ("Jump") && grounded) {
			rb.velocity += new Vector2 (0, jumpHeight);
		} else if (Input.GetButtonUp ("Jump") && rb.velocity.y > 0) {
			rb.velocity = new Vector2 (rb.velocity.x, rb.velocity.y * 0.5f);
		}
	}

	void MovementX() {
		if (Mathf.Abs(rb.velocity.x) > maxSpeed) {
			rb.velocity = new Vector2 (Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
			return;
		}

		float h = Input.GetAxisRaw("Horizontal");
		if (h != 0) {
			rb.velocity += new Vector2 (h * speed, 0); 
		} else if (grounded) {
			rb.velocity = new Vector2 (rb.velocity.x * (1f - drag), rb.velocity.y);
		}
	}
}
