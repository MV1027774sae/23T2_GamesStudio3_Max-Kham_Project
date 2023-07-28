using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public float damage = 1;
    [SerializeField] private float lifeTime;
    [SerializeField] private GameObject explosion;

    void Start()
    {
        StartCoroutine(DeathDelay());
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        DestroySelf();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealthManager>().DamageEnemy(damage);
            DestroySelf();
        }

        if (collision.tag == "DestroyableObject")
        {
            collision.gameObject.GetComponent<ObjectHealthManager>().DamageObject(damage);
            DestroySelf();
        }

        if (collision.tag == "StrongObject")
        {
            DestroySelf();
        }

        if (collision.tag == "Simple Collider")
        {
            DestroySelf();
        }

        else return;
    }

    private void DestroySelf()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
