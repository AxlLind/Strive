using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour {

	public static bool isPaused;
	public Button pauseButton;
	public Button restartButton;
	public Button startScreenButton;
	public Text scoreText;
	public Rigidbody2D playerRB;
	int score;

	void Start() {
		isPaused = false;
		pauseButton.onClick.AddListener (PauseUnPauseGame);
		restartButton.onClick.AddListener (OnClickRestart);
		startScreenButton.onClick.AddListener (OnClickStartScreen);
	}

	void Update () {
		if (isPaused) {
			return;
		}
		UpdateScore ();
	}

	void UpdateScore() {
		score = (int)Camera.main.transform.position.y;
		scoreText.text = "Score: " + score;
	}

	public void PauseUnPauseGame() {
		isPaused = !isPaused;
		playerRB.simulated = isPaused ? false : true;

	}

	void OnClickRestart() {
		SceneManager.LoadScene ("Main_Game");
	}

	void OnClickStartScreen() {
		SceneManager.LoadScene ("Start_Screen");
	}
}
