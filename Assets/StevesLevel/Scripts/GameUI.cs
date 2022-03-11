using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameUI : MonoBehaviour
{
    public Text Battery_UIText;

    void Start()
    {

    }
    void Update()
    {
        //finds gameobject named flashlight, pulls flashlight script, targets battery life variable, and turns float into string.
        Battery_UIText.text = "Power: " + GameObject.Find("Flashlight").GetComponent<Flashlight>().batteryLife.ToString() +"%";


    }

}
