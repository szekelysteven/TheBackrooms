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
        if (Input.GetMouseButtonDown(0)) 
        {
            Debug.Log("Mouse Clicked"); ///ensure that the mouse is indeed active and playing
            if(bottomBar.IsCompleted())
            {
                bottomBar.PlayNextSentence(); // once the mouse is clicked this advance into the next screen. 
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //loads the next scene in the build index after the player clicks on the last sentence. 
            }
                    
        }
    }
}
