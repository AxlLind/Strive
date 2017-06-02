using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkerScript : OnJumpScript {

    new Renderer renderer;

    private new void Awake()
    {
        renderer = GetComponent<Renderer>();
        base.Awake();
    }

    protected override void OnJump()
    {
        renderer.enabled = true;
    }
}
