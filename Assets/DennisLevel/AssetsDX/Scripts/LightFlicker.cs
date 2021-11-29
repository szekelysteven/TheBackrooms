using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Light _Light;

    [SerializeField] float MinTime;
    [SerializeField] float MaxTime;
    [SerializeField] float Timer;

    [SerializeField] AudioSource Source;
    [SerializeField] AudioClip StaticClip;


    private void Start()
    {
        Timer = Random.Range(MinTime, MaxTime);
    }

    private void Update()
    {
        FlickeringLight();
    }

    void FlickeringLight()
    {
        if (Timer > 0)Timer -= Time.deltaTime;

        if (Timer <= 0)
        {
            _Light.enabled = !_Light.enabled;
            Timer = Random.Range(MinTime, MaxTime);
            Source.PlayOneShot(StaticClip);
        }
    }
}
