using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnableEnemy;

    [SerializeField] private float spawnRate;
    [SerializeField] private float spawnRange = 4;

    void Start()
    {
        StartCoroutine(SpawnEnemy(spawnRate, spawnableEnemy));
    }

    private IEnumerator SpawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(gameObject.transform.position.x + Random.Range(-spawnRange, spawnRange), gameObject.transform.position.y + Random.Range(-spawnRange, spawnRange), 0), Quaternion.identity);
        StartCoroutine(SpawnEnemy(interval, enemy));
    }
}
