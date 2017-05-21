using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsScreenController : MonoBehaviour {
	public Toggle soundToggle;
	public Toggle musicToggle;

	void Start() {
		string sound = PlayerPrefs.GetString ("Sound");
		soundToggle.isOn = (sound == "True");

		string music = PlayerPrefs.GetString ("Music");
		musicToggle.isOn = (music == "True");
	}

	void Update() {
		if (Input.GetKeyDown( KeyCode.Escape )) {
			OnClickBackButton();
		}
	}

	public void OnClickBackButton() {
		SceneManager.LoadScene ("Start_Screen");
	}

	public void OnClickCreditsButton() {
		SceneManager.LoadScene ("Credits_Screen");
	}

	public void OnToggleSound() {
		PlayerPrefs.SetString("Sound", soundToggle.isOn ? "True" : "False");
	}

	public void OnToggleMusic() {
		PlayerPrefs.SetString("Music", musicToggle.isOn ? "True" : "False");
	}
}
