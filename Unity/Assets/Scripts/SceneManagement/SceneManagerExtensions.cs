using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneManager
{
    public static void LoadScene(Scene scene)
    {
        switch(scene)
        {
            case Scene.MENU_MAIN:
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
                break;
            case Scene.MENU_CREDITS:
                UnityEngine.SceneManagement.SceneManager.LoadScene("CreditMenu");
                break;
            case Scene.MENU_WIN:
                UnityEngine.SceneManagement.SceneManager.LoadScene("WinMenu");
                break;
        }
    }
}
