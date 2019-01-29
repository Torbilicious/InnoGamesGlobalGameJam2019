using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public static class SceneManager
{
    public static Scene Scene = Scene.MENU_MAIN;
    public static int LevelId = -1;

    public static void LoadScene(Scene scene, int levelId = -1)
    {
        Scene = scene;
        LevelId = levelId;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Loader");
    }
}
