using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineScript : PlatformScript
{

    public TrampolineScript()
    {
        jumpSpeed = 40f;
    }

    new void FixedUpdate()
    {
        if (GameControllerScript.isPaused)
        {
            return;
        }

        base.FixedUpdate();
    }


    private new void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    

}
