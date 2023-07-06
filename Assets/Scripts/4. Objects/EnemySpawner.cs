using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnableEnemy;
    [SerializeField] private float spawnRate = 3f;
    [SerializeField] private float spawnRange = 4f;
    private bool canSpawn = true;
    [SerializeField] private int totalEnemiesSpawned = 0;
    [SerializeField] private int enemySpawnLimit = 10;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRange);
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    public IEnumerator SpawnEnemy()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        int enemiesSpawned = 0; // Track the number of enemies spawned

        while (canSpawn && enemiesSpawned < enemySpawnLimit)
        {
            yield return wait;

            int rand = Random.Range(0, spawnableEnemy.Length);
            GameObject enemyToSpawn = spawnableEnemy[rand];

            Instantiate(enemyToSpawn, GetRandomSpawnPosition(), Quaternion.identity);
            enemiesSpawned++;
        }
    }
    private Vector3 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(-spawnRange, spawnRange);
        float randomY = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPosition = transform.position + new Vector3(randomX, randomY, 0);
        return spawnPosition;
    }

    private bool IsPlayerInsideSpawnRange()
    {
        // Get the player GameObject or tag from your game
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Vector3 playerPosition = player.transform.position;
            Vector3 spawnerPosition = transform.position;
            float distance = Vector3.Distance(playerPosition, spawnerPosition);

            return distance <= spawnRange;
        }

        return false;
    }
}

