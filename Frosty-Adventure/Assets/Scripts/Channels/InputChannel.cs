using System;
using UnityEngine;
using UnityEngine.InputSystem;
//using UnityEngine.SceneManagement;
using static PlayerController;

[CreateAssetMenu(fileName = "Input Channel", menuName = "Channels/Input Channel", order = 0)]
public class InputChannel : ScriptableObject, IPlayerActions
{
    PlayerController inputActions;

    public event Action<Vector2> MoveEvent;
    public event Action JumpEvent;
    public event Action JumpCanceledEvent;

   // public event Action ClickEvent;
   // public event Action PauseEvent;
   //// public event Action PauseCanceledEvent;
   // public event Action ReplayEvent;
   // public event Action HomeEvent;
   // public event Action ResumeEvent;
    //public event Action GameOverEvent;

    private void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerController();
            inputActions.Player.SetCallbacks(this);
           // inputActions.UI.SetCallbacks(this);
            inputActions.Enable();
        }
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    //public void TriggerClickEvent()
    //{
    //    ClickEvent?.Invoke();
    //}

    //public void TriggerPauseEvent()
    //{
    //    PauseEvent?.Invoke();
    //}

    //public void TriggerReplayEvent()
    //{
    //    ReplayEvent?.Invoke();
    //}

    //public void TriggerHomeEvent()
    //{
    //    HomeEvent?.Invoke();
    //}

    //public void TriggerResumeEvent()
    //{
    //    ResumeEvent?.Invoke();
    //}

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log(context.phase);   
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
           //  Debug.Log("Jump canceled");
        }
    }

    //public void EnablePlayer()
    //{
    //    inputActions.Player.Enable();
    //    inputActions.UI.Disable();
    //}

    //public void EnableMenu()
    //{
    //    inputActions.Player.Disable();
    //    inputActions.UI.Enable();
    //}

    //public void OnClick(InputAction.CallbackContext context)
    //{
    //    if (context.phase == InputActionPhase.Performed)
    //    {
    //        TriggerClickEvent();
    //        //Debug.Log("Click detected");
    //    }
    //}
    
    //public void OnReplay(InputAction.CallbackContext context)
    //{
    //    //if (context.phase == InputActionPhase.Performed)
    //    //{
    //    //    TriggerReplayEvent();
    //    //    //Debug.Log("Replay action triggered");
    //    //}
    //}
    
    //public void OnPause(InputAction.CallbackContext context)
    //{
    //    if (context.phase == InputActionPhase.Performed)
    //    {
    //        TriggerPauseEvent();
    //        Debug.Log("Pause action triggered");
    //    }
    //    //else if (context.phase == InputActionPhase.Canceled)
    //    //{
    //    //    PauseCanceledEvent?.Invoke();
    //    //}
    //}

    //public void OnResume(InputAction.CallbackContext context)
    //{
    //    if (context.phase == InputActionPhase.Performed)
    //    {
    //        TriggerResumeEvent();
    //      //  Debug.Log("Resume action triggered");
    //    }
    //}

    //public void OnHome(InputAction.CallbackContext context)
    //{
    //    if (context.phase == InputActionPhase.Performed)
    //    {
    //        TriggerHomeEvent();
    //       // Debug.Log("Main Menu action triggered");         
    //    }
    //}

}