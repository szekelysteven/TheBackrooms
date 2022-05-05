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
    [SerializeField] private float startTimeBtwShots = 5;

    private bool battleStart;

    public GameObject[] pylons;

    public AudioClip bossRoar;

    public AudioSource audioSource;

    private bool stageTwo;

    public GameObject[] pylonObjects;

    private int stageTwoBossCount;
    private int pylonCount;
    private bool isPylonEmpty;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        

        timeBtwShots = startTimeBtwShots;

        battleStart = false;

        stageTwo = false;
    }
    private void Start()
    {
        pylonObjects = GameObject.FindGameObjectsWithTag("Pylon");
        pylonCount = pylonObjects.Length;


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

        if (pylonObjects != null)
        {
            stageTwo = true;
            Debug.Log("The Pylons are null, starting stage two");
            BossStageTwo();
        }
        
    }


    private void BossStageTwo()
    {
        Debug.Log("The Boss has entered stage two");
        if (stageTwo == true)
        {
            stageTwoBossCount = 0;
            startTimeBtwShots = 2.5f;
            stageTwoBossCount++;
            
            if (stageTwoBossCount == 15)
            {
                Debug.Log("15 seconds has passed within Stage two. Starting end");
            }

        }

    }





}
