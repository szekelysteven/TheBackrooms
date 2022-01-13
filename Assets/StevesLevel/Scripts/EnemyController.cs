//Written using Unity Using Aritifical Intelligence Fourth Edition from Game 445.

//Initially written for enemies on Kevin's level. Enemies should freeze when looked at by player, and slowly chase player after
//not being looked at for a set amount of time. This is a simple FSM.

//11/7/2021
//added for loops to fill arrays ,made arrays for enemy objects and enemy transforms to handle multiple enemies. now sends messages throughn raycast.

//11/8/2021
//adding navmesh functionality

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : FiniteStateMachineAbstractClass
{
    public enum EnemyState
    {
        None,
        Idle,
        Chase,
        Attack,
    }


    public NavMeshAgent agent;
    public EnemyState currentState;
    private float currentSpeed;
    private float currentRotationSpeed;
    private int health;
    private Rigidbody enemyRigidbody;
    public bool inPlayerVision = false;
    public float distance;
    public float attackTime;
    public float attackRate;
    
    
    //Starts the state machine
    protected override void Initialize()
    {
        agent = GetComponent<NavMeshAgent>();

        currentState = EnemyState.Chase;
        currentSpeed = 1.0f;
        currentRotationSpeed = 50.0f;
        elapsedTime = 0;
        attackTime = 3;
        attackRate = 3;
        inPlayerVision = false;

       
        //this is where patrol state will be set up in the future with waypoints

        //find player
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");

        //get own ridgidbody
        enemyRigidbody = GetComponent<Rigidbody>();
        playerTransform = objPlayer.transform;
    }

    protected override void FSMUpdate()
    {
        switch (currentState)
        {
            case EnemyState.Idle: UpdateIdleState(); break;
            case EnemyState.Chase: UpdateChaseState(); break;
            case EnemyState.Attack: UpdateAttackState(); break;
        }

        //Timer to keep track of run time.
        elapsedTime += Time.deltaTime;
  
    }
    //*****************************************************CHASE STATE**************************************************************
    protected void UpdateChaseState()
    {
        agent.Resume();
        //checks distance to player, if close enough attacks.  
        //if player looks at enemy, goes idle.

        destinationPosition = playerTransform.position;
        distance = Vector3.Distance(transform.position, playerTransform.position);

        //face player
        Quaternion targetRotation = Quaternion.LookRotation(destinationPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * currentRotationSpeed);

        //move towards player
        //transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed);
        agent.destination = destinationPosition;


        //Transition states if seen by player
        if (inPlayerVision == true)
        {
            currentState = EnemyState.Idle;

        }

        //check distance to player, if close enough transition to attacking
        if (distance <= 5.0f)
        {
           
            attackTime = elapsedTime + 1.0f;
            currentState = EnemyState.Attack;
        }


    }

    //method that set inPlayerVision to true for use with raycast messaging.
    protected void detected(bool vision)
    {
        inPlayerVision = vision;
    }
    
        
        
        
    //********************************************************IDLE STATE**************************************************************
   protected void UpdateIdleState()
    {
        agent.Stop();
       if (inPlayerVision == false)
       {
         currentState = EnemyState.Chase;
       }
    }
    //********************************************************ATTACK STATE*************************************************************
    protected void UpdateAttackState()
    {
        agent.Stop();
        distance = Vector3.Distance(transform.position, playerTransform.position);
        

        //reduces players health over time while being touched
        if (elapsedTime >= attackTime)
        {
            GameObject.Find("Player").GetComponent<PlayerController>().playerHealth -= 20.0f;
            attackTime = elapsedTime + 1.0f;
            GetComponent<AudioSource>().Play();
        }


        if (distance > 3.0f)
        {
            currentState = EnemyState.Chase;
        }
        if (distance < 3.0f)
        {
            currentState = EnemyState.Attack;
        }
        //transition to idle when player detects enemy
        if (inPlayerVision == true)
        {
            currentState = EnemyState.Idle;
        }
    }

}
