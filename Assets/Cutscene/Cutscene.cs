using UnityEngine;

[CreateAssetMenu(fileName = "NewSpeaker", menuName = "Data/New Speaker')")]
[System.Serializable]
public class Cutscene : ScriptableObject
{
    public string speakerName;
    public Color textColor;
}
