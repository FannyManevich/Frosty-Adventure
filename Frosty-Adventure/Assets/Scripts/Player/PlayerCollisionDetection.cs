using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCollisionDetection : MonoBehaviour
{
   // public static event Action OnPlayerEnterPortal;

    public Text ScoreText;
    private int scoreCount = 0;

    public HealthManager healthManager;
    public GameStateManager gameStateManager;

    public void Start()
    {
        healthManager = FindObjectOfType<HealthManager>();

        if (healthManager == null)
        {
            Debug.LogError("HealthManager not found in the scene! Assign it in the Inspector.");
        }
        if (ScoreText == null)
        {
            Debug.LogError("Score Text is not assigned! Assign it in the Inspector.");
        }
    }
    
    //private void OnEnable()
    //{
    //    Portal.OnPlayerEnterPortal += HandlePortalEntry;
    //}

    //private void OnDisable()
    //{
    //    Portal.OnPlayerEnterPortal -= HandlePortalEntry;
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with: " + other.gameObject.name);

        Pickup pickup = other.GetComponent<Pickup>();
        if (pickup != null)
        {
            Debug.Log("Pickup detected: " + pickup.pickupType.typeName);

            if (pickup.pickupType.typeName == "Heart")
            {
                healthManager.IncreaseHealth();
                Destroy(other.gameObject);
            }
            else if (pickup.pickupType.typeName == "Collectible")
            {
                CollectibleScore collectible = other.GetComponent<CollectibleScore>();
                if (collectible != null)
                {
                    int collectibleScore = collectible.Score;
                    scoreCount += collectibleScore;

                    Debug.Log("Item collected, Score: " + collectibleScore);

                    UpdateScoreText();
                    Destroy(other.gameObject);
                }
                else
                {
                    Debug.LogError("CollectibleScore component not found on the collectible object.");
                }
            }
            else if (pickup.pickupType.typeName == "Enemy")
            {
               
                    Debug.Log("Player killed the enemy: " + other.gameObject.name);
                    Destroy(other.gameObject);
            }
            else if (pickup.pickupType.typeName == "Damage")
            {
                Debug.Log("Player got hurt by an enemy: " + other.gameObject.name);
                healthManager.PlayerisInjured();
            }
            else if (other.CompareTag("Portal"))
            {
                Debug.Log("Portal entered, transitioning to Level 3...");
                SceneManager.LoadScene("Level_3");
                //TransitionToLevel("Level_3");
              //  OnPlayerEnterPortal?.Invoke();
            }
        }      
    }

   //private void HandlePortalEntry()
   //{
   //     OnPlayerEnterPortal?.Invoke();
   //}

    private void UpdateScoreText()
    {
        if (ScoreText != null)
        {
            ScoreText.text = "Score: " + scoreCount;
        }
        else
        {
            Debug.LogError("Score Text is not assigned in the Inspector!");
        }
    }
}