using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static PlayerController;

[CreateAssetMenu(fileName = "Input Channel", menuName = "Channels/Input Channel", order = 0)]
public class InputChannel : ScriptableObject, IPlayerActions, IUIActions
{
    PlayerController inputActions;

    public event Action<Vector2> MoveEvent;
    public event Action JumpEvent;
    public event Action JumpCanceledEvent;

    public event Action PauseEvent;
    public event Action ReplayEvent;
    public event Action MainMenuEvent;
    public event Action ResumeEvent;


    private void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerController();
            inputActions.Player.SetCallbacks(this);
            inputActions.UI.SetCallbacks(this);
            inputActions.Enable();
        }
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void TriggerPauseEvent()
    {
        PauseEvent?.Invoke();
    }

    public void TriggerReplayEvent()
    {
        ReplayEvent?.Invoke();
    }

    public void TriggerMainMenuEvent()
    {
        MainMenuEvent?.Invoke();
    }

    public void TriggerResumeEvent()
    {
        ResumeEvent?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // Debug.Log(context.phase);   
        Vector2 moveValue = context.ReadValue<Vector2>();
        MoveEvent?.Invoke(moveValue);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            JumpEvent?.Invoke();
            // Debug.Log("Jump started");
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            JumpCanceledEvent?.Invoke();
            // Debug.Log("Jump canceled");
        }
    }

    public void EnablePlayer()
    {
        inputActions.Player.Enable();
        inputActions.UI.Disable();
    }

    public void EnableMenu()
    {
        inputActions.Player.Disable();
        inputActions.UI.Enable();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("Click detected");
        }
    }
    
    public void OnReplay(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            ReplayEvent?.Invoke();
            Debug.Log("Replay action triggered");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            PauseEvent?.Invoke();
            Debug.Log("Pause action triggered");
        }
    }

    public void OnResume(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            ResumeEvent?.Invoke();
            Debug.Log("Resume action triggered");
           // ResumeGame();
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        ReplayEvent?.Invoke();
        Debug.Log("Game resumed!");
    }

    public void OnMainMenu(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            MainMenuEvent?.Invoke();
            Debug.Log("Main Menu action triggered");
            GoToMainMenu();
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}