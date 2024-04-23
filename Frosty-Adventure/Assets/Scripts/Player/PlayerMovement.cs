 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Transform leftWall;
    public Transform rightWall;
    public Transform bottomWall;

    private Rigidbody2D rb;
    private float moveSpeed = 100.0f;
    private Vector2 moveDirection = Vector2.zero;

    

    InputChannel inputChannel;

    void Start()
    {
        AddListeners();
    }    


    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 newPosition = transform.position + new Vector3(horizontalInput, verticalInput, 0) * moveSpeed * Time.deltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, leftWall.position.x + 0.5f, rightWall.position.x - 0.5f);
        newPosition.y = Mathf.Clamp(newPosition.y, bottomWall.position.y + 0.5f, Mathf.Infinity);

        transform.position = newPosition;
    }


    private void FixedUpdate()
    { 
    
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

        rb.velocity = moveDirection * 100;
    }
}
