//Written using Unity Using Aritifical Intelligence Fourth Edition from Game 445.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This abstract class defines the methods that enemyControllers will pull and use.
public class FiniteStateMachineAbstractClass : MonoBehaviour
{
    protected Transform playerTransform;
    protected Vector3 destinationPosition;
    protected GameObject[] waypoints;
    protected float elapsedTime;

    protected virtual void Initialize() { }
    protected virtual void FSMUpdate() { }
    protected virtual void FSMFixedUpdate() { }

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        FSMUpdate();
    }

    void FixedUpdate()
    {
        FSMFixedUpdate();
    }
}

