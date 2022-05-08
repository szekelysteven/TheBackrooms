using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour
{

    [SerializeField] float speed;

    private Transform player;
    private Vector3 target;

    [SerializeField] ParticleSystem explosionParticle;

    public GameObject particleInstantiation;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector3(player.position.x, player.position.y, player.position.z);
        //this is to register the players position
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        //this is used to move the projectile towards the player model.

        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            
            DestroyProjectile();
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pylon")
        {
            
            Destroy(collision.gameObject);
            DestroyProjectile();
        }
        if (collision.gameObject.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerController>().playerHealth -= 50.0f;
            GameObject.Find("Player").GetComponent<HitEffect>().SimulateHit();

            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        GameObject partic = Instantiate(particleInstantiation, transform.position, transform.rotation);
        GetComponent<ParticleSystem>().Play();
        Destroy(gameObject);
        Destroy(partic, 3);
    }
}
