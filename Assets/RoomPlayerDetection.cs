using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RoomPlayerDetection : MonoBehaviour
{
    private float overlapBoxSizeX;
    private float overlapBoxSizeY;
    [SerializeField] private LayerMask enemyLayer;
    void Start()
    {
        overlapBoxSizeX = transform.localScale.x;
        overlapBoxSizeY = transform.localScale.y;
        DisableEnemies();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            EnableEnemies();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            DisableEnemies();
        }
    }

    private void EnableEnemies()
    {
        Collider2D[] enemiesFound = Physics2D.OverlapBoxAll(transform.position, new Vector2(overlapBoxSizeX, overlapBoxSizeY), 0, enemyLayer);

        foreach (Collider2D enemy in enemiesFound)
        {
            if (enemy.gameObject.GetComponent<AIPath>() != null)
            {
                enemy.gameObject.GetComponent<AIPath>().enabled = true;
            }
            if (enemy.gameObject.GetComponent<EnemySpawner>() != null)
            {
                enemy.gameObject.GetComponent<EnemySpawner>().enabled = true;
                enemy.gameObject.GetComponent<EnemySpawner>().canSpawn = true;
                StartCoroutine(enemy.gameObject.GetComponent<EnemySpawner>().SpawnEnemy());
            }       
        }
    }

    private void DisableEnemies()
    {
        Collider2D[] enemiesFound = Physics2D.OverlapBoxAll(transform.position, new Vector2(overlapBoxSizeX, overlapBoxSizeY), 0, enemyLayer);

        foreach(Collider2D enemy in enemiesFound)
        {
            if (enemy.gameObject.GetComponent<AIPath>() != null) 
            { 
                enemy.gameObject.GetComponent<AIPath>().enabled = false;
            } 
            if (enemy.gameObject.GetComponent<EnemySpawner>() != null)
            {
                enemy.gameObject.GetComponent<EnemySpawner>().canSpawn = false;
                enemy.gameObject.GetComponent<EnemySpawner>().enabled = false;
            }
        }
    }
}
