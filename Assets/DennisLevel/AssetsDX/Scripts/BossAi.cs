using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAi : MonoBehaviour
{
    /*
    public enum Stage
    {
        WaitingToStart,
        Stage_1,
        Stage_2,
        Stage_3,
    }
    */

    //private Stage stage;

    private Transform player;
    [SerializeField] Transform lookAtTarget;

    public GameObject projectile;
    private float timeBtwShots;
    [SerializeField] private float startTimeBtwShots = 5;

    private bool battleStart;

    public GameObject[] pylons;

    public AudioClip bossRoar;

    public AudioSource audioSource;


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

            //stage = Stage.WaitingToStart;
            
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

        if (pylons.Length <= 0)
        {
            StartCoroutine(stage2BossBattle());
            
        }
    }



    IEnumerator stage2BossBattle()
    {
        Debug.Log("all Pylons are destroyed, Boss is getting angry!");
        startTimeBtwShots = 2.5f;
        yield return new WaitForSeconds(15);

        Debug.Log("Stage two has finished at : " + Time.time);


    }



    /* private void BossBattleStages()
    {
        switch (stage)
        {

            case Stage.Stage_1:

                if (pylons.Length <= 0)
                {
                    Debug.Log("Pylons Destroyed!");
                    //all pylons are destroyed move to stage 2
                    StartNextStage();
                }
                break;
            case Stage.Stage_2:

                startTimeBtwShots = 2.5f;
                yield return new WaitForSeconds (10);
                Debug.Log("Boss is getting Angry!");

                break;

            case Stage.Stage_3:

                startTimeBtwShots = 1;
                yield return new WaitForSeconds(7);
                Debug.Log("Boss is at the brink!");

                break;
        }

    }


    private void StartNextStage()
    {
        switch (stage)
        {

            case Stage.WaitingToStart:
                stage = Stage.Stage_1;
                break;
            case Stage.Stage_1:
                stage = Stage.Stage_2; 
                break;
            case Stage.Stage_2:
                stage = Stage.Stage_3;
                break;

        }

        Debug.Log("Starting next stage: " + stage);
    }
    */

}
