using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	public Transform player;

	public float lerpSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 lerpthingy = Vector2.Lerp (transform.position, player.position, lerpSpeed);
		transform.position = new Vector3(lerpthingy.x, lerpthingy.y, transform.position.z);
	}
}
