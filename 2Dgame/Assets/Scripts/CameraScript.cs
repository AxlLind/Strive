using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	public float standardSpeed = 0.05f;
	public float scrollSpeed;
	Transform transform;
	public Rigidbody2D target;

	void Awake() {
		transform = GetComponent<Transform> ();
	}

	void Update() {
		transform.position = new Vector3 (transform.position.x, transform.position.y + scrollSpeed, transform.position.z);
		if (target.position.y - transform.position.y > 5f) {
			float t = 0.01f;
			scrollSpeed = Mathf.Lerp (scrollSpeed, target.velocity.y, 0.001f);
		} else {
			scrollSpeed = Mathf.Lerp(scrollSpeed, standardSpeed, 0.2f);
		}
	}
}
