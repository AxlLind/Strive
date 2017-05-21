using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScreenController : MonoBehaviour {
	public void OnClickBackButton() {
		SceneManager.LoadScene ("Settings_Screen");
	}
}
