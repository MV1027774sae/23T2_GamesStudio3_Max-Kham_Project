using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnableEnemy;
    [SerializeField] private float spawnRate = 3f;
    [SerializeField] private float spawnRange = 4f;
    public bool canSpawn = false;
    //[SerializeField] private int totalEnemiesSpawned = 0;
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
        int enemiesSpawned = 0;

        while (enemiesSpawned < enemySpawnLimit && canSpawn)
        {
            yield return wait;

            if (canSpawn && this != null)
            {
                int rand = Random.Range(0, spawnableEnemy.Length);
                GameObject enemyToSpawn = spawnableEnemy[rand];

                Instantiate(enemyToSpawn, GetRandomSpawnPosition(), Quaternion.identity);
                enemiesSpawned++;
            }
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




