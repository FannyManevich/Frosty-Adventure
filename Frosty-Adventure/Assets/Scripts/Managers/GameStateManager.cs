using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class GameStateManager : MonoBehaviour
{
    public static event Action<GameState> OnGameStateChange;
    public event Action GameOverEvent;

   // public static event Action OnEnterPortal;

    public GameStateChannel gameStateChannel;
    public PlayerInput playerInput;

    public GameState currentState;
    public GameState CurrentGameState => currentState;
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

        GameStateManager.OnGameStateChange += HandleGameStateChange;
    }

    public void TransitionToLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
        Debug.Log("Transitioning to: " + levelName);
    }

    //private void OnEnable()
    //{
    //    Portal.OnEnterPortal += HandlePortalEntry;
    //}

    //private void OnDisable()
    //{
    //    Portal.OnEnterPortal -= HandlePortalEntry;
    //}

    public void SetCurrentState(GameState state)
    {
        currentState = state;
        if (gameStateChannel != null)
        {
            gameStateChannel.StateEntered(state);
        }
        else
        {
            Debug.LogError("GameStateChannel is not assigned!");
        }
    }
    private void HandleGameStateChange(GameState newState)
    {
        if (playerInput == null)
        {
            Debug.LogError("PlayerInput component not assigned.");
            return;
        }
        switch (newState.stateName)
        {
            case "GameplayState":
                playerInput.SwitchCurrentActionMap("Player");
                Debug.Log("Switched to Player action map.");
                break;

            case "MainMenuState":
            case "GameOverState":
                playerInput.SwitchCurrentActionMap("UI");
                Debug.Log("Switched to UI action map.");
                break;

            case "PausedState":
                playerInput.SwitchCurrentActionMap("UI");
                Debug.Log("Switched to UI action map - Paused.");
                break;

            default:
                Debug.LogWarning(newState.stateName);
                break;
        }
    }

    private void HandlePortalEntry()
    {
        Debug.Log("Portal entered, transitioning to Level 3...");
        TransitionToLevel("Level_3");
    }

    public void ChangeState(GameState newState)
    {
        if (gameStateChannel != null)
        {
            gameStateChannel.StateExited(currentState);
            currentState = newState;
            gameStateChannel.StateEntered(currentState);
        }
        else
        {
            Debug.LogError("GameStateChannel is not assigned!");
        }
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

            if (newState.currentState == GameState.State.GameOver)
            {
                Debug.Log("Game Over State triggered.");
                GameOverEvent?.Invoke();
            }
        }        
    }   
}