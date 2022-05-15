using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoorUnlock : MonoBehaviour
{


    [SerializeField] private Key.KeyType keyType;
    public GameObject replaceDoor;

    public Key.KeyType GetKeyType()
    {
        return keyType;
    }
    public void OpenDoor()
    {

        gameObject.SetActive(false);

        replaceDoor.SetActive(true);
    }

}