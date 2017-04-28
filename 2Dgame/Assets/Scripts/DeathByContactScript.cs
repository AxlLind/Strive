using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathByContactScript : MonoBehaviour {

	public GameObject gameOverScreen;
	public GameControllerScript gcs;

	void OnCollisionEnter2D(Collision2D other) {
		// Called when player loses:
		gameOverScreen.SetActive(true);
		gcs.PauseUnPauseGame ();
	}
}
