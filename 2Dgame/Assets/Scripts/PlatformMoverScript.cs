using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoverScript : PlatformScript {

    public float speed;
    bool movingRight;

    private void Awake() 
	{
		movingRight = Random.Range (0, 2) == 0 ? true : false;
    }

    new void FixedUpdate () {
		if (GameControllerScript.isPaused) {
			return;
		}

		base.FixedUpdate ();

        if (transform.position.x > 8 - this.transform.localScale.x/2)
        {
            movingRight = false;
        }
        else if (transform.position.x < -8 + this.transform.localScale.x/2)
        {
            movingRight = true;
        }

		float directionSpeed = movingRight ? speed : -speed;
		transform.position = new Vector2(transform.position.x + directionSpeed, transform.position.y);
	}


}
