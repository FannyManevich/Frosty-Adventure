using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsTransitions : MonoBehaviour
{ 
   private enum MovementState {standing,walking,jumping,swimming}
    private Rigidbody2D rb;
   
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        
    }
}
