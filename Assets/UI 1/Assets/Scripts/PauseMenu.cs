using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GameIsPaused; /// We want it to be accesible on other scripts and we want to check to see if it is paused
    public GameObject pauseMenuUI;


    private void Start()
    {
        GameIsPaused = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            {
                if (GameIsPaused)
                {
                    Resume();
                }else
                {
                    Pause();
                }
            }
        }
    }
    public void Resume () 
    
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }    

    public void LoadMenu ()
    {
        Debug.Log("Loading Menu");
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }
    
    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

}
