using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollisionDetection : MonoBehaviour
{
    public Text ScoreText;
    private int scoreCount = 0;
    private HealthManager healthManager;

    public void Start()
    {
        healthManager = FindObjectOfType<HealthManager>();
        if (healthManager == null)
        {
            Debug.LogError("HealthManager not found in the scene!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            Destroy(other.gameObject);
            Debug.Log("Water bottle collected!");
            scoreCount += 1;
            UpdateLevel1Text();
        }
        else if (other.CompareTag("Score10"))
        {
            Destroy(other.gameObject);
            Debug.Log("Item collected!");
            scoreCount += 10;
            UpdateLevel3Text();
        }
        else if (other.CompareTag("Score20"))
        {
            Destroy(other.gameObject);
            Debug.Log("Item collected!");
            scoreCount += 20;
            UpdateLevel3Text();
        }
        else if (other.CompareTag("Score100"))
        {
            Destroy(other.gameObject);
            Debug.Log("Item collected!");
            scoreCount += 100;
            UpdateLevel3Text();
        }
        else if (other.CompareTag("Enemy") || other.CompareTag("Spike"))
        {
            Debug.Log("Player got hurt by " + other.tag);
            healthManager.PlayerisInjured();
        }
    }

    private void UpdateLevel1Text()
    {
        if (ScoreText != null)
        {
            ScoreText.text = ScoreText.text + scoreCount;
        }
        else
        {
            Debug.LogError("Level 1 Text is not assigned in the Inspector!");
        }
    }

    private void UpdateLevel2Text()
    {
        if (ScoreText != null)
        {
            ScoreText.text = ScoreText.text + scoreCount;
        }
        else
        {
            Debug.LogError("Level 2 Text is not assigned in the Inspector!");
        }
    }

    private void UpdateLevel3Text()
    {
        if (ScoreText != null)
        {
            ScoreText.text = ScoreText.text + scoreCount;
        }
        else
        {
            Debug.LogError("Level 3 Text is not assigned in the Inspector!");
        }
    }

    private void UpdateLevel4Text()
    {
        if (ScoreText != null)
        {
            ScoreText.text = ScoreText.text + scoreCount;
        }
        else
        {
            Debug.LogError("Level 4 Text is not assigned in the Inspector!");
        }
    }
}