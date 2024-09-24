using UnityEngine;

public class Bottle : MonoBehaviour
{
    public delegate void BottleCollectedEventHandler();
    public static event BottleCollectedEventHandler OnBottleCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Trigger the event when the bottle is collected
            if (OnBottleCollected != null)
            {
                OnBottleCollected.Invoke();
            }

            // Destroy or disable this bottle object
            Destroy(gameObject);
        }
    }
}
