using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    private UserInput userInput;

    private void Awake()
    {
        userInput = new UserInput();
        userInput.ClickOption.Click.performed += OnMouseClick;
        userInput.Enable();
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
   public void Home()
   {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");        
   }

   public void Resume()
   {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
   }

    private void OnDestroy()
    {
        userInput.ClickOption.Click.performed -= OnMouseClick;
    }

    private void OnMouseClick(InputAction.CallbackContext context)
    {
        if (pauseMenu.activeSelf)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

}
