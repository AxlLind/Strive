using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorImageScript : MonoBehaviour {

    private Rigidbody2D player;
    private float offset;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        offset = this.transform.position.x - player.transform.position.x;
    }

    private void Update()
    {
        this.transform.position = new Vector2(player.transform.position.x + offset, player.transform.position.y);
    }
}
