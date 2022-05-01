using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAi : MonoBehaviour
{

    private Transform player;
    [SerializeField] Transform lookAtTarget;

    public GameObject projectile;
    private float timeBtwShots;
    public float startTimeBtwShots;

    private bool battleStart;

    public GameObject[] pylons;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBtwShots = startTimeBtwShots;

        battleStart = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            battleStart = true;
            Debug.Log("You have entered the Boss Battle Arena!");
        }
    }


    void Update()
    {
        transform.LookAt(lookAtTarget);

        if (timeBtwShots <= 0 && battleStart == true)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            //Quaternion.identity cancels rotation of the object
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;

        }
    }


}
