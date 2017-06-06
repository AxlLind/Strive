using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class DeathScript : MonoBehaviour {

	public GameObject gameOverScreen;
	public GameObject pauseButton;
	public GameControllerScript gcs;
	public Text gameOverScore;

	/**
	 * Called when the player touches this object and thereby losing the game.
	 * Activates the game over window and 'pauses' the game.
     * 
     * Shows an ad before showing the gameOverScreen.
	 */
	void OnTriggerEnter2D(Collider2D other) {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }

		gameOverScreen.SetActive(true);
		pauseButton.SetActive (false);

		int bestScore = PlayerPrefs.GetInt ("LocalScore");
		if (bestScore < gcs.score) {
			PlayerPrefs.SetInt ("LocalScore", gcs.score);
			gameOverScore.text = "New Highscore! " + gcs.score;
		} else {
			gameOverScore.text = "Score: " + gcs.score;
		}

		gcs.PauseUnPauseGame ();
	}
}
