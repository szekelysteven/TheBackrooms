using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHolder : MonoBehaviour
{

    private List<Key.KeyType> keyList;




    private void Awake()
    {
        keyList = new List<Key.KeyType>();



    }

    public void AddKey(Key.KeyType keyType)
    {
        Debug.Log("Added Key: " + keyType);

        keyList.Add(keyType);
    }

    public void UseKey(Key.KeyType keyType)
    {

        keyList.Remove(keyType);
    }
    
    public bool ContainsKey(Key.KeyType keyType)
    {
        return keyList.Contains(keyType);
    }

    private void OnTriggerEnter(Collider other)
    {
        Key key = other.GetComponent<Key>();
        if (key != null)
        {
            AddKey(key.GetKeyType());

            Destroy(key.gameObject, 1f);
        }

        KeyDoor keyDoor = other.GetComponent<KeyDoor>();
        if (keyDoor != null)
        {
            if (ContainsKey(keyDoor.GetKeyType()))
            {
                UseKey(keyDoor.GetKeyType());
                keyDoor.OpenDoor();
            }
        }
        KeyDoorUnlock keyDoorUnlock = other.GetComponent<KeyDoorUnlock>();
        if (keyDoorUnlock != null)
        {
            if (ContainsKey(keyDoorUnlock.GetKeyType()))
            {
                UseKey(keyDoorUnlock.GetKeyType());
                keyDoorUnlock.OpenDoor();
            }
        }
    }

}
