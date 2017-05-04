using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathByContactScript : MonoBehaviour {

	public GameObject gameOverScreen;
	public GameControllerScript gcs;
	public Text gameOverScore;

	/**
	 * Called when the player touches this object and thereby losing the game.
	 * Activates the game over window and 'pauses' the game.
	 */
	void OnTriggerEnter2D(Collider2D other) {
		// Called when player loses:
		gameOverScreen.SetActive(true);
		gameOverScore.text = "You lose! Score: " + gcs.score;
		gcs.PauseUnPauseGame ();
	}
}
