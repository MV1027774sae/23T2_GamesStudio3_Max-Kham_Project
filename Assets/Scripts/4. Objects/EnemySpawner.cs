using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnableEnemy;
    [SerializeField] private float spawnRate = 3f;
    [SerializeField] private float spawnRange = 4f;
    private bool canSpawn = false;
    [SerializeField] private int totalEnemiesSpawned = 0;
    [SerializeField] private int enemySpawnLimit = 10;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRange);
    }

    private void Start()
    {
        // Start the spawning coroutine when the EnemySpawner is created
        StartCoroutine(SpawnEnemy());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the spawn range.");
            canSpawn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited the spawn range.");
            canSpawn = false;
        }
    }

    public IEnumerator SpawnEnemy()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        int enemiesSpawned = 0;

        while (enemiesSpawned < enemySpawnLimit)
        {
            // Wait until spawning is allowed and the player is inside the spawn range
            yield return new WaitUntil(() => canSpawn);

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
}




