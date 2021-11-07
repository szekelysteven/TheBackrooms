//Written using Unity Using Aritifical Intelligence Fourth Edition from Game 445.

//Initially written for enemies on Kevin's level. Enemies should freeze when looked at by player, and slowly chase player after
//not being looked at for a set amount of time. This is a simple FSM that doesnt really make use of Unity's built in FSM capabilities since the
//enemy states are limited.

//11/7/2021
//updated with for loops and arrays to handle multiple enemies.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : FiniteStateMachineAbstractClass
{
    public enum EnemyState
    {
        None,
        Idle,
        Chase,
        Attack,
    }

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
        currentState = EnemyState.Chase;
        currentSpeed = 1.0f;
        currentRotationSpeed = 50.0f;
        elapsedTime = 0;
        attackTime = 0;
        attackRate = 0;
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

    protected void UpdateChaseState()
    {
        //checks distance to player, if close enough attacks.  
        //if player looks at enemy, goes idle.

        destinationPosition = playerTransform.position;
        distance = Vector3.Distance(transform.position, playerTransform.position);

        //face player
        Quaternion targetRotation = Quaternion.LookRotation(destinationPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * currentRotationSpeed);

        //move towards player
        transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed);


        //TO DO: need code from player on wether it can detect enemy or not in raycast. 
        //TO DO: needs to send over command to set inPlayerVision to true.

        //Transition states if seen by player
        if (inPlayerVision == true)
        {
            currentState = EnemyState.Idle;
        }

        //check distance to player, if close enough transition to attacking
        if (distance <= 3.0f)
        {
           
            attackTime = elapsedTime + 3.0f;
            currentState = EnemyState.Attack;
        }


    }

    //method that set inPlayerVision to true for use with raycast messaging.
    protected void detected(bool vision)
    {
        inPlayerVision = vision;
    }
    
        
        
        
        
   protected void UpdateIdleState()
    {
       if (inPlayerVision == false)
       {
         currentState = EnemyState.Chase;
       }
    }

    protected void UpdateAttackState()
    {
        distance = Vector3.Distance(transform.position, playerTransform.position);
        

        //reduces players health over time while being touched
        if (elapsedTime >= attackTime)
        {
            GameObject.Find("Player").GetComponent<PlayerController>().playerHealth -= 5.0f;
            attackTime = elapsedTime + 3.0f;
        }


        if (distance >= 3.0f)
        {
            currentState = EnemyState.Chase;
        }
    //transition to idle when player detects enemy
        if (inPlayerVision == true)
        {
            currentState = EnemyState.Idle;
        }
    }

}
