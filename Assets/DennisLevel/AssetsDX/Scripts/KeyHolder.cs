using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHolder : MonoBehaviour
{

    private List<Key.KeyType> keyList;

    public AudioSource audioSource;
    public AudioClip keyPickupClip;
    public AudioClip keyUsedClip;


    private void Awake()
    {
        keyList = new List<Key.KeyType>();

        audioSource = GetComponent<AudioSource>();

    }

    public void AddKey(Key.KeyType keyType)
    {
        Debug.Log("Added Key: " + keyType);
        audioSource.PlayOneShot(keyPickupClip, 0.7f);
        keyList.Add(keyType);
    }

    public void UseKey(Key.KeyType keyType)
    {
        audioSource.PlayOneShot(keyUsedClip, 0.7f);
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
    }

}
