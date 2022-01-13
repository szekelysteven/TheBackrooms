using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//1/12/2022
//Updating the way this script deals with enemies. instead of individually drag and dropping references in inspector to create an array, the script will now
//automatically search and assign all enemies in scene to the enemyarray using "GameObject.FindGameObjectsWithTag".

public class PlayerController : MonoBehaviour
{
    //To Do: Add array of enemies.

    public enum EnemyType
    {
        Mannequin,
        Rat,
        Type3,
        Type4,
    }

    public EnemyType enemyType;
    public string thisSceneName;
    private Rigidbody rb;
    private float velocity;
    public float speed;
    public float playerHealth = 100.0f;
    //detection rate is how fast the raycast fires off, slower saves processing power.
    public float detectionRate = .5f;
    private float detectionTime;
    private float elapsedTime;
    private float enemyLastSeenTime;
    public int FieldOfView = 30;
    public int ViewDistance = 5;
    public Transform[] allEnemyTransforms;
    public GameObject[] allEnemyObjects;
    public Vector3[] rayDirection;
    public bool flashlightTurnedOn;

    private Transform playerTransform;
    
    
    //1/12/2022
    //Function that will handle automatically setting and updating all array lengths based on what findgamesobjectswithtag finds.
    public void UpdateArray()
    {
        //sets all enemy objects in scene into array
        allEnemyObjects = GameObject.FindGameObjectsWithTag("Enemy");

        //set length of allEnemyTransforms equal to objects.
        allEnemyTransforms = new Transform[allEnemyObjects.Length];

        //set length of rays array equal to objects array, top code didnt work.
        //rayDirection = new Transform[allEnemyObjects.Length];
        rayDirection = new Vector3[allEnemyObjects.Length];

        //for loop to load transform array based on enemy array length and values.
        for (int i = 0; i < allEnemyObjects.Length; i++)
        {
            allEnemyTransforms[i] = allEnemyObjects[i].GetComponent<Transform>();
        }




    }

    // Start is called before the first frame update
    public void Start()
    {
        
        playerTransform = this.transform;
        elapsedTime = 0;
        rb = gameObject.GetComponent<Rigidbody>();
        //Debug.Log(allEnemyTransforms.Length);
        //rayDirection.Length = allEnemyTransforms.Length;
        UpdateArray();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    
        Controls();
        HealthUpdate();
        Timer();
        FlashlightCheck();
    }

    public void Controls()
    {
        Vector3 dir = new Vector3(0, 0, 0);

        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");

        transform.Translate(dir * speed * Time.deltaTime);
    }

    public void HealthUpdate()
    {
        //Debug.Log(playerHealth);
        if (playerHealth <= 0)
        {
            Dead();
        }
    }

    public void Timer()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= detectionTime)
        {

            SightUpdate();
            detectionTime = elapsedTime + detectionRate;
        }
    }

    //only rat enemy so far needs to know if player flash light is on or off.
    public void FlashlightCheck()
    {
        //tells all enemy rat objects individually that player has their flashlight on.
        if (enemyType == EnemyType.Rat)
        {
            if (flashlightTurnedOn == true)
            {
                for (int i = 0; i < allEnemyObjects.Length; i++)
                {
                    allEnemyObjects[i].GetComponent<RatController>().flashlightOn = true;
                }
            }
            if (flashlightTurnedOn == false)
            {
                for (int i = 0; i < allEnemyObjects.Length; i++)
                {
                    allEnemyObjects[i].GetComponent<RatController>().flashlightOn = false;
                }
            }
        }

    }

    public void Dead()
    { 
        SceneManager.LoadScene(thisSceneName);
    }

    
    public void SightUpdate()
    {
        //All enemies are reset to out of player vision after time has elapsed from last seen.
        if (enemyType == EnemyType.Mannequin)
        {
            //this code checks if the player has seen any mannequin in the last 3 seconds. if 3 seconds passes and the player
            //hasnt looked at a mannequin, they will all resume movement.
            if ((enemyLastSeenTime + 3.0f) <= elapsedTime)
            {
                for (int i = 0; i < allEnemyObjects.Length; i++)
                {
                    allEnemyObjects[i].GetComponent<EnemyController>().inPlayerVision = false;
                }
            }
        }

        if (enemyType == EnemyType.Rat)
        {
            //this code checks if the player has seen any rat in the last .1 seconds. if .1 seconds passes and the player
            //hasnt looked at a rat, the vision check will fail.
            if ((enemyLastSeenTime + .1f) <= elapsedTime)
            {
                for (int i = 0; i < allEnemyObjects.Length; i++)
                {
                    allEnemyObjects[i].GetComponent<RatController>().inPlayerVision = false;
                }
            }
        }




        RaycastHit hit;

     
            //for loop added to cycle through each enemy in enemy Array's ray direction. this pulls each
            //enemy out of the enemy array to check if its in field of view and distance of view. if
            //both conditions are true, the enemy is in view and will be told thats it detected.
            for (int i = 0; i < allEnemyTransforms.Length; i++)
        {
            {

                rayDirection[i] = allEnemyTransforms[i].position - transform.position;
            }

            if ((Vector3.Angle(rayDirection[i], transform.forward)) < FieldOfView)
            {

                //detect if enemy is within field of view
                if (Physics.Raycast(playerTransform.position, rayDirection[i], out hit, ViewDistance))
                {

                    if (hit.collider.tag == "Enemy")
                    {
                        enemyLastSeenTime = elapsedTime;
                        //Debug.Log(hit.collider.tag);
                        //Debug.Log("EnemyDetected");
                        //this code sets in vision to true in the enemy script.
                        hit.collider.SendMessage("detected", true);


                    }
                  
                }

            }
        }
    }

    //draw the rays with gizmo so we can view in editor

    public void OnDrawGizmos()
    {
        if (!Application.isEditor || playerTransform == null) return;
        for (int i = 0; i < allEnemyTransforms.Length; i++)
        {
            Debug.DrawLine(playerTransform.position, allEnemyTransforms[i].position, Color.red);
        }

        Vector3 frontRayPoint = playerTransform.position + (transform.forward * ViewDistance);

        Vector3 leftRayPoint = Quaternion.Euler(0, FieldOfView * 0.5f, 0) * frontRayPoint;
        Vector3 rightRayPoint = Quaternion.Euler(0, - FieldOfView * 0.5f, 0) * frontRayPoint;

        Debug.DrawLine(transform.position, frontRayPoint, Color.green);
        Debug.DrawLine(transform.position, leftRayPoint, Color.green);
        Debug.DrawLine(transform.position, rightRayPoint, Color.green);

    }
}
