using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerController;

[CreateAssetMenu(fileName ="Input Channel", menuName = "Channels/Input Channel", order = 0 )]
public class InputChannel : ScriptableObject, IPlayerActions
{
    PlayerController inputActions;

    public event Action<Vector2> MoveEvent;
    public event Action JumpEvent;
    public event Action JumpCanceledEvent;
    public event Action AttackEvent;
    public event Action AttackCanceledEvent;

    private void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerController();
            inputActions.Player.SetCallbacks(this);
            inputActions.Enable();
        }
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log(context.phase);   
        Vector2 moveValue = context.ReadValue<Vector2>();
        MoveEvent?.Invoke(moveValue);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
             AttackEvent?.Invoke();
             Debug.Log("Attacking Detected");
       }
       if (context.phase == InputActionPhase.Canceled)
       {
           AttackCanceledEvent?.Invoke();
           Debug.Log("Attack Canceled");

       }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            JumpEvent?.Invoke();
            Debug.Log("Jump started");
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            JumpCanceledEvent?.Invoke();
            Debug.Log("Jump canceled");
        }
    }

    public void EnablePlayer()
    {
        inputActions.Player.Enable();
    }

    public void EnableMenu()
    {
        inputActions.Player.Disable();
    }

    
    }