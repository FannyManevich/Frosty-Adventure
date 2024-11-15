using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private Button pauseButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button replayButton;

    public Text scoreText;
    private PlayerController playerController;

    private void Start()
    {
        AddListeners();

        Scene currentScene = SceneManager.GetActiveScene();
        Debug.Log("Current active scene: " + currentScene.name);

        var healthManager = FindObjectOfType<HealthManager>();
        if (healthManager != null)
        {
            healthManager.GameOverEvent += OnGameOver;
        }
        else
        {
            Debug.LogError("HealthManager not found!");
        }
    }

    private void AddListeners()
    {
        pauseButton.onClick.AddListener(OnPauseButtonClicked);
        resumeButton.onClick.AddListener(OnResumeButtonClicked);
        replayButton.onClick.AddListener(OnReplayButtonClicked);
        homeButton.onClick.AddListener(OnHomeButtonClicked);

        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        
        UpdateButtonInteractivity();
    }
    private void OnEnable()
    {
        playerController = new PlayerController();
        playerController.UI.Pause.performed += OnPausePerformed;
        playerController.UI.Resume.performed += OnResumePerformed;
        playerController.UI.Replay.performed += OnReplayPerformed;
        playerController.UI.Home.performed += OnHomePerformed;
        playerController.Enable();
    }

    private void OnDisable()
    {
        playerController.UI.Pause.performed -= OnPausePerformed;
        playerController.UI.Resume.performed -= OnResumePerformed;
        playerController.UI.Replay.performed -= OnReplayPerformed;
        playerController.UI.Home.performed -= OnHomePerformed;
        playerController.Disable();
    }

    public void QuitGame()
    {
        Application.Quit();
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

    private void OnHomePerformed(InputAction.CallbackContext context)
    {
        if (pausePanel.activeSelf)
        {
            OnHomeButtonClicked();
        }
        Debug.Log("Home action performed");
    }

    private void OnPausePerformed(InputAction.CallbackContext context)
    {
        if (!pausePanel.activeSelf)
        {
            OnPauseButtonClicked();
        }
        Debug.Log("Pause action performed");
    }

    private void OnResumePerformed(InputAction.CallbackContext context)
    {
        if (pausePanel.activeSelf)
        {
            OnResumeButtonClicked();
        }
        Debug.Log("Resume action performed");
    }

    private void OnReplayPerformed(InputAction.CallbackContext context)
    {
        if (gameOverPanel.activeSelf)
        {
            OnReplayButtonClicked();
        }
        Debug.Log("Replay action performed");
    }

    public void OnPauseButtonClicked()
    {
        if (pausePanel.activeSelf)
        {
            Debug.Log("Pause button clicked");
            pausePanel.SetActive(false);
            Time.timeScale = pausePanel.activeSelf ? 0f : 1f;

            pauseButton.interactable = true;
        }
        else
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
            pauseButton.interactable = false;          
        }
        UpdateButtonInteractivity();      
    }

    private void UpdateButtonInteractivity()
    {
        bool isPausePanelActive = pausePanel.activeSelf;
        pauseButton.interactable = !isPausePanelActive;
        resumeButton.interactable = isPausePanelActive;
        replayButton.interactable = isPausePanelActive;
        homeButton.interactable = isPausePanelActive; 
        Debug.Log($"Button states updated: pauseButton {pauseButton.interactable}, resumeButton {resumeButton.interactable}, replayButton {replayButton.interactable}, homeButton {homeButton.interactable}");
    }
    public void OnReplayButtonClicked()
    {
        Debug.Log("Replay button clicked");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnHomeButtonClicked()
    {
        Debug.Log("Main Menu button clicked");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void OnResumeButtonClicked()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        Debug.Log("Game resumed");
    }

    private void OnGameOver()
    {
        ShowGameOverPanel();
    }
  
    public void ShowGameOverPanel()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
           // Debug.Log("Game Over panel shown");
        }
        else
        {
            Debug.LogError("GameOverPanel is not assigned!");
        }
    }    
}