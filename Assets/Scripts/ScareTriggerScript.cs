using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareTriggerScript : MonoBehaviour
{
    public AudioSource jumpscareAudio;
    public AudioClip CreepyNoise;

    private bool hasPlayedSound;
    // a true or false for whether or not the sound has played

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && hasPlayedSound == false)
        {
            jumpscareAudio.PlayOneShot(CreepyNoise);
            hasPlayedSound = true;
        }
    }



}
