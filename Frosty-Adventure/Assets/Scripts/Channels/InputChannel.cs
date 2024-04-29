using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Movement;

[CreateAssetMenu(fileName ="Input Channel", menuName = "Channels/Input Channel", order = 0 )]
public class InputChannel : ScriptableObject, IPlayerActions
{
    Movement move, attack;

    public event Action<Vector2> MoveEvent;
    

    private void OnEnable()
    {
        if (move == null)
        {
            move = new Movement();
            move.Player.SetCallbacks(this);
            move.Enable();
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