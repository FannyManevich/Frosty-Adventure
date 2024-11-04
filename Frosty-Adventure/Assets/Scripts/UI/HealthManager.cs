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
    //private UIManager UImanager;

    void Start()
    {
        //UImanager = FindObjectOfType<UIManager>();

        //if (UImanager == null)
      //  {
        //    Debug.LogError("UIManager not found in the scene!");
       // }

      //  UpdateHealthUI();
    }

    public void PlayerisInjured()
    {
        if (health > 0) { 
            health -= 1;
            hearts[health].sprite = emptyHeart;
            Debug.Log("Player got hurt. Current health: " + health);
        }       
    
        if (health == 0)
        {
            GameOver();
        }    
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = (i < health) ? fullHeart : emptyHeart;
        }
       
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
       // UIManager.isGameOver = true;
        gameObject.SetActive(false);
    }
}
