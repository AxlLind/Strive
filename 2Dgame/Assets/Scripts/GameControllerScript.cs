using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour {

	public static bool isPaused;
	public GameObject nameInput;
	public Button sendHighscore;
	public Text scoreText;
	public Rigidbody2D playerRB;
	public int score;

	void Start() {
		isPaused = false;
		sendHighscore.interactable = true;
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



	// Button press-methods

	public void OnClickRestart() {
		SceneManager.LoadScene ("Main_Game");
	}

	public void OnClickStartScreen() {
		SceneManager.LoadScene ("Start_Screen");
	}

	public void OnClickSendHighScore() {
		nameInput.SetActive (true);
	}

	public void OnEnterName() {
		FirebaseController.sendHighScore (nameInput.GetComponent<InputField>().text, score);
		nameInput.SetActive (false);
		sendHighscore.interactable = false;
	}

	// Button press-methods
}
