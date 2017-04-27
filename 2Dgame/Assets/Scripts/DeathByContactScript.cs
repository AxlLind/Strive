using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathByContactScript : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D other) {
		// Called when player loses:
		// TODO: Implement losing condition.
		SceneManager.LoadScene("Level 1");
	}
}
