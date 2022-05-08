using UnityEngine;

[CreateAssetMenu(fileName = "NewSpeaker", menuName = "Data/New Speaker")] // This script is used to name speakers for the time. 
[System.Serializable]

public class Speaker : ScriptableObject
{
    public string speakerName;
    public Color textcolor; // This highlights each speakers by color.
}