using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float lifeTime = 5f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerStatManager>().DamagePlayer(damage);
            DestroySelf();
        }

        if (collision.tag == "Simple Collider" || collision.tag == "DestroyableObject")
        {
            DestroySelf();
        }
        else return;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
