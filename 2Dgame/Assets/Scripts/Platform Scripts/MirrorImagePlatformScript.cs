using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorImagePlatformScript : OnJumpScript {

    public GameObject mirrorImage;

    private Rigidbody2D playerPos;

    private new void Awake()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        base.Awake();
    }

    protected override void OnJump()
    {
        SpawnMirrorImages(2);
    }
    
    private void SpawnMirrorImages(int numberOf)
    {
        float gameWidth = 16f;
        for (int i = 0; i < numberOf * 2; i++)
        {
            Instantiate(mirrorImage, new Vector2(-gameWidth - (playerPos.position.x) + ((gameWidth / numberOf) * i), playerPos.position.y), Quaternion.identity);
        }
    }
}
