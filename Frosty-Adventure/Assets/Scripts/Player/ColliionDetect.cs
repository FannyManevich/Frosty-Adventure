using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionDetection : MonoBehaviour
{
    public Text WaterText;
    private int countBottles;
    public Transform leftWall;
    public Transform rightWall;
    public Transform bottomWall;
    public Transform topWall;

    public void Start()
    {
        countBottles = 0;
        UpdateThirstText();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectible"))
        {
            Debug.Log("Water bottle collected!");
            countBottles += 1;

            UpdateThirstText();

            Destroy(other.gameObject);
        }
    }

    private void UpdateThirstText()
    {
        Debug.Log("Updating water counter: " + countBottles);
        if (WaterText != null)
        {
            WaterText.text = "Water collected : " + countBottles;
        }
        else
        {
            Debug.LogError("WaterCollected Text is not assigned in the Inspector!");
        }
    }
}
