using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script goes on the train's floor and ensures the player moves with the train animation by parenting the two.
public class Train : MonoBehaviour
{
    private Animation anim;
    private GameObject MovingTrain;
    // Start is called before the first frame update
    void Start()
    {
        MovingTrain = GameObject.FindGameObjectWithTag("MovingTrain");
        anim = MovingTrain.GetComponent<Animation>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.transform.SetParent(gameObject.transform, true);
            anim.Play("TrainMove");
            
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.transform.parent = null;
        }
    }
}
