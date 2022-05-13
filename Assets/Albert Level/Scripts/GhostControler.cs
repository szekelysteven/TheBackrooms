using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostControler : MonoBehaviour
{
    Animator _ghostAnim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            _ghostAnim.SetBool("isMoving", true);
        }
        else
        {
            _ghostAnim.SetBool("isMoving", false);
        }
    }
  
    // Start is called before the first frame update
    void Start()
    {
        _ghostAnim = this.transform.parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
