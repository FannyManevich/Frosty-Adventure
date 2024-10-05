using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsTransitions : MonoBehaviour
{ 
    public Animator anim;

    private enum MovementState {standing, walking, jumping, swimming}
    private MovementState state = MovementState.standing;

    public enum Level { Level1, Level2, Level3, Level4 }   
    private Level currentLevel;

    void Start()
    {                
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("Animator is null, Assign it to the Player object.");
        }

        ActivateLevel(Level.Level1);
    }
    
    public void HandleJump()
    {
        state = MovementState.jumping;
        Debug.Log("Jumping detected!");
        UpdateAnimatorState();
    }

    public void HandleJumpCancel()
    {
        state = MovementState.standing;
        Debug.Log("Jump canceled, back to standing.");
        UpdateAnimatorState();
    }

    public void ActivateLevel(Level level)
    {
        currentLevel = level;

        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0f);
        }

        switch (currentLevel)
        {
            case Level.Level1:
                anim.SetLayerWeight(0, 1f);
                break;
            case Level.Level2:
                anim.SetLayerWeight(1, 1f);
                break;
            case Level.Level3:
                anim.SetLayerWeight(2, 1f);
                break;
            case Level.Level4:
                anim.SetLayerWeight(3, 1f);
                break;
        }
    }

    public void OnLevelComplete(int levelIndex)
    {
        Level newLevel = (Level)levelIndex;
        ActivateLevel(newLevel);
    }

    private void UpdateAnimatorState()
    {
        if (anim != null)
        {
            anim.SetBool("IsStanding", state == MovementState.standing);
            anim.SetBool("IsWalking", state == MovementState.walking);
            anim.SetBool("IsJumping", state == MovementState.jumping);
            anim.SetBool("IsSwimming", state == MovementState.swimming);
            
        }      
    }

    public void HandleMovementAnimation(Vector2 movementInput)
    {   
        Debug.Log("Movement Input: " + movementInput);
        if (state > 0)
        {
            state = MovementState.walking;
        }
        else
        {
            state = MovementState.standing;
        }
        Debug.Log("Movement State: " + state);
        UpdateAnimatorState();
    }


}
