using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour {
	
	void FixedUpdate () {
		if (GameControllerScript.isPaused) {
			return;
		}
		float diff = Camera.main.transform.position.y - this.transform.position.y;
		if (diff > 25f) {
			Destroy (this.gameObject);
		}
	}

}
