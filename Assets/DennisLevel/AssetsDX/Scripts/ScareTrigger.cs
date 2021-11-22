using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareTrigger : MonoBehaviour
{
    public AudioSource jumpScareAudio;
    public AudioClip scareSound;

    private bool hasPlayedAudio;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && hasPlayedAudio == false)
        {
            jumpScareAudio.PlayOneShot(scareSound);
            hasPlayedAudio = true;
        }
    }

}
