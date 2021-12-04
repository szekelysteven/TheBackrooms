using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnouncementsPlayer : MonoBehaviour
{
    public AudioClip[] audioClipArray;
    private AudioSource source;
    public float playInterval;
    private float elapsedTime;
    private int i;

    private
    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        //Keeps track of game time and plays a message in audioclip array at desired increments.
        
        
        
        if (elapsedTime >= playInterval)
        {
            elapsedTime = 0f;
            source.clip = audioClipArray[i];
            source.PlayOneShot(source.clip);
            i++;
            if (i >= audioClipArray.Length)
            {
                i = 0;
            }
        }


    }
}
