
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Transform leftWall;
    public Transform rightWall;
    public Transform bottomWall;
    public Transform topWall;


    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private bool isGrounded;
    public Transform groundCheck;
    public float jumpForce = 5f;
    public float moveSpeed = 10.0f;
    public float smallDownwardForce = 2f;
    private Rigidbody2D rb;
    private InputChannel inputChannel;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 3;
        inputChannel = FindObjectOfType<BeaconSO>().inputChannel;

        AddListeners();

        if (inputChannel == null)
        {
            Debug.LogError("InputChannel not found in BeaconSO!");
        }
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

        inputChannel.MoveEvent += HandleMovment;
    }

    private void FixedUpdate()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, leftWall.position.x + 0.5f, rightWall.position.x - 0.5f);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, bottomWall.position.y + 0.5f, topWall.position.y - 0.5f);
        transform.position = clampedPosition;
    }

    private void MoveEvent(Vector2 moveDirection)
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        rb.velocity = moveDirection * moveSpeed;

        if (isGrounded && moveDirection.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    public void HandleMovment(Vector2 moveDirection)
    {
        Vector2 newPosition = (Vector2)transform.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
        transform.position = newPosition;

        Vector2 moveForce = moveDirection * moveSpeed;
        rb.AddForce(moveForce);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnMove(Vector2 moveDirection)
    {
        if (isGrounded && moveDirection.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else if (!isGrounded)
        {
            rb.AddForce(new Vector2(0, -smallDownwardForce), ForceMode2D.Force);
        }


    }
}