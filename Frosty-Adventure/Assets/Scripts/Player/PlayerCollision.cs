
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] private Text WaterCollected;
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
            Debug.Log("Water bottle collected!");
            countBottles += 1;

            UpdateThirstText();

            Destroy(other.gameObject);
        }
    }

    private void UpdateThirstText()
    {
        Debug.Log("Updating water counter: " + countBottles);
        if (WaterCollected != null)
        {
            WaterCollected.text = "Water collected : " + countBottles;
        }
        else
        {
            Debug.LogError("WaterCollected Text is not assigned in the Inspector!");
        }
    }

 //   private void OnCollisionEnter2D(Collision2D collision)
  //  {
      
      //   if (collision.transform.tag == "Enemy")
         //{
          // HealthManager.health--;
           // if (HealthManager.health <= 0)
         //   {
               // UIManager.isGameOver = true;
            //    gameObject.SetActive(false);
          //  }
           // else
          //  {
             //  StartCoroutine(GetHurt());
           // }
      //   }
   // }


}
