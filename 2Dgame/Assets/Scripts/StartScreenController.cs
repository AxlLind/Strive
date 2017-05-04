using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreenController : MonoBehaviour {
	public void OnClickStartButton() {
		SceneManager.LoadScene("Main_Game");
	}

	public void OnClickSettingsButton() {
		SceneManager.LoadScene("Settings_Screen");
	}

	public void OnClickHighScoreButton() {
		SceneManager.LoadScene("Highscore_Screen");
	}
}
