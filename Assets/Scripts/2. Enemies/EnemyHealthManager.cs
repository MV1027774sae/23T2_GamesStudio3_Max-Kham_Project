using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    [SerializeField] private int maxHealth = 1;
    public int health = 1;

    void Start()
    {
        health = maxHealth;
    }

    public void DamageEnemy(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            DestroyEnemy();
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
