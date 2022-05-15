using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script will control the battery gameobject, destroy it when the player collides with it, and give the flashlight object a set amount of battery power
public class Battery : MonoBehaviour
{
    public GameObject battery;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.Find("Flashlight").GetComponent<Flashlight>().batteryLife += 15;
           
            //plays sounds attached to battery
            GetComponent<AudioSource>().Play();

            //destory battery
            Destroy(battery, 2f);
        }
    }
}
