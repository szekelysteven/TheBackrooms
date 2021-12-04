 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script will be attached to the rat enemy. For now it will animate idle when the model is still, and running whenever the model is moving.
public class RatAnimator : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        //get animator
        animator = GetComponent<Animator>();
        animator.SetBool("Run", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
