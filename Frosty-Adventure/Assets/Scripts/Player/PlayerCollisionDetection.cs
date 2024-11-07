using UnityEngine;
using UnityEngine.UI;

public class PlayerCollisionDetection : MonoBehaviour
{
    public Text ScoreText;

    private int scoreCount = 0;
    private UIManager UImanager;

    public void Start()
    {
        UImanager = FindObjectOfType<UIManager>();
        if (UImanager == null)
        {
            Debug.LogError("UIManager not found in the scene! Assign it in the Inspector.");
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
                UImanager.IncreaseHealth();
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
                    UImanager.PlayerisInjured();
                }
            }
            else if (pickup.pickupType.typeName == "Traps")
            {
                Debug.Log("Player got hurt by a trap: " + other.gameObject.name);
                UImanager.PlayerisInjured();
            }
        }
        else
        {
            Debug.LogWarning("Pickup component not found on the collided object.");
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