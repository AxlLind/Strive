using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarterScript : MonoBehaviour {
	public GameControllerScript gcs;
	public Text countdown;
	public Rigidbody2D player;

	float shootSpeed = 70f;

	void Start () {
		gcs = GameObject.Find("GameController").GetComponent<GameControllerScript>();
		StartCoroutine (DelayedStart());
	}

	IEnumerator DelayedStart() {
		if (GameControllerScript.isPaused == false) {
			gcs.PauseUnPauseGame ();
		}

		for (int i = 3; i > 0; i--) {
			countdown.text = i.ToString();
			yield return new WaitForSeconds (1);
		}
	
		gcs.PauseUnPauseGame ();

		countdown.gameObject.SetActive (false);
		player.velocity = new Vector2 (0, shootSpeed);
	}
}
