using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //To Do: Add array of enemies.

   
    private Rigidbody rb;
    private float velocity;
    public float speed;
    public float playerHealth = 100.0f;
    //detection rate is how fast the raycast fires off, slower saves processing power.
    public float detectionRate = .5f;
    private float detectionTime;
    private float elapsedTime;
    private float enemyLastSeenTime;
    public int FieldOfView = 90;
    public int ViewDistance = 100;
    public Transform[] allEnemyTransforms;
    public GameObject[] allEnemyObjects;

    private Transform playerTransform;
    public Vector3[] rayDirection;


    // Start is called before the first frame update
    public void Start()
    {
        
        playerTransform = this.transform;
        elapsedTime = 0;
        rb = gameObject.GetComponent<Rigidbody>();
        //Debug.Log(allEnemyTransforms.Length);
        //rayDirection.Length = allEnemyTransforms.Length;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    
        Controls();
        HealthUpdate();
        Timer();
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

    public void Dead()
    {
        SceneManager.LoadScene("Grace&Mercy2");
    }

    public void SightUpdate()
    {

        if ((enemyLastSeenTime + 3.0f) <= elapsedTime)
        {
            for (int i = 0; i < allEnemyObjects.Length; i++)
            {
                allEnemyObjects[i].GetComponent<EnemyController>().inPlayerVision = false;
            }
           
        }
        
        RaycastHit hit;


        //for loop added to cycle through each enemy in enemy Array's ray direction.
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
