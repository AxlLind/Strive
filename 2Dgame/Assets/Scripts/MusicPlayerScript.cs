using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayerScript : MonoBehaviour
{

    static bool alreadyExists;

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