using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;

public class HighscoreScreenController : MonoBehaviour {

	public Button backButton;
	DatabaseReference reference;

	void Start () {
		backButton.onClick.AddListener (OnClickBackButton);

		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://jumperunitygame.firebaseio.com/");

		reference = FirebaseDatabase.DefaultInstance.RootReference;
		string json = JsonUtility.ToJson (new HighscoreObject("Axel", 100));
		Debug.Log(reference.SetRawJsonValueAsync (json));
	}

	void OnClickBackButton() {
		SceneManager.LoadScene ("Start_Screen");
	}
}
