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

    // Method to update collected bottles
    public void UpdateWaterCollected(int collected, int total)
    {
        Debug.Log($"Collected {collected} out of {total} bottles.");
        // Update your UI here (e.g., progress bars, text, etc.)
    }

    // Method to update remaining lives
    public void UpdateLives(int lives)
    {
        Debug.Log($"Lives remaining: {lives}");
        // Update your UI here (e.g., hearts, numbers, etc.)
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
