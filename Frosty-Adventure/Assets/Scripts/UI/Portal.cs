using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
   // public static event Action OnPlayerEnterPortal;

    public string levelToLoad = "Level_3";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Portal entered, transitioning to Level 3...");
            SceneManager.LoadScene("Level_3"); ; 
        }
        else
        {
            Debug.Log("Non-player object entered portal: " + other.name);
        }
    }

}