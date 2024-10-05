using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UserInput;

[CreateAssetMenu(fileName = "Click Channel", menuName = "Channels/Click Channel", order = 2)]

public class ClickChannel : ScriptableObject, IClickOptionActions
{
    public UserInput userInput;
    public Movement playerInput;

    public Action clickEvent;
    public Action clickCanceledEvent;

    public void OnEnable()
    {
        if (userInput == null)
        {
            userInput = new UserInput();
            userInput.ClickOption.SetCallbacks(this);
            userInput.Enable();
        }      
    }

    private void OnDisable()
    {
        userInput?.Disable();
    }

    public void EnableGameplayInput()
    {
        playerInput?.Player.Enable();
        userInput?.ClickOption.Disable();
    }

    public void EnableMenuInput()
    {
        userInput?.ClickOption.Enable();
        playerInput?.Player.Disable();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            clickEvent?.Invoke();
            //Debug.Log("Mouse Click Detected");
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            clickCanceledEvent?.Invoke();
        }
    }

}
