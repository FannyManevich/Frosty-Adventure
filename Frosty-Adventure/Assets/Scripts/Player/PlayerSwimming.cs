using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSwimming : MonoBehaviour
{
    InputChannel inputChannel;

    private Rigidbody2D rb;

    private float moveSpeed = 10.0f;

    private Vector2 moveDirection = Vector2.zero;

    public Transform leftWall;
    public Transform rightWall;
    public Transform topWall;
    public Transform bottomWall;

    //public Animator anim;
    // private enum MovementState {walking,catnip,hurt}

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       // anim = GetComponent<Animator>();

        AddListeners();
    }
    void Update()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, leftWall.position.x + 0.5f, rightWall.position.x - 0.5f);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, bottomWall.position.y + 0.5f, topWall.position.y - 0.5f);
        transform.position = clampedPosition;
    }
    public void HandleMovment(Vector2 moveDirection)
    {
        Vector2 newPosition = (Vector2)transform.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
        transform.position = newPosition;

        rb.velocity = moveDirection * 10;


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
    //private void OnTriggerEnter2D(Collider2D collision)
   // {

   // }
}
