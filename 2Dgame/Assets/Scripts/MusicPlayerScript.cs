using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayerScript : MonoBehaviour
{

    static bool alreadyExists;

    private AudioSource audioSource;

    void Start()
    {
        if (alreadyExists)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            alreadyExists = true;
            audioSource = GetComponent<AudioSource>();

            if (!GameControllerScript.musicOn)
            {
                audioSource.Pause();
            }
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Start_Screen")
        {
            alreadyExists = false;
            Destroy(gameObject);
        }
    }
}