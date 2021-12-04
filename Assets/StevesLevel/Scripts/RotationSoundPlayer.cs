using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Will play one click every time the turnstile has turned 1/3 of the way to represent each bar.
//Values returned range from .5 to -.5, so the third points will be the two extremes and 0.
public class RotationSoundPlayer : MonoBehaviour
{
    private AudioSource source;
    private bool soundPlayed;
    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(this.transform.rotation.z);


        if (this.transform.rotation.z >= -.1 && this.transform.rotation.z <= .1)
        {
            if (soundPlayed == false)
            {
                source.PlayOneShot(source.clip);
                soundPlayed = true;
            }
        }

        //resets sound played between .2 and .4
        if (this.transform.rotation.z >= .2 && this.transform.rotation.z <= .4)
        {
            soundPlayed = false;
        }

        //resets sound played between .2 and .4
        if (this.transform.rotation.z >= -.4 && this.transform.rotation.z <= -.2)
        {
            soundPlayed = false;
        }

        //plays sound whenever greater than .4
        if (this.transform.rotation.z >= .4 && this.transform.rotation.z <= .5)
        {
            if (soundPlayed == false)
            {
                source.PlayOneShot(source.clip);
                soundPlayed = true;
            }
        }
        //plays sound whenever less than than -.4
        if (this.transform.rotation.z <= -.4 && this.transform.rotation.z >= -.5)
        {
            if (soundPlayed == false)
            {
                source.PlayOneShot(source.clip);
                soundPlayed = true;
            }
        }


        if (this.transform.rotation.z > .5 || this.transform.rotation.z < -.5)
        {
            soundPlayed = false;
        }

    }
}
