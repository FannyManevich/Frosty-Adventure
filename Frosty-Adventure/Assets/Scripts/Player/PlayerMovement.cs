
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputChannel inputChannel;
    public Transform leftWall;
    public Transform rightWall;
    public Transform bottomWall;
    public Transform topWall;

    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isGrounded;
    public Transform groundCheck;
    public float jumpForce = 5f;
    public float moveSpeed;

    private Rigidbody2D rb;
    Vector2 moveDirection;

    private bool isJumping;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 9f;
        
        if (inputChannel == null)
        {
            Debug.LogError("InputChannel not found in BeaconSO!");
        }

        AddListeners();
    }

    private void AddListeners()
    {
        var beacon = FindObjectOfType<BeaconSO>();
        if (beacon == null)
        {
            Debug.LogError("BeaconSO not found!");
            return;
        }

        inputChannel = beacon.inputChannel;
        if (inputChannel == null)
        {
            Debug.LogError("InputChannel not found in BeaconSO!");
            return;
        }
        else
        {
            inputChannel.MoveEvent += HandleMovement;
            inputChannel.JumpEvent += HandleJump;
        }
        if (isGrounded)
        {
            isJumping = false;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            isJumping = false;
        }
    }

    private void MoveEvent(Vector2 moveDirection)
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        rb.velocity = moveDirection * moveSpeed;

        if (isGrounded && moveDirection.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (isGrounded)
        {
            isJumping = false;
        }
    }

    public void HandleMovement(Vector2 moveDirection)
    {
        this.moveDirection = moveDirection * moveSpeed;
    }

    private void HandleJump()
    {
        if (isGrounded)
        {
            isJumping = true;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Debug.Log("Jump performed!");
        }  
    }

    public bool IsJumping()
    {
        return isJumping;
    }

    private void OnDestroy()
    {
        if (inputChannel != null)
        {
            inputChannel.MoveEvent -= HandleMovement;
            inputChannel.JumpEvent -= HandleJump;
        }
    }
}
