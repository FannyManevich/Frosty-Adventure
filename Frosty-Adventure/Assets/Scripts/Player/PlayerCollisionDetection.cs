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
            Debug.LogError("HealthManager not found in the scene!");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with: " + other.gameObject.name);

        if (other.CompareTag("Collectible"))
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
                Debug.LogError("CollectibleScore component not found on the collectible object");
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            if (GetComponent<Rigidbody2D>().velocity.y < 0)
            {
                Debug.Log("Player jumped on enemy: " + other.gameObject.name);
                Destroy(other.gameObject);
            }
            else
            {
                Debug.Log("Player got hurt by " + other.tag);
                UImanager.PlayerisInjured();
            }
        }
        else if (other.CompareTag("Spike"))
        {
            Debug.Log("Player got hurt by spike: " + other.gameObject.name);
            UImanager.PlayerisInjured();
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