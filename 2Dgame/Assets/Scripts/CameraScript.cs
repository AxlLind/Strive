using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	float standardSpeed = 5.4f;
	float scrollSpeed;
	bool startedMoving = false;

	public Rigidbody2D target;

	void Update() {
		if (GameControllerScript.isPaused) {
			return;
		}
		if (!startedMoving && target.position.y > 0f) {
			startedMoving = true;
		}
		if (startedMoving) {
			ScrollUp ();
		}
	}

	/**
	 * Moves the camera up slightly every frame. 
	 * Moves up faster if the player is high up thereby preventing
	 * the player from being able to exit the view.
	 * Uses linear interpolation to make this switch in speed feel smooth.
	 */
	void ScrollUp() {
		transform.position = new Vector3 (transform.position.x, transform.position.y + scrollSpeed*Time.deltaTime, transform.position.z);

		if (target.position.y - transform.position.y > 5f) {
			scrollSpeed = Mathf.Lerp (scrollSpeed, target.velocity.y, 0.1f);
		} else {
			scrollSpeed = Mathf.Lerp(scrollSpeed, standardSpeed, 2f);
		}
	}
}
