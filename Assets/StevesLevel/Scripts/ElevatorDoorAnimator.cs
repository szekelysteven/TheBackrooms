using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Trigger code that will open the train doors when the user is present infront. Door can be locked and will check for desired item.
public class ElevatorDoorAnimator : MonoBehaviour
{
    [SerializeField] private Animator elevator = null;
    private bool openTrigger = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (openTrigger)
            {
                elevator.Play("Open", 0, 0.0F);
                openTrigger = false;
            }
            else
            {
                elevator.Play("Close", 0, 0.0F);
                openTrigger = true;
            }
        }
    }
}
