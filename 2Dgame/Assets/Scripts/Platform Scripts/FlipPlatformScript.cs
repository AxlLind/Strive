using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipPlatformScript : OnJumpScript {

    public GameControllerScript gcs;

    private new void Awake()
    {
        gcs = GameObject.Find("GameController").GetComponent<GameControllerScript>();
        base.Awake();
    }

    protected override void OnJump()
    {
        StartCoroutine(gcs.RotateCameraSmooth());
    }
}
