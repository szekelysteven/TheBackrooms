using UnityEngine;

[CreateAssetMenu(fileName = "NewSpeaker", menuName = "Data/New Speaker')")]
[System.Serializable]
public class Cutscene : ScriptableObject
{
    public string speakerName;  //states the name of the speaker along with there text color. 
    public Color textColor;
}
