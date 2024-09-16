using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  

public class SceneLoader : MonoBehaviour
{
    public string sceneNameToLoad;
    public KeyCode activationKey = KeyCode.UpArrow;  
    public Button myButton;

    private bool isPlayerOnTile = false;

    void Start()
    {
        
        if (myButton != null)
        {
            myButton.onClick.AddListener(LoadScene);  
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