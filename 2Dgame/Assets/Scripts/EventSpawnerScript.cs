using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSpawnerScript : MonoBehaviour {

    public GameObject mirrorImage;

    private void Start()
    {
        SpawnMirrorImages(5);
    }

    public void SpawnMirrorImages(int numberOf)
    {
        float gameWidth = 16f;
        for (int i = 0; i < numberOf * 2; i++)
        {
            Instantiate(mirrorImage, new Vector2(-gameWidth + ((gameWidth / numberOf) * i), 0), Quaternion.identity);
        }
    }
}
