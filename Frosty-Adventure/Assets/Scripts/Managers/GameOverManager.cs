using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public event Action GameOverEvent;
    public InputChannel inputChannel;

    private bool isGameOver;
    public GameState currentState;

    public GameObject gameOverScreen;

    private void OnEnable()
    {
       // inputChannel.ReplayEvent += HandleReplay;
        GameOverEvent += GameOver;
    }

    private void OnDisable()
    {
       // inputChannel.ReplayEvent -= HandleReplay;
        GameOverEvent -= GameOver;
    }

    public void TriggerGameOverEvent()
    {
        GameOverEvent?.Invoke();
    }

    public void HandleReplay()
    {
        Debug.Log("Replay game...");
      //  currentState.SetState(GameState.State.Playing);

        Time.timeScale = 1f;
        isGameOver = false;
       // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    private void GameOver()
    {
        if (!isGameOver)
        {
            Debug.Log("Game Over!");
            isGameOver = true;

            if (gameOverScreen != null)
            {
                currentState.currentState = GameState.State.GameOver;
                gameOverScreen.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                Debug.LogError("GameOverScreen is not assigned in the Inspector!");
            }
        }      
    }
}
