//Written using Unity Using Aritifical Intelligence Fourth Edition from Game 445.

//Initially written for enemies on Kevin's level. Enemies should freeze when looked at by player, and slowly chase player after
//not being looked at for a set amount of time. Ideally we will have four enemy types for the game. This is a simple FSM.

//11/7/2021
//added for loops to fill arrays ,made arrays for enemy objects and enemy transforms to handle multiple enemies. now sends messages throughn raycast.

//11/8/2021
//adding navmesh functionality

//12/3/2021
//Merged animation and enemy scripts. States now trigger animations in animators.
//Each enemy now has its own script for custom states as opposed to adding conditional statements to a general enemy script.
//Since there are many enemy scripts, the player will have a public variable for enemy type so it knows what enemy script to send its raycast messages to on each level.

//1/8/2022
//Adding unique rat functionality. The rat will keep track of when in player sight and when the flashlight is on. 
//when both conditions are true the rat will explode by setting the explode script into motion. Rat
//chasing  will also need to be proximity based to know when the player is close enough to start following.

//1/17/2022
//need to triple player vision detection range. rat explosion functionality working. will need to add a timed coroutine to make rat disappear
//shortly after being "exploded". may also open up the random range for where rat can explode to.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RatController : FiniteStateMachineAbstractClass
{
    public Animator animator;
    HitEffect a;

    //added Explode state for rat 
    public enum EnemyState
    {
        None,
        Idle,
        Chase,
        Attack,
        Explode,
    }

    public NavMeshAgent agent;
    public EnemyState currentState;
    private float currentSpeed;
    private float currentRotationSpeed;
    private int health;
    private Rigidbody enemyRigidbody;
    private float distance;
    private float attackTime;
    public float attackRate;
    public bool flashlightOn;
    public bool inPlayerVision;
    public float attackDistance;
    public float proximityDistance;
    
    
    //Starts the state machine
    protected override void Initialize()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        currentState = EnemyState.Idle;
        currentSpeed = 1.0f;
        currentRotationSpeed = 50.0f;
        elapsedTime = 0;
        attackTime = 3;
        attackRate = 3;
        attackDistance = 3.0f;
        
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
            case EnemyState.Explode: UpdateExplodeState(); break;
        }

        //Timer to keep track of run time.
        elapsedTime += Time.deltaTime;
  
    }
    //*****************************************************CHASE STATE**************************************************************
    protected void UpdateChaseState()
    {

        //Transition to explode state if hit by flashlight while in player vision.
        if ((inPlayerVision == true) && (flashlightOn == true))
        {
            currentState = EnemyState.Explode;

        }
        //since rats are proximity based, if distance to player is great than proximity distance, they will go idle.
        //still deciding if i want the rat to disengage as is, or delete this code to have infinite engagement.
        //a happy medium may be to increase distance needed to lose rat by x2.
        if (distance >= (proximityDistance * 2))
        {
            currentState = EnemyState.Idle;
        }
        agent.Resume();
        //Sets animator bool run to true
        animator.SetBool("Run", true);
        Debug.Log(distance);
       

        destinationPosition = playerTransform.position;
        distance = Vector3.Distance(transform.position, playerTransform.position);

        //face player
        Quaternion targetRotation = Quaternion.LookRotation(destinationPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * currentRotationSpeed);

        //move towards player, old way without using nav meshes
        //transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed);
        agent.destination = destinationPosition;


       

        //check distance to player, if close enough transition to attacking
        if (distance <= attackDistance)
        {
           
            attackTime = elapsedTime + 1.0f;
            currentState = EnemyState.Attack;
        }


    }

    //method that sets inPlayerVision to true for use with raycast messaging.
    protected void detected(bool vision)
    {
        inPlayerVision = vision;
    }

    //receives from player controller
    protected void flashlightTurnedOn(bool flashlight)
    {
        flashlightOn = flashlight;
    }




    //********************************************************IDLE STATE**************************************************************
    protected void UpdateIdleState()
    {
        //Transition to explode state if hit by flashlight while in player vision.
        if ((inPlayerVision == true) && (flashlightOn == true))
        {
            currentState = EnemyState.Explode;

        }
        //stops navmesh agent from moving
        agent.Stop();
        //Sets animator bool run to false
        animator.SetBool("Run", false);

        //changed from a vision check for rat proximity functionality
        distance = Vector3.Distance(transform.position, playerTransform.position);

        //this is the distance check for when the rat will engage player. it should not disengage once engaged.
        if (distance <= proximityDistance)
        {
            currentState = EnemyState.Chase;
        }
    }




    //********************************************************ATTACK STATE*************************************************************
    protected void UpdateAttackState()
    {
        //Transition to explode state if hit by flashlight while in player vision.
        if ((inPlayerVision == true) && (flashlightOn == true))
        {
            currentState = EnemyState.Explode;

        }
        agent.Stop();
        animator.SetBool("Run", true);
        distance = Vector3.Distance(transform.position, playerTransform.position);
        

        //reduces players health over time while being touched
        if ((elapsedTime >= attackTime) && (distance <= attackDistance))
        {
            GameObject.Find("Player").GetComponent<PlayerController>().playerHealth -= 20.0f;
            GameObject.Find("Player").GetComponent<HitEffect>().SimulateHit();
            attackTime = elapsedTime + 3.0f;
            GetComponent<AudioSource>().Play();
}


        if (distance > attackDistance)
        {
            currentState = EnemyState.Chase;
        }
        if (distance < attackDistance)
        {
            currentState = EnemyState.Attack;
        }

       


    }

    //********************************************************EXPLODE STATE*************************************************************

    protected void UpdateExplodeState()
    {
        this.GetComponent<NavMeshAgent>().enabled = false;
        this.GetComponent<Explosion>().Explode();

        Debug.Log("Explode!");
    }

}
