using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsScreenController : MonoBehaviour {

	public Button backButton;

	void Start () {
		backButton.onClick.AddListener (OnClickBackButton);
	}

	void OnClickBackButton() {
		SceneManager.LoadScene ("Start_Screen");
	}
}
