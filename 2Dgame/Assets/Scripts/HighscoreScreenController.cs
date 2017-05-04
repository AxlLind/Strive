using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighscoreScreenController : MonoBehaviour {

	public void OnClickBackButton() {
		SceneManager.LoadScene ("Start_Screen");
	}
}
