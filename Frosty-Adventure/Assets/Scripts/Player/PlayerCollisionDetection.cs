using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollisionDetection : MonoBehaviour
{
    public Text WaterText;
    private int countBottles;

    public void Start()
    {
        countBottles = 0;
        UpdateThirstText();
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
