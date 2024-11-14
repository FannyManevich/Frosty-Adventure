using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class GameStateManager : MonoBehaviour
{
    public static event Action<GameState> OnGameStateChange;
    public event Action GameOverEvent;

   // public InputChannel inputChannel;
    public GameStateChannel gameStateChannel;

    public GameState currentState;
    public GameState CurrentGameState => currentState;
   // public GameState previosState;
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
    }

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
        }

        if (newState.currentState == GameState.State.GameOver)
        {
            Debug.Log("Game Over State triggered.");
            GameOverEvent?.Invoke();
        }
    }   
}