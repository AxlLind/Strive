using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	float standardSpeed;
	float scrollSpeed;

	public Rigidbody2D target;

	void Update() {
		if (GameControllerScript.isPaused) {
			return;
		}
		ScrollUp ();
	}

	/**
	 * Moves the camera up slightly every frame. 
	 * Moves up faster if the player is high up thereby preventing
	 * the player from being able to exit the view.
	 * Uses linear interpolation to make this switch in speed feel smooth.
	 */
	void ScrollUp() {
        StandardSpeed();

        if (target.position.y - transform.position.y > 3f)
        {
            scrollSpeed = Mathf.Lerp(scrollSpeed, target.velocity.y, LerpSpeed());
        }
        else
        {
            scrollSpeed = Mathf.Lerp(scrollSpeed, standardSpeed, LerpSpeed());
        }

        transform.position = new Vector3 (transform.position.x, transform.position.y + scrollSpeed * Time.deltaTime, transform.position.z);
	}

    /**
     * Scrolls up in different speeds, depending on score.
     * To make the start easier.
     */
    void StandardSpeed()
    {
        if (transform.position.y < 200)
        {
            standardSpeed = 2f;
        }
        else if (transform.position.y < 500)
        {
            standardSpeed = 4f;
        }
        else
        {
            standardSpeed = 5.4f;
        }
    }

    /**
     * Lerps faster the farther away from the middle the player is.
     */
    float LerpSpeed()
    {
        float distance = Mathf.Abs(target.position.y - transform.position.y);
        return (distance / 100) * 0.1f * distance;
    }
}
