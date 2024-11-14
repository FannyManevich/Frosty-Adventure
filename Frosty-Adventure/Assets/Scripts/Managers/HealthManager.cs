using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public event Action<int> HealthChangedEvent;
    public event Action GameOverEvent;

    public int playerHealth = 3;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Awake()
    {
        UpdateHealthUI();
    }

    public void PlayerisInjured()
    {
        if (playerHealth > 0)
        {
            playerHealth -= 1;
            hearts[playerHealth].sprite = emptyHeart;
            Debug.Log("Player got hurt. Current health: " + playerHealth);
        }

        UpdateHealthUI();
        HealthChangedEvent?.Invoke(playerHealth);

        if (playerHealth <= 0)
        {
            GameOverEvent?.Invoke();
        }
    }

    public void IncreaseHealth()
    {
        if (playerHealth < hearts.Length)
        {
            hearts[playerHealth].sprite = fullHeart;
            playerHealth += 1;
            Debug.Log("Player's health increased. Current health: " + playerHealth);
        }
        else
        {
            Debug.Log("Health is already at maximum!");
        }

        UpdateHealthUI();
        HealthChangedEvent?.Invoke(playerHealth);
    }

    private void UpdateHealthUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = (i < playerHealth) ? fullHeart : emptyHeart;
        }      
    }

}
