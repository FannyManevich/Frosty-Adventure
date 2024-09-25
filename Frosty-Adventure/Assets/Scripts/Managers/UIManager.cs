using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static bool isGameOver;
    public GameObject gameOverScreen;

    private void Awake()
    {
        isGameOver = false;

        // Check if GameOverScreen is assigned in the Inspector
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false); // Hide GameOverScreen on start
            Debug.Log("GameOverScreen is successfully assigned.");
        }
        else
        {
            Debug.LogError("GameOverScreen is not assigned in the Inspector! Please assign it in the Inspector.");
        }
    }

    private void Update()
    {
        if (isGameOver)
        {
            if (gameOverScreen != null)
            {
                gameOverScreen.SetActive(true); // Show the GameOverScreen when game is over
                Time.timeScale = 0f; // Pause the game
            }
            else
            {
                Debug.LogError("GameOverScreen is not assigned in the Inspector and cannot be displayed!");
            }
        }
    }

    // Method to update collected bottles in the UI
    public void UpdateWaterCollected(int collected, int total)
    {
        Debug.Log($"Collected {collected} out of {total} bottles.");
        // Update the UI for collected bottles here (e.g., progress bars, text, etc.)
    }

    // Method to update remaining lives in the UI
    public void UpdateLives(int lives)
    {
        Debug.Log($"Lives remaining: {lives}");
        // Update the UI for remaining lives here (e.g., hearts, numbers, etc.)
    }

    // Method to restart the game
    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume game time
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    // Method to quit the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
