using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static bool isGameOver;
    public GameObject gameOverScreen;
    [SerializeField] GameObject pauseMenu;
    public Text scoreText;

    private PlayerController UIinput;

    public int health = 3;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Awake()
    {
        isGameOver = false;

        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false); 
            Debug.Log("GameOverScreen is successfully assigned.");
        }
        else
        {
            Debug.LogError("GameOverScreen is not assigned in the Inspector! Please assign it in the Inspector.");
        }
        UIinput = new PlayerController();

        UpdateHealthUI();
    }
    
    public void UpdateScoreText(int scoreCount)
    {
        if (scoreText != null)
        {
            scoreText.text = scoreText.text  + scoreCount;
            Debug.Log($"Score updated to {scoreCount}");
        }
        else
        {
            Debug.LogError("Score text is not assigned!");
        }
    }

    public void Pause()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            Debug.LogError("pauseMenu is not assigned in the Inspector! Please assign it in the Inspector.");

        }
    }
    
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    
    public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1f;
        isGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }   

    public void QuitGame()
    {
        Application.Quit();
    }
    
    private void OnEnable()
    {
        if (UIinput == null)
        {
            UIinput = new PlayerController();
            UIinput.Enable();
            UIinput.UI.Click.performed += OnClick;
        }
    } 
    
    private void OnDisable()
    {
        if (UIinput != null)
        {
            UIinput.UI.Click.performed -= OnClick;
            UIinput.Disable();
        }
    }
    
    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("Click detected");
        }
    }

    public void PlayerisInjured()
    {
        if (health > 0)
        {
            health -= 1;
            hearts[health].sprite = emptyHeart;
            Debug.Log("Player got hurt. Current health: " + health);
        }

        if (health == 0)
        {
            GameOver();
        }
    }

    public void IncreaseHealth()
    {
        if (health < hearts.Length)
        {
            hearts[health].sprite = fullHeart;
            health += 1;
            Debug.Log("Player's health increased. Current health: " + health);
        }
        else
        {
            Debug.Log("Health is already at maximum!");
        }
    }

    private void UpdateHealthUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = (i < health) ? fullHeart : emptyHeart;
        }

    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        isGameOver = true;
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            Debug.LogError("GameOverScreen is not assigned in the Inspector!");
        }
    }
}