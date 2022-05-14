using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BicController : MonoBehaviour
{
    Animator _bicAnim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            _bicAnim.SetBool("isMoving", true);
        }
        else
        {
            _bicAnim.SetBool("isMoving", false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _bicAnim = this.transform.parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
