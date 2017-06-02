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
	public static bool soundOn;
	public static bool musicOn;
	public static bool straight;

	public Text scoreText;
	public Rigidbody2D playerRB;
	public int score;

	public GameObject pauseScreen;
    public GameObject pauseButton;
	public GameObject scoreOnGameOver;
	public InputField nameInput;
	public Button sendHighscore;
    public Transform cameraTransform;

	public Button sendButton;
	public Sprite pauseSprite;
	public Sprite playSprite;

	public Sprite enterName;
	public Sprite enterNameHi;

	public TextMesh highscoreText;
	public Transform scoreLine;

	void Start() {
		#if (UNITY_ANDROID || UNITY_IOS)
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		#endif
		sendHighscore.interactable = true;
        straight = true;

		int highscore = PlayerPrefs.GetInt ("LocalScore");
		int y = highscore == 0 ? -100 : highscore;
		scoreLine.position = new Vector2 (scoreLine.position.x, y);
		highscoreText.text = "Highscore: " + highscore;

		string sound = PlayerPrefs.GetString ("Sound");
		soundOn = (sound == "True");

		string music = PlayerPrefs.GetString ("Music");
		musicOn = (music == "True");
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

    public IEnumerator PauseForSeconds(float seconds)
    {
        pauseButton.SetActive(false);
        PauseUnPauseGame();
        yield return new WaitForSeconds(seconds);
        PauseUnPauseGame();
        pauseButton.SetActive(true);
    }

    // Button press-methods

    public void OnClickPause(Image img) {
		img.overrideSprite = isPaused ? pauseSprite : playSprite;
		pauseScreen.SetActive( !isPaused );
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
			nameInput.text = PlayerPrefs.GetString ("UserName");
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
			PlayerPrefs.SetString ("UserName", name);
			FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://jumperunitygame.firebaseio.com/");
			DatabaseReference highscoreRef = FirebaseDatabase.DefaultInstance.GetReference ("Highscores");
			string userID = PlayerPrefs.GetString ("UserID");

			highscoreRef.Child( userID ).Child( RandomLong().ToString() ).Child( name ).SetValueAsync( score );

			nameInput.gameObject.SetActive (false);
			scoreOnGameOver.SetActive (true);
			sendHighscore.interactable = false;
		} else {
			sendButton.image.overrideSprite = enterName;
			Sprite spr = sendButton.spriteState.highlightedSprite; 
			spr = enterNameHi;
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

	// Button press-methods


    /**
     * Rotates the camera smooooth
     *
     */ 
    public IEnumerator RotateCameraSmooth()
    {
        PauseUnPauseGame();

		float angle = straight ? 180 : 0;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
        {
            cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, Quaternion.Euler(0, 0, angle), t);
            yield return new WaitForEndOfFrame();
        }
        straight = !straight;

        PauseUnPauseGame();
    }
}
