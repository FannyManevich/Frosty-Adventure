using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Level3Movement : MonoBehaviour
{
    private Rigidbody2D rb;
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
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDirection * 5;
    }
}
