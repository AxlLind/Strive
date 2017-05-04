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
	List<Highscore> leaderBoard;
	DatabaseReference highscoreRef;

	void Start() {
		loadingSign.SetActive(true);
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://jumperunitygame.firebaseio.com/");
		highscoreRef = FirebaseDatabase.DefaultInstance.GetReference ("Highscores");
		leaderBoard = new List<Highscore>();
		retrieveHighScoreList ();
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
				DataSnapshot snap = task.Result;
				foreach(var child in snap.Children) {
					string name = child.Key;
					long score = (long) child.Value;
					leaderBoard.Add( new Highscore(name, score) );
				}
				leaderBoard.Sort();
				leaderBoard.Reverse();

				loadingSign.SetActive(false);
				updateHighscoreBoard();
			}
		});
	}

	/**
	 * Updates the text UI-componenets of the scene with
	 * info from the 'leaderBoard'-field.
	 */
	void updateHighscoreBoard() {
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
