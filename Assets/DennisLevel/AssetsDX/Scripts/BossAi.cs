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
    private bool stageThree;

    private bool stageOneisDone = false;
    private bool stageTwoisDone = false;
    private bool stageThreeisDone = false;

    public GameObject[] pylonChecker;

    private bool startTwoTimer;
    private bool startThreeTimer;

    private float stageTwoBossCount;
    private int pylonCount;
    private bool isPylonEmpty;

    private float stageThreeBossCount;

    public GameObject endPortal;

    public GameObject particleInstantiation;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBtwShots = startTimeBtwShots;

        battleStart = false;

        stageTwo = false;
        stageThree = false;
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        startTwoTimer = false;
        startThreeTimer = false;
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

        if (startTwoTimer == true && !stageTwoisDone)
        {
            stageTwoBossCount += Time.deltaTime;
            Debug.Log("Boss Count timer = " + stageTwoBossCount);

            if (stageTwoBossCount >= 17f)
            {
                stageThree = true;
                BossStageThree();
                stageThree = false;

                stageTwoisDone = true;
                Debug.Log("15 seconds has passed within Stage two. Starting end");
            }
        }

        if (startThreeTimer == true && !stageThreeisDone)
        {
            stageThreeBossCount += Time.deltaTime;
            Debug.Log("Stage Three Boss Count Timer = " + stageThreeBossCount);
            //InvokeRepeating("BossRoar", 0, 2); //calls BossRoar every 2 seconds
            
            if (stageThreeBossCount >= 15f)
            {
                ActivateEndPortal();


                stageThreeisDone = true;
                Debug.Log("The Bic is defeated! You Win!");
            }
        }
    }





     private void BossStageTwo()
    {
        Debug.Log("The Boss has entered stage two");
        if (stageTwo == true)
        {
            audioSource.PlayOneShot(bossRoar, 0.7f);
            
            startTimeBtwShots = 2.5f;
            startTwoTimer = true;


        }

    }

     void BossStageThree()
    {
        
        Debug.Log("The boss has entered stage three");
        if (stageThree == true)
        {
            audioSource.PlayOneShot(bossRoar, 1.0f);

            startTimeBtwShots = 1.25f;
            startThreeTimer = true;
        }


    }

    private void ActivateEndPortal()
    {
        endPortal.SetActive(true);
        Debug.Log("The End Portal is now active. You may leave this place now");

        GameObject partic = Instantiate(particleInstantiation, transform.position, transform.rotation);
        GetComponent<ParticleSystem>().Play();

        Destroy(GameObject.FindWithTag("Enemy"));
        Destroy(partic, 3);
    }



}
