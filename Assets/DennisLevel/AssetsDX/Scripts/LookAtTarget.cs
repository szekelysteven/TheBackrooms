using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public Transform lookAtTarget;


    // Update is called once per frame
    void Update()
    {
        transform.LookAt(lookAtTarget);
    }
}
