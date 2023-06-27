using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnableEnemy;

    [SerializeField] private float spawnRate = 3f;
    [SerializeField] private float spawnRange = 4;
    private bool canSpawn = true;
    [SerializeField] private int totalEnemiesSpawned = 0;
    [SerializeField] private int enemySpawnLimit = 10;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        while (canSpawn && totalEnemiesSpawned <= enemySpawnLimit)
        {
            yield return wait;
            int rand = Random.Range(0, spawnableEnemy.Length);
            GameObject enemyToSpawn = spawnableEnemy[rand];

            Instantiate(enemyToSpawn, new Vector3(gameObject.transform.position.x + Random.Range(-spawnRange, spawnRange), gameObject.transform.position.y + Random.Range(-spawnRange, spawnRange), 0), Quaternion.identity);
            totalEnemiesSpawned++;
        }
    }
}
