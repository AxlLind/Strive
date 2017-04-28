using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreenController : MonoBehaviour {

	public Button startButton;
	public Button settingsButton;
	public Button highscoreButton;

	void Start() {
		startButton.onClick.AddListener(OnClickStartButton);
		settingsButton.onClick.AddListener(OnClickSettingsButton);
		highscoreButton.onClick.AddListener(OnClickHighScoreButton);
	}

	void OnClickStartButton() {
		SceneManager.LoadScene("Main_Game");
	}

	void OnClickSettingsButton() {
		SceneManager.LoadScene("Settings_Screen");
	}

	void OnClickHighScoreButton() {
		SceneManager.LoadScene("Highscore_Screen");
	}
}
