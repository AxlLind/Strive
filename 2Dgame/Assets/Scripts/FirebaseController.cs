using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;


public static class FirebaseController {
	static DatabaseReference highscoreRef;
	public static ArrayList leaderBoard;

	private static void getDatabaseRef() {
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://jumperunitygame.firebaseio.com/");
		highscoreRef = FirebaseDatabase.DefaultInstance.GetReference ("Highscores");
		leaderBoard = new ArrayList ();
		receiveHighScoreList ();
	}

	public static void sendHighScore(string name, long score) {
		if (highscoreRef == null) {
			getDatabaseRef ();
		}
		highscoreRef.Child (name).SetValueAsync (score);
	}

	public static void receiveHighScoreList() {
		if (highscoreRef == null) {
			getDatabaseRef ();
		}
		highscoreRef.GetValueAsync ().ContinueWith (task => {
			if (task.IsFaulted) {
				Debug.Log("Firebase FAILED");
			}
			else if (task.IsCompleted) {
				DataSnapshot snap = task.Result;
				foreach(var child in snap.Children) {
					long score = (long) child.Value;
					string name = child.Key;
					leaderBoard.Add( new HighscoreObject(name, score) );
				}
			}
		});
	}
}
