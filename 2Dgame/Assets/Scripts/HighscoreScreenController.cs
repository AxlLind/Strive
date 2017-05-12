using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;

public class HighscoreScreenController : MonoBehaviour {

	public GameObject loadingSign;
	public List<Text> uiTexts;

	List<Highscore> leaderBoard = new List<Highscore>();
	DatabaseReference highscoreRef;

	bool hasInternet = false;
	bool hasGotList = false;

	void Start() {
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://jumperunitygame.firebaseio.com/");
		highscoreRef = FirebaseDatabase.DefaultInstance.GetReference ("Highscores");

		// In a coroutine to prevent the game being unresponsive until it gets a response
		StartCoroutine (checkConnection());
	}

	void Update() {
		if (!hasInternet || hasGotList) {
			return;
		}

		hasGotList = true;
		retrieveHighScoreList ();
	}

	/**
	 * Checks to see if the user has an internet connection.
	 * Does this by pinging google.com and sets the 'hasInternet' variable
	 * to true if it gets a response.
	 */
	IEnumerator checkConnection() {
		WWW web = new WWW ("http://google.com");
		yield return web;
		if (web.error != null) {
			loadingSign.GetComponent<Text>().text = "No Internet";
			Debug.Log ("No Internet Connection!");
		}
		hasInternet = (web.error == null);
	}

	/**
	 * Asks the firebase database for the data under the "Highscores"-node
	 * Populates the field "leaderBoard" with Highscore objects.
	 * Then calls updateHighscoreBoard().
	 */
	void retrieveHighScoreList() {
		highscoreRef.GetValueAsync ().ContinueWith (task => {
			if (task.IsFaulted) {
				Debug.Log("Firebase FAILED");
			}
			else if (task.IsCompleted) {
				foreach(var child in task.Result.Children) {
					foreach(var hs in child.Children) {
						// should only be one interation in the inner loop
						leaderBoard.Add( new Highscore(hs.Key, (long) hs.Value) );
					}
				}
				leaderBoard.Sort();
				updateHighscoreBoard();
			}
		});
	}

	/**
	 * Updates the text UI-componenets of the scene with
	 * info from the 'leaderBoard'-field.
	 */
	void updateHighscoreBoard() {
		loadingSign.SetActive(false);
		int count = leaderBoard.Count > 10 ? 10 : leaderBoard.Count;

		for (int i = 0; i < count; i++) {
			Highscore hs = leaderBoard [i];
			uiTexts [i].text = (i+1) + ". " + hs.score + ", " + hs.name;
		}
	}

	public void OnClickBackButton() {
		SceneManager.LoadScene ("Start_Screen");
	}
}
