using UnityEngine;
using UnityEngine.UI;

public class PlayerCollisionDetection : MonoBehaviour
{
    public Text ScoreText;
    private int scoreCount = 0;

    public HealthManager healthManager;

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
                if (GetComponent<Rigidbody2D>().velocity.y < 0)
                {
                    Debug.Log("Player killed the enemy: " + other.gameObject.name);
                    Destroy(other.gameObject);
                }
                else
                {
                    Debug.Log("Player got hurt by an enemy:  " + other.tag);
                    healthManager.PlayerisInjured();
                }
            }
            else if (pickup.pickupType.typeName == "Damage")
            {
                Debug.Log("Player got hurt by an enemy: " + other.gameObject.name);
                healthManager.PlayerisInjured();
            }
        }
    }
   
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