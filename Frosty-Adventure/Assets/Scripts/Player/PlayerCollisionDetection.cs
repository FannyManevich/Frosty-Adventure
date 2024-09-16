// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class PlayerCollisionDetection : MonoBehaviour
// {
//     public Text WaterText;
//     private int countBottles;

//     public void Start()
//     {
//         countBottles = 0;
//         UpdateThirstText();
//     }

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Collectible"))
//         {

//             Destroy(other.gameObject);

//             Debug.Log("Water bottle collected!");
//             countBottles += 1;

//             UpdateThirstText();
//         }
//     }

//     private void UpdateThirstText()
//     {
//         if (WaterText != null)
//         {       
//             WaterText.text = "Water collected: " + countBottles;
//         }
//         else
//         {
//             Debug.LogError("WaterCollected Text is not assigned in the Inspector!");
//         }
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollisionDetection : MonoBehaviour
{
    public Text WaterText;
    private int countBottles;

    // Reference to the HealthManager
    private HealthManager healthManager;

    public void Start()
    {
        countBottles = 0;
        UpdateThirstText();

        // Find and assign the HealthManager in the scene
        healthManager = FindObjectOfType<HealthManager>();
        if (healthManager == null)
        {
            Debug.LogError("HealthManager not found in the scene!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectible"))
        {
            Destroy(other.gameObject);
            Debug.Log("Water bottle collected!");
            countBottles += 1;
            UpdateThirstText();
        }
        else if (other.CompareTag("Enemy"))
        {
            Debug.Log("Player hit by " + other.tag);
            if (healthManager != null)
            {
                healthManager.TakeDamage();
            }
        }
    }

    private void UpdateThirstText()
    {
        if (WaterText != null)
        {       
            WaterText.text = "Water collected: " + countBottles;
        }
        else
        {
            Debug.LogError("WaterCollected Text is not assigned in the Inspector!");
        }
    }
}
