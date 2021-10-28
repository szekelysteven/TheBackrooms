using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterExit : MonoBehaviour
{
    [SerializeField] GameObject player = null;
    [SerializeField] GameObject spot = null;

    [SerializeField] KeyCode enterExitKey = KeyCode.E;



    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetKeyDown(enterExitKey))
        {
            GetOutOfSpot();
        }
    }


    void GetOutOfSpot()
    {
        player.SetActive(true);

        player.transform.position = spot.transform.position + spot.transform.TransformDirection(Vector3.left);
    }
}
