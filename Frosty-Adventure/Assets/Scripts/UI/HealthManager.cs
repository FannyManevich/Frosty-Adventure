// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class HealthManager : MonoBehaviour
// {
//    public int health = 3;
//    public Image[] hearts;
//    public Sprite fullHeart;
//    public Sprite emptyHeart;

//    void Update()
//    {
//     foreach (Image img in hearts)
//     {
//         img.sprite = emptyHeart;
//     }
//     for (int i = 0; i < health ; i++)
//     {
//         hearts[i].sprite = fullHeart;
//     }
//    }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int health = 3;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    // Reference to UIManager to signal game over
    private UIManager uiManager;

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        if (uiManager == null)
        {
            Debug.LogError("UIManager not found in the scene!");
        }
        UpdateHealthUI();
    }

    // Method to reduce health
    public void TakeDamage()
    {
        health -= 1;
        Debug.Log("Player took damage. Current health: " + health);
        UpdateHealthUI();

        if (health <= 0)
        {
            health = 0;
            GameOver();
        }
    }

    // Update the heart UI based on current health
    private void UpdateHealthUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            // Optionally, disable heart images that are not needed
            hearts[i].enabled = i < hearts.Length;
        }
    }

    // Handle game over condition
    private void GameOver()
    {
        Debug.Log("Game Over!");
        if (uiManager != null)
        {
            UIManager.isGameOver = true;
        }
    }

    void Update()
    {
        // Optional: You can remove the Update method if it's not needed anymore
    }
}
