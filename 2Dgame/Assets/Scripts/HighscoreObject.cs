using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreObject : IComparable<HighscoreObject> {
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

	public int CompareTo(HighscoreObject that) {
		return this.score.CompareTo (that.score);
	}
}
