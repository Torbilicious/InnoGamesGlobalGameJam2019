using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject PauseMenuUI;
    public GameObject PauseMenuBackground;

    private void Start()
    {
        PauseMenuBackground.SetActive(false);
        PauseMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
     
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        PauseMenuBackground.SetActive(false);
        PauseMenuUI.SetActive(false);
        
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SplashScreen");
    }

    void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
        PauseMenuBackground.SetActive(true);
        PauseMenuUI.SetActive(true);
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
