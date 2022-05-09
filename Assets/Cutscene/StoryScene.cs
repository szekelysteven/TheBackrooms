using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStoryScene", menuName ="Data/New Story Scene")]
[System.Serializable]
public class StoryScene : ScriptableObject
{
    public List<Sentence> sentences;
    public Sprite background; // this allows the scene to have a background and a list of sentences. 
    public StoryScene nextScene;

    [System.Serializable]
    public struct Sentence
    { // allows the scene to displays sentences along with who is speaking.


        public string text; 
        public Speaker speaker;
    }
}
 