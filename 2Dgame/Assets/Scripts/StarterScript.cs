using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarterScript : MonoBehaviour {
	public GameControllerScript gcs;
	public GameObject pauseButton;
	public Text countdown;
	public Rigidbody2D player;
	public AudioSource audioSource;

	float shootSpeed = 70f;

	void Start () {
		gcs = GameObject.Find("GameController").GetComponent<GameControllerScript>();
		StartCoroutine (DelayedStart());
	}

	void Update() {
		float diff = Camera.main.transform.position.y - this.transform.position.y;
		if (diff > 25f) {
			Destroy (this.gameObject);
		}
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
		pauseButton.SetActive (true);
		if (GameControllerScript.soundOn) {
			audioSource.Play ();
		}
	}
}
