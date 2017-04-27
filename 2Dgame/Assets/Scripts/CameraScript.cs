using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	public float standardSpeed = 0.05f;
	float scrollSpeed;
	public Rigidbody2D target;
	bool startedMoving = false;

	void Update() {
		if (!startedMoving && target.position.y > 0f) {
			startedMoving = true;
		}

		if (startedMoving) {
			ScrollUp ();
		}
	}

	void ScrollUp() {
		transform.position = new Vector3 (transform.position.x, transform.position.y + scrollSpeed, transform.position.z);
		if (target.position.y - transform.position.y > 5f) {
			scrollSpeed = Mathf.Lerp (scrollSpeed, target.velocity.y, 0.001f);
		} else {
			scrollSpeed = Mathf.Lerp(scrollSpeed, standardSpeed, 0.2f);
		}
	}
}
