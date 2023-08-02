using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private KeyCollector keyCollector;

    private void Start()
    {
        keyCollector = FindObjectOfType<KeyCollector>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Key has been collected
            keyCollector.KeyCollected();
            Destroy(gameObject); // Remove the key from the scene
        }
    }
}




