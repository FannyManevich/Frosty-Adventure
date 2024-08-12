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

    private float jumpForce = 5f;
    private float moveSpeed = 10.0f;
 
    private float horizontal;
    private Rigidbody2D rb;
   //rivate bool isFacingRight;
    private Vector2 moveDirection = Vector2.zero;
   
    private InputChannel inputChannel;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 2;

        AddListeners();
    }
    
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce);
        }

        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();
    }
    
    void FixedUpdate()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, leftWall.position.x + 0.5f, rightWall.position.x - 0.5f);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, bottomWall.position.y + 0.5f, topWall.position.y - 0.5f);
        transform.position = clampedPosition;
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

    public void HandleMovment(Vector2 moveDirection)
    {
        Vector2 newPosition = (Vector2)transform.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
        transform.position = newPosition;

     // Vector2 moveForce = moveDirection * moveSpeed;
        rb.AddForce(moveForce);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void Flip()
    {
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    
}
