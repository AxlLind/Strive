using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreenController : MonoBehaviour {

	void Start() {
		string sound = PlayerPrefs.GetString ("Sound");
		string music = PlayerPrefs.GetString ("Music");
		if (sound == "") {
			PlayerPrefs.SetString ("Sound", "True");
		}
		if (music == "") {
			PlayerPrefs.SetString ("Music", "True");
		}
	}

	public void OnClickStartButton() {
		SceneManager.LoadScene("Main_Game");
	}

	public void OnClickSettingsButton() {
		SceneManager.LoadScene("Settings_Screen");
	}

	public void OnClickHighScoreButton() {
		SceneManager.LoadScene("Highscore_Screen");
	}

	public void OnClickCreditsButton() {
		SceneManager.LoadScene("Credits_Screen");
	}
}
