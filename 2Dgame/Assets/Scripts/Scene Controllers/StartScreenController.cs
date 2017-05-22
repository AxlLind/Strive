using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreenController : MonoBehaviour {

	void Start() {
		string sound = PlayerPrefs.GetString ("Sound");
		string music = PlayerPrefs.GetString ("Music");
		string id = PlayerPrefs.GetString ("UserID");
		if (sound == "") {
			PlayerPrefs.SetString ("Sound", "True");
		}
		if (music == "") {
			PlayerPrefs.SetString ("Music", "True");
		}
		if (id == "") {
			PlayerPrefs.SetString ("UserID", RandomLong().ToString());
		}
	}

	/**
	 * Generates a random long number
	 * (how does C# NOT have a built in function for this?!)
	 * Used to generate userID.
	 */
	private long RandomLong() {
		int a = Random.Range (int.MinValue, int.MaxValue);
		int b = Random.Range (int.MinValue, int.MaxValue);
		return ((long) a) << 32 + b;
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
