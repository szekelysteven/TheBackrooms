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

    public AudioClip bossRoar;

    AudioSource audioSource;

    private bool stageTwo;
    private bool stageOneisDone = false;

    public GameObject[] pylonChecker;


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
        audioSource = GetComponent<AudioSource>();


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
        pylonChecker = GameObject.FindGameObjectsWithTag("Pylon");
        pylonCount = pylonChecker.Length;

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

        if (pylonCount <= 0 && !stageOneisDone)
        {
            stageTwo = true;
            Debug.Log("The Pylons defending the Bic are gone, starting stage two");
            BossStageTwo();
            stageTwo = false;
            stageOneisDone = true;
        }
        
    }


    private void BossStageTwo()
    {
        Debug.Log("The Boss has entered stage two");
        if (stageTwo == true)
        {
            audioSource.PlayOneShot(bossRoar, 0.7f);
            stageTwoBossCount = 0;
            startTimeBtwShots = 2.5f;
            stageTwoBossCount++;
            Debug.Log("Boss Count timer = " + stageTwoBossCount);
            
            if (stageTwoBossCount == 15)
            {
                Debug.Log("15 seconds has passed within Stage two. Starting end");
            }

        }

    }





}
