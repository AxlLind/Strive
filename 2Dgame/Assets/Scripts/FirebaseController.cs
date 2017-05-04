using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;


public static class FirebaseController {
	static DatabaseReference highscoreRef;

	private static void getDatabaseRef() {
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://jumperunitygame.firebaseio.com/");
		highscoreRef = FirebaseDatabase.DefaultInstance.GetReference ("Highscores");
	}

	public static void sendHighScore(string name, long score) {
		if (highscoreRef == null) {
			getDatabaseRef ();
		}
		highscoreRef.Child (name).SetValueAsync (score);
	}
}
