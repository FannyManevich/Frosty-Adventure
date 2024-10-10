using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static PlayerController;

public class UIManager : MonoBehaviour
{
    public static bool isGameOver;
    public GameObject gameOverScreen;
    public Text scoreText;

    public PlayerController UIinput;

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
    }

    private void Update()
    {
        if (isGameOver)
        {
            if (gameOverScreen != null)
            {
                gameOverScreen.SetActive(true); 
                Time.timeScale = 0f; 
            }
            else
            {
                Debug.LogError("GameOverScreen is not assigned in the Inspector and cannot be displayed!");
            }
        }
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

    public void UpdateLives(int lives)
    {
        Debug.Log($"Lives remaining: {lives}");
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
}