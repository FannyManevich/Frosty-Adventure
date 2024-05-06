using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Movement;

[CreateAssetMenu(fileName ="Input Channel", menuName = "Channels/Input Channel", order = 0 )]
public class InputChannel : ScriptableObject, IPlayerActions
{
    Movement move, attack;

    public event Action<Vector2> MoveEvent;
    public event Action AttackEvent;
    public event Action AttackCanceledEvent;

    private void OnEnable()
    {
        if (move == null)
        {
            move = new Movement();
            move.Player.SetCallbacks(this);
            move.Enable();
        }
        if (attack == null)
        {
            attack = new Movement();
            attack.Player.SetCallbacks(this);
            attack.Enable();
        }

    }

    private void OnDisable()
    {
        move.Disable();
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
    public void EnablePlayer()
    {
        move.Player.Enable();

    }

    public void EnableMenu()
    {
        move.Player.Disable();
    }



}