using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathByContactScript : MonoBehaviour {

	public GameObject gameOverScreen;
	public GameControllerScript gcs;
	public Text gameOverScore;

	void OnTriggerEnter2D(Collider2D other) {
		// Called when player loses:
		gameOverScreen.SetActive(true);
		gameOverScore.text = "You lose! Score: " + gcs.score;
		gcs.PauseUnPauseGame ();
	}
}
