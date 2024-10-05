using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            if (player != null && player.IsJumping())
            {
                Destroy(gameObject);
                Debug.Log("Enemy destroyed by jump!");
            }
        }
    }

}
