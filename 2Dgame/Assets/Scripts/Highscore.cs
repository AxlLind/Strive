using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highscore : IComparable<Highscore> {
	public string name { get;}
	public long score { get;}

	public Highscore(string name, long score) {
		this.name = name;
		this.score = score;
	}

	public int CompareTo(Highscore that) {
		return -this.score.CompareTo (that.score);
	}
}
