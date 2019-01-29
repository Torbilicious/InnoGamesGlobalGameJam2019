using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public static class SceneManager
{
    public static void LoadScene(Scene scene)
    {
        AsyncSceneLoader.Scene = scene;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Loader");
    }
}
