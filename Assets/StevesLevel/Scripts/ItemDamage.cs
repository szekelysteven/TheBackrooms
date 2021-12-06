using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDamage : MonoBehaviour
{
    public float damage;
    public float timeInterval;
    public bool collideDetected;
    public IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
        coroutine = DamageOverTime();
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnCollisionEnter(Collision collision)
    {
        collideDetected = true;
        GetComponent<AudioSource>().Play();
    }

    public IEnumerator DamageOverTime()
    {

        while (true)
        {
            yield return new WaitForSeconds(timeInterval);
            if (collideDetected == true)
            {
                GameObject.Find("Player").GetComponent<PlayerController>().playerHealth -= damage;
                GameObject.Find("Player").GetComponent<HitEffect>().SimulateHit();
            }
        }



    }
}
