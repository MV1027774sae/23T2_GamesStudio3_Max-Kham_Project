using UnityEngine;

public class RoomController : MonoBehaviour
{
    public GameObject[] enemies; // Reference to the enemies in the room
    public TilemapController tilemapController; // Reference to the TilemapController script

    public EnemySpawner[] enemySpawners; // Array to hold references to the EnemySpawner scripts
    public TrapShooter[] trapShooters; // Array to hold references to the TrapShooter scripts

    private void Start()
    {
        // Disable all enemies initially
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Activate the enemies when the player enters the collider
            foreach (GameObject enemy in enemies)
            {
                enemy.SetActive(true);
            }

            // Start the enemy spawner coroutines
            foreach (EnemySpawner spawner in enemySpawners)
            {
                if (spawner != null)
                {
                    StartCoroutine(spawner.SpawnEnemy());
                }
            }
            foreach (TrapShooter trapShooter in trapShooters)
            {
                if (trapShooter != null)
                {
                    trapShooter.ActivateTrap();
                }
            }
        }
    }
}












