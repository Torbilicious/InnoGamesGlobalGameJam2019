using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    // Start is called before the first frame update

    public void LoadNextLevel()
    {
        if(GameState.nexLevel >= 0) 
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level " + GameState.nexLevel);
        }else{
            SceneManager.LoadScene(Scene.MENU_CREDITS);
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(Scene.MENU_MAIN);
    }
}
