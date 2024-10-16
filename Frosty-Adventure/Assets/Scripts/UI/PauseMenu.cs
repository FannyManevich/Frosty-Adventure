using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    private void Awake()
    {
       
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

    }


}
