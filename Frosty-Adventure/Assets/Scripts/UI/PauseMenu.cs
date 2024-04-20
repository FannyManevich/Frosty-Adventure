using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void pause()
    {
        pauseMenu.SetActive(true);
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Resume()
    {
       pauseMenu.SetActive(false); 
        Time.timeScale = 1;
    }
}
