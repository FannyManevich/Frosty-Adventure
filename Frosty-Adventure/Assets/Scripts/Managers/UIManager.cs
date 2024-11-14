using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public InputChannel inputChannel;

    public GameObject mainMenuUI;
    public GameObject gameplayUI;
    public GameObject gameOverUI;

    public Text scoreText;

    private void Start()
    {
        AddListeners();
    }

    private void AddListeners()
    {
        var beacon = FindObjectOfType<BeaconSO>();
        if (beacon == null)
        {
            Debug.LogError("BeaconSO not found!");
            return;
        }

        inputChannel = beacon.inputChannel;
        if (inputChannel == null)
        {
            Debug.LogError("InputChannel not found in BeaconSO!");
            return;
        }

        GameStateManager.OnGameStateChange += HandleGameStateChange;

        inputChannel.PauseEvent += OnPauseButtonClicked;
        inputChannel.ReplayEvent += OnReplayButtonClicked;
        inputChannel.MainMenuEvent += OnMainMenuButtonClicked;
        inputChannel.ResumeEvent += OnResumeButtonClicked;

        Debug.Log("Listeners added successfully.");
    }

    private void OnDisable()
    {
        GameStateManager.OnGameStateChange -= HandleGameStateChange;

        if (inputChannel != null)
        {
            inputChannel.PauseEvent -= OnPauseButtonClicked;
            inputChannel.ReplayEvent -= OnReplayButtonClicked;
            inputChannel.MainMenuEvent -= OnMainMenuButtonClicked;
            inputChannel.ResumeEvent -= OnResumeButtonClicked;
        }

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

    private void HandleGameStateChange(GameState newState)
    {
        mainMenuUI.SetActive(false);
        gameplayUI.SetActive(false);
        gameOverUI.SetActive(false);

        if (newState.stateName == "MainMenuState")
        {
            mainMenuUI.SetActive(true);
        }
        else if (newState.stateName == "GameplayState")
        {
            gameplayUI.SetActive(true);
        }
        else if (newState.stateName == "GameOverState")
        {
            gameOverUI.SetActive(true);
        }
    }

    public void OnPauseButtonClicked()
    {
        if (inputChannel != null)
        {
            inputChannel.TriggerPauseEvent();
        }
    }

    public void OnReplayButtonClicked()
    {
        if (inputChannel != null)
        {
            inputChannel.TriggerReplayEvent();
        }
    }

    public void OnMainMenuButtonClicked()
    {
        if (inputChannel != null)
        {
            inputChannel.TriggerMainMenuEvent();
        }
    }

    public void OnResumeButtonClicked()
    {
        if (inputChannel != null)
        {
            inputChannel.TriggerResumeEvent();
        }
    }
}