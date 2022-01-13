using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//1/8/2022
//Added code to tell rat enemy script when flashlight is on or off.

public class Flashlight : MonoBehaviour
{
    public Light flashlightLight;
    public bool powered;
    public float batteryLife;
    public IEnumerator coroutine;
    public bool flashlightTurnedOn;

    // Start is called before the first frame update
    public void Start()
    {
        coroutine = BatteryDrain();
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    public void Update()
    {
        KeyDetect();
        PowerChecker();
    }

    void KeyDetect()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (batteryLife > 0)
            {
                
                //acts as a switch by setting inverse.
                flashlightLight.enabled = !flashlightLight.enabled;
                powered = !powered;
                //tells the player controller that the flashlight has been turned off.
                GameObject.Find("Player").GetComponent<PlayerController>().flashlightTurnedOn = powered;
            }
            if (batteryLife <= 0)
            {
                GameObject.Find("Player").GetComponent<PlayerController>().flashlightTurnedOn = false;
                powered = false;
                flashlightLight.enabled = false;
            }
        }


    }


    void PowerChecker()
    {
        if (batteryLife <= 0)
        {
            powered = false;
            flashlightLight.enabled = false;
        }
    }

    //need to set batterydrain as a couroutine so that waitforseconds can be used by unity.
    public IEnumerator BatteryDrain()
    {
        

        while (true)
            {
               yield return new WaitForSeconds(1F);
                if (powered == true)
            {
                    batteryLife -= 1;
                    Debug.Log(batteryLife);
                }
            }
           
            
    }

}
