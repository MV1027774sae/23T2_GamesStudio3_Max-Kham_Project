using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject virtualCam;
    public GameObject enemySpawner;
    private Renderer spawnerRenderer;

    private void Start()
    {
        // Get the renderer component of the enemy spawner
        spawnerRenderer = enemySpawner.GetComponent<Renderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCam.SetActive(true);
            spawnerRenderer.enabled = true; // Enable the renderer to make the spawner visible
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCam.SetActive(false);
            spawnerRenderer.enabled = false; // Disable the renderer to hide the spawner
        }
    }
}




