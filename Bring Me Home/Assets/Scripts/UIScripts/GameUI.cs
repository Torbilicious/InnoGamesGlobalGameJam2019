using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public PauseMenu pause;
    public GameObject replay;
    public GameObject menu;
    // Start is called before the first frame update
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadPause()
    {
        pause.Pause();
        replay.SetActive(false);
        menu.SetActive(false);
    }
}
