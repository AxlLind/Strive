using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScipt : MonoBehaviour {

	Rigidbody2D rb;
	public float horizontalSpeed;
	public float jumpSpeed;
	public float extraGravityDown;
	public Transform groundCheckLeft;
	public Transform groundCheckRight;
	public bool grounded;

	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate () {
		grounded = isGrounded ();
		ExtraGravityDown ();
		HorizontalMovement ();
		Jump ();
	}

	void HorizontalMovement() {
		//float h = Input.acceleration.x;
		float h = Input.GetAxis("Horizontal");
		rb.velocity = new Vector2 (h * horizontalSpeed,rb.velocity.y);
	}

	void Jump() {
		//bool jumped = Input.GetButtonDown ("Jump"); Now Auto jumping
		if (rb.velocity.y <= 0 && grounded) {
			rb.velocity = new Vector2 (rb.velocity.x , jumpSpeed);
		}
	}

	bool isGrounded() {
		return Physics2D.Linecast (rb.position, groundCheckLeft.position)
			|| Physics2D.Linecast (rb.position, groundCheckRight.position);
	}

	void ExtraGravityDown() {
		if (rb.velocity.y < 0) {
			rb.velocity = new Vector2 (rb.velocity.x, rb.velocity.y * extraGravityDown);
		}
	}
}
