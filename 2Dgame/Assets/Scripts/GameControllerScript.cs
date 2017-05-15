using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;

public class GameControllerScript : MonoBehaviour {

	public static bool isPaused;
	public Text scoreText;
	public Rigidbody2D playerRB;
	public int score;

	public GameObject scoreOnGameOver;
	public InputField nameInput;
	public Button sendHighscore;
    public Transform cameraTransform;

	public Button sendButton;
	public Sprite pauseSprite;
	public Sprite playSprite;

	public Sprite enterName;
	public Sprite enterNameHi;

	void Start() {
		#if (UNITY_ANDROID || UNITY_IOS)
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		#endif
		isPaused = false;
		sendHighscore.interactable = true;
	}

	void Update () {
		if (isPaused) {
			return;
		}
        this.transform.position = cameraTransform.position;
		UpdateScore ();
	}

	/**
	 * Updates the UI-text object showing the score
	 * Takes the score simply from the camera's y-position.
	 */
	void UpdateScore() {
		score = (int) Camera.main.transform.position.y;
		scoreText.text = "Score: " + score;
	}

	/**
	 * Pauses and unpauses the game (depending on the current state).
	 * Does this by the field 'isPaused' and by turning off 'simulated'
	 * on the player.
	 */
	public void PauseUnPauseGame() {
		isPaused = !isPaused;
		playerRB.simulated = isPaused ? false : true;
	}

	/**
	 * Generates a random long number
	 * (how does C# NOT have a built in function for this?!)
	 * Used as an ID for high score submissions.
	 */
	private long RandomLong() {
		int a = Random.Range (int.MinValue, int.MaxValue);
		int b = Random.Range (int.MinValue, int.MaxValue);
		return ((long) a) << 32 + b;
	}



	// Button press-methods

	public void OnClickPause(Image img) {
		img.overrideSprite = isPaused ? pauseSprite : playSprite;
		PauseUnPauseGame ();
	}

	public void OnClickRestart() {
		SceneManager.LoadScene ("Main_Game");
	}

	public void OnClickStartScreen() {
		SceneManager.LoadScene ("Start_Screen");
	}

	public void OnClickSendHighScore() {
		if (nameInput.gameObject.activeSelf) {
			SendHighscore();
		} else {
			nameInput.gameObject.SetActive (true);
			scoreOnGameOver.SetActive (false);
		}
	}

	/**
	 * Sends the input from the 'input-field' along with the current score
	 * to the firebase database. Also makes the send highscore button inactive
	 * and changes the text to "High score sent!"
	 * 
	 * This is called when pressing enter in the input-field or when pressing the send HS button again.
	 */
	public void SendHighscore() {
		string name = nameInput.text.Trim();
		if (name.Length > 0) {
			FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://jumperunitygame.firebaseio.com/");
			DatabaseReference highscoreRef = FirebaseDatabase.DefaultInstance.GetReference ("Highscores");

			highscoreRef.Child( RandomLong().ToString() ).Child( name ).SetValueAsync( score );

			nameInput.gameObject.SetActive (false);
			scoreOnGameOver.SetActive (true);
			sendHighscore.interactable = false;
		} else {
			sendButton.image.overrideSprite = enterName;
			Sprite spr = sendButton.spriteState.highlightedSprite; 
			spr = enterNameHi;
		}
	}

	// Button press-methods
}
