using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour {

	public Text scoreText;
	int score;

	// Update is called once per frame
	void Update () {
		UpdateScore ();
	}

	void UpdateScore() {
		score = (int)Camera.main.transform.position.y;
		scoreText.text = "Score: " + score;
	}
}
