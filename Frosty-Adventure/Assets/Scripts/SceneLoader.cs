using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // Required for interacting with UI buttons

public class SceneLoader : MonoBehaviour
{
    public string sceneNameToLoad;
    public KeyCode activationKey = KeyCode.UpArrow;  
    public Button myButton; // Reference to the UI Button

    private bool isPlayerOnTile = false;

    void Start()
    {
        // Optionally, add a button listener if the button is assigned
        if (myButton != null)
        {
            myButton.onClick.AddListener(LoadScene);  // Use LoadScene as the method to call when the button is clicked
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerOnTile = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerOnTile = false;
        }
    }

    void Update()
    {
        // Check if the player is on the tile and the specific key is pressed
        if (isPlayerOnTile && Input.GetKeyDown(activationKey))
        {
            LoadScene();
        }
    }

    public void LoadScene()
    {
        Debug.Log("Loading scene: " + sceneNameToLoad);
        SceneManager.LoadScene(sceneNameToLoad);
    }
}