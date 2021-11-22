using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDoor : MonoBehaviour
{

    [SerializeField] bool open = false; //For Saving the Door State

    [SerializeField] float doorOpenAngle = 90f;
    [SerializeField] float doorClosedAngle = 0f;

    [SerializeField] float smooth = 2f; // This will be used to change the speed of the actual rotation of the object

    public void ChangeDoorState()
    {
        open = !open;
    }


    void Update()
    {
        if (open)
        {
            Quaternion targetRotationOpen = Quaternion.Euler(0, doorOpenAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotationOpen, smooth * Time.deltaTime);
        }
        else
        {
            Quaternion targetRotationClosed = Quaternion.Euler(0, doorClosedAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotationClosed, smooth * Time.deltaTime);
        }
    }
}
