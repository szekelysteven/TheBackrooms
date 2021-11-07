using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerEnter : MonoBehaviour
{
    [SerializeField] GameObject LockerCam;
    [SerializeField] GameObject ExitTrigger;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Locker;
    private int TriggerCheck;

    /* For getting this code to work you will need a Exit Trigger. Create an empty and name it exit trigger and reset its transform. 
     * Then deactivate the object and assign the locker exit script to it. Place the objects into the public fields and you are good to go. */

    private void Start()
    {
        LockerCam.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        TriggerCheck = 1;
        Debug.Log("You are in the Trigger Zone");
    }
    private void OnTriggerExit(Collider other)
    {
        TriggerCheck = 0;
        Debug.Log("You are now leaving the Trigger Zone");

    }

    private void Update()
    {
        if (TriggerCheck == 1)
        {
            if (Input.GetKeyDown("e"))
            {
                LockerCam.SetActive(true);
                Player.SetActive(false);
                ExitTrigger.SetActive(true);
                TriggerCheck = 0;
            }
            
        }

    }
}
