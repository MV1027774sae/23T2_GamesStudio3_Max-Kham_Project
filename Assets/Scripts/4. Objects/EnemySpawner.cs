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

    private IEnumerator SpawnEnemy()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (canSpawn && totalEnemiesSpawned <= enemySpawnLimit)
        {
            yield return wait;

            if (IsPlayerInsideSpawnRange())
            {
                int rand = Random.Range(0, spawnableEnemy.Length);
                GameObject enemyToSpawn = spawnableEnemy[rand];
                Vector3 spawnPosition = transform.position;
                Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
                totalEnemiesSpawned++;
            }
        }
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

