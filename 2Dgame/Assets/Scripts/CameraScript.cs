using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	public float scrollSpeed;
	Transform transform;

	void Awake() {
		transform = GetComponent<Transform> ();
	}

	void Update() {
		transform.position = new Vector3 (transform.position.x, transform.position.y + scrollSpeed, transform.position.z);
	}
}
