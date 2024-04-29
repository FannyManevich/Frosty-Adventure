 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Transform leftWall;
    public Transform rightWall;
    public Transform bottomWall;

    
    
    private float moveSpeed = 10.0f;
    private Vector2 moveDirection = Vector2.zero;

    private Rigidbody2D rb;
   // private bool grounded;
    //public float gravityScale = 1.0f;
    //public LayerMask groundLayer;
   // private Movement.PlayerActions playerActions;

    private InputChannel inputChannel;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
      //  rb.gravityScale = 0;

       // playerActions = new Movement.PlayerActions(new Movement());
      //  playerActions.Enable();

        AddListeners();
    }    


    void Update()
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

        rb.velocity = moveDirection * 5;
    }
}
