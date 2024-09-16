// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine.SceneManagement;
// using UnityEngine;

// public class UIManager : MonoBehaviour
// {
//     public static bool isGameOver;
//     public GameObject gameOverScreen;
//     private void Awake()
//     {
//         isGameOver = false;
//     }



//     void Update()
//     {
//         if (isGameOver)
//         {
//             gameOverScreen.SetActive(true);
//         }
//     }

   
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static bool isGameOver;
    public GameObject gameOverScreen;

    private void Awake()
    {
        isGameOver = false;
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }
        else
        {
            Debug.LogError("GameOverScreen is not assigned in the Inspector!");
        }
    }

    void Update()
    {
        if (isGameOver)
        {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0f; // Optional: Pause the game
        }
    }

    // Optional: Method to restart the game
    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume game time
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Optional: Method to quit the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
