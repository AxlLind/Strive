using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreObject {
	string name;
	long score;

	public HighscoreObject(string name, long score) {
		this.name = name;
		this.score = score;
	}

	public string getName() {
		return this.name;
	}

	public long getScore() {
		return this.score;
	}
}
