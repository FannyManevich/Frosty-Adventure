using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class GameStateManager : MonoBehaviour
{
    public static event Action<GameState> OnGameStateChange;
    public event Action GameOverEvent;

    public InputChannel inputChannel;

    public GameState currentState;
    public GameState gameOverState;

    private void Start()
    {       
        if (currentState != null)
        {
            SetState(currentState);
        }
        else
        {
            Debug.LogError("CurrentState is not assigned.");
        }

        AddListeners();
    }

    private void Update()
    {
        if (currentState == gameOverState)
        {
            Debug.Log("Game Over state reached.");
            GameOverEvent?.Invoke();
        }
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

        inputChannel.PauseEvent += PauseGame;
      //  inputChannel.ResumeEvent += ResumeGame;
      //  inputChannel.ReplayEvent += ReplayGame;
        inputChannel.MainMenuEvent += GoToMainMenu;

        Debug.Log("Listeners added successfully.");
    }

    private void OnDisable()
    {
        inputChannel.PauseEvent -= PauseGame;
       // inputChannel.ResumeEvent -= ResumeGame;
       // inputChannel.ReplayEvent -= ReplayGame;
        inputChannel.MainMenuEvent -= GoToMainMenu;
    }

    private void PauseGame()
    {
        if (currentState.currentState == GameState.State.Paused)
        {
            currentState.currentState = GameState.State.Playing;
            Time.timeScale = 1f;
        }
        else
        {
            currentState.currentState = GameState.State.Paused;
            Time.timeScale = 0f;
        }

        OnGameStateChange?.Invoke(currentState);
    }

    public void SetState(GameState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
            OnGameStateChange?.Invoke(currentState);
            Debug.Log("State changed to: " + currentState.stateName);
        }

        if (newState.currentState == GameState.State.GameOver)
        {
            Debug.Log("Game Over State triggered.");
            GameOverEvent?.Invoke();
        }
    }

    private void GoToMainMenu()
    {
        Debug.Log("Going to Main Menu.");
        SceneManager.LoadScene("Menu");
    }
}