using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public StoryScene currentScene;
    public BottomBarController bottomBar;

    void Start()
    {
        bottomBar.PlayScene(currentScene);   ///plays the current scene. 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) 
        {
            Debug.Log("Mouse Clicked or Space Bar Pressed"); ///ensure that the mouse is indeed active and playing
            if(bottomBar.IsCompleted())
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                if (bottomBar.IsLastSentence())
                {
                    currentScene = currentScene.nextScene;
                    //bottomBar.PlayNextSentence(); // once the mouse is clicked this advance into the next screen. 
                    bottomBar.PlayScene(currentScene);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //loads the next scene in the build index after the player clicks on the last sentence. 
                }
                else
                {
                    bottomBar.PlayNextSentence();
                }

            }
                    
        }
    }
}
