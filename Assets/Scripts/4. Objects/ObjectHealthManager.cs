using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHealthManager : MonoBehaviour
{
    [SerializeField] private int maxHealth = 6;
    public int health = 0;

    void Start()
    {
        health = maxHealth;
    }

    public void DamageObject(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            DestroyObject();
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
