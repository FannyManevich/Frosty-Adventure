using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI Manager")]
    public UIManager uiManager; // Reference to the UIManager

    [Header("Game Settings")]
    public int startingLives = 3;

    private int lives;
    private int totalBottles;
    private int bottlesCollected;

    private void Awake()
    {
        // Implement Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        lives = startingLives;
        InitializeScene();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        Bottle.OnBottleCollected += HandleBottleCollected; // Subscribe to the event from Bottle
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Bottle.OnBottleCollected -= HandleBottleCollected; // Unsubscribe from the event
    }

    // Initialize game state for each scene
    private void InitializeScene()
    {
        // Find all Bottle objects in the current scene
        Bottle[] bottles = FindObjectsOfType<Bottle>();
        totalBottles = bottles.Length;
        bottlesCollected = 0;

        // Find the Portal in the scene and deactivate it
        GameObject portal = GameObject.FindWithTag("Portal");
        if (portal != null)
        {
            portal.SetActive(false);
        }

        // Update the UI
        UpdateUI();
    }

    // Handle scene load
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeScene();
    }

    // This method is triggered when a bottle is collected
    private void HandleBottleCollected()
    {
        bottlesCollected++;
        UpdateUI();

        if (bottlesCollected >= totalBottles)
        {
            ActivatePortal();
        }
    }

    /// <summary>
    /// Activates the portal when all bottles are collected.
    /// </summary>
    private void ActivatePortal()
    {
        GameObject portal = GameObject.FindWithTag("Portal");
        if (portal != null)
        {
            portal.SetActive(true);
            Debug.Log("All bottles collected! Portal activated.");
        }
        else
        {
            Debug.LogError("Portal not found in the scene!");
        }
    }

    public void LoseLife()
    {
        lives--;
        UpdateUI();
        Debug.Log("Player hit! Lives remaining: " + lives);

        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            // Reload the current scene
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        // Load Game Over scene or restart the game
        SceneManager.LoadScene("GameOverScene"); // Ensure you have a GameOverScene
    }

    private void UpdateUI()
    {
        if (uiManager != null)
        {
            uiManager.UpdateWaterCollected(bottlesCollected, totalBottles);
            uiManager.UpdateLives(lives);
        }
        else
        {
            Debug.LogWarning("UIManager is not assigned in GameManager.");
        }
    }

    public void LoadNextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(currentIndex + 1);
        }
        else
        {
            Debug.Log("All levels completed!");
            SceneManager.LoadScene("MainMenu"); // Replace with your main menu scene
        }
    }
}
