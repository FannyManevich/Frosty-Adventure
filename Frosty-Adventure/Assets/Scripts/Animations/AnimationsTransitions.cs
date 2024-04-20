using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationsTransitions : MonoBehaviour
{ 
    private enum MovementState {standing,walking,jumping,swimming}
    private Rigidbody2D rb;
    private Animator anim;
    private MovementState state;

    void Start()
    {
        state = MovementState.standing;
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
        if(anim is null)
        {
            Debug.LogError("Animator is null, please assign it");
        }
    }


    private void Update()
    {
        anim.SetBool("IsWalking", state == MovementState.walking);
        anim.SetBool("IsJumping", state == MovementState.jumping);
        anim.SetBool("IsSwimming", state == MovementState.swimming);
    }

    private void OnMovementInputPerformed(InputAction.CallbackContext context)
    {
        Vector2 inputDirection = context.ReadValue<Vector2>();

        if (inputDirection != Vector2.zero)
        {
            state = MovementState.walking;
        }
        else
        {
            state = MovementState.standing;
        }
    }

  
    private void OnMovementInputCanceled(InputAction.CallbackContext context)
    {
        state = MovementState.standing;
    }

}
