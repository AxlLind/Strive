using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoverScript : MonoBehaviour {

    public float speed;


    private Transform transform;
    private bool movingRight;
 
    

    private void Awake()
    {
        if (Random.Range(0, 2) == 0)
        {
            movingRight = true;
        }
        else
        {
            movingRight = false;
        }
        transform = GetComponent<Transform>();
    }

    void Update () {
        if (transform.position.x > 8 - this.transform.localScale.x/2)
        {
            movingRight = false;
        }
        else if (transform.position.x < -8 + this.transform.localScale.x/2)
        {
            movingRight = true;
        }

		if (movingRight)
        {
            transform.position = new Vector2(transform.position.x + speed, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed, transform.position.y);
        }
	}


}
