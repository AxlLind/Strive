using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPlayerScript : MonoBehaviour {
	static bool alreadyExists;

	// Use this for initialization
	void Start () {
		if (alreadyExists) {
			Destroy (gameObject);
		} else {
			DontDestroyOnLoad (gameObject);
			alreadyExists = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (SceneManager.GetActiveScene().name == "Main_Game") {
			alreadyExists = false;
			Destroy (gameObject);
		}
	}
}
