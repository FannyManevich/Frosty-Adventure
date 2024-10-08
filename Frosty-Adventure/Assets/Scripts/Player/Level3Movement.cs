using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Level3Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 100.0f;
    private Vector2 moveDirection = Vector2.zero;
    public InputChannel inputChannel;

    public Transform leftWall;
    public Transform rightWall;
    public Transform topWall;
    public Transform bottomWall;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        inputChannel.MoveEvent += HandleMovment;
    }

    public void HandleMovment(Vector2 moveDirection)
    {
        this.moveDirection = moveDirection;
        //Vector2 newPosition = (Vector2)transform.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
        //transform.position = newPosition;

        //rb.velocity = moveDirection * 2;
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDirection * 2;
    }
    void Update()
    {
        //Vector3 clampedPosition = transform.position;
        //clampedPosition.x = Mathf.Clamp(clampedPosition.x, leftWall.position.x + 0.5f, rightWall.position.x - 0.5f);
        //clampedPosition.y = Mathf.Clamp(clampedPosition.y, bottomWall.position.y + 0.5f, topWall.position.y - 0.5f);
        //transform.position = clampedPosition;
    }
}
