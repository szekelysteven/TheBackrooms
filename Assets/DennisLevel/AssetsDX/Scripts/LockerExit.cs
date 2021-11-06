using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerExit : MonoBehaviour
{
    [SerializeField] GameObject LockerCam;
    [SerializeField] GameObject ExitTrigger;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Locker;
    [SerializeField] GameObject ExitPlace;


    private void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            Player.SetActive(true);
            Player.transform.position = ExitPlace.transform.position;
            LockerCam.SetActive(false);
            Debug.Log("You have left the locker");
            ExitTrigger.SetActive(false);
        }
    }


}

