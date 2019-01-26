using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    // Start is called before the first frame update

    public void LoadNextLevel()
    {
        if(GameState.nexLevel >= 0) 
        {
            SceneManager.LoadScene("Level " + GameState.nexLevel);
        }else{
            SceneManager.LoadScene("Credits");
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Splashscreen");
    }
}
