using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour {

	public static bool isPaused = false;
	public Button pauseButton;
	public Text scoreText;
	public Rigidbody2D playerRB;
	Vector2 playerSpeedAtPause;
	int score;

	void Start() {
		pauseButton.onClick.AddListener (OnClickPause);
	}

	void Update () {
		if (GameControllerScript.isPaused) {
			return;
		}
		UpdateScore ();
	}

	void UpdateScore() {
		score = (int)Camera.main.transform.position.y;
		scoreText.text = "Score: " + score;
	}

	void OnClickPause() {
		isPaused = !isPaused;
		playerRB.simulated = !playerRB.simulated;
		if (!isPaused) {
			playerRB.velocity = playerSpeedAtPause;
		} else {
			playerSpeedAtPause = playerRB.velocity;
		}
	}
}
