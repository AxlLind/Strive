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
        SpawnMirrorImages(5);
    }
    
    private void SpawnMirrorImages(int numberOf)
    {
        GameObject[] mirrorImages = new GameObject[numberOf * 2];
        float gameWidth = 16f;
        for (int i = 0; i < numberOf * 2; i++)
        {
            GameObject go = Instantiate(mirrorImage, new Vector2(-gameWidth - playerPos.position.x + ((gameWidth / numberOf) * i), 0), Quaternion.identity);
            mirrorImages[i] = go;
        }
    }
}
