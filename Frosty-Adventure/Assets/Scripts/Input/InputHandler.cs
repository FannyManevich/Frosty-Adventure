using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private PlayerInput playerInput;

    private void OnEnable()
    {
        GameStateManager.OnGameStateChange += HandleGameStateChange;
    }

    private void OnDisable()
    {
        GameStateManager.OnGameStateChange -= HandleGameStateChange;
    }

    private void HandleGameStateChange(GameState newState)
    {
        playerInput = GetComponent<PlayerInput>();

        if (newState.stateName == "GameplayState")
        {
            playerInput.SwitchCurrentActionMap("Player");
        }
        else if (newState.stateName == "MainMenuState")
        {
            playerInput.SwitchCurrentActionMap("UI");
        }
        else if (newState.stateName == "GameOverState")
        {
            playerInput.SwitchCurrentActionMap("UI");
        }
    }
}