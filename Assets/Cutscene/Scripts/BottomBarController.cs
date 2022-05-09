using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BottomBarController : MonoBehaviour
{
    public TextMeshProUGUI barText;
    public TextMeshProUGUI personNameText;

    private int sentenceIndex = -1; 
    private StoryScene currentScene;
    private State state = State.COMPLETED; 


    private enum State
    {
        PLAYING,
        COMPLETED
    }
   
    public void PlayScene(StoryScene scene) // plays the current scene
    {
        currentScene = scene;
        sentenceIndex = -1;
        PlayNextSentence();
    }
    public void PlayNextSentence() //plays the next sentence and displays who is speaking. 
    {
        StartCoroutine(TypeText(currentScene.sentences[++sentenceIndex].text));
        personNameText.text = currentScene.sentences[sentenceIndex].speaker.speakerName;
        personNameText.color = currentScene.sentences[sentenceIndex].speaker.textcolor;
    }

    public bool IsCompleted()
    {
        return state == State.COMPLETED; /// once the scene is completed it will return to a completed state.  
    }

    public bool IsLastSentence()
    {
        return sentenceIndex + 1 == currentScene.sentences.Count;
    }

    private IEnumerator TypeText(string text) // this creates a typing effect on the scene. 
    {
        barText.text = "";
        state = State.PLAYING;
        int wordIndex = 0;

        while(state != State.COMPLETED)
        {
            barText.text += text[wordIndex];
            yield return new WaitForSeconds(0.05f); //scrolls the speed of the sentence by -0.05
            if (++wordIndex == text.Length)
                {
                state = State.COMPLETED;
                break;
            }
        }
    }

}
