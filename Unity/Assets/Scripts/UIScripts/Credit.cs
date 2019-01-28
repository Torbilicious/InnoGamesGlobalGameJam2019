using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credit : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("SplashScreen");
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("SplashScreen");
    }

}
