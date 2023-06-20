using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private Transform tr;
    
    [SerializeField] private float lifeTime;
    public int damage = 1;

    void Start()
    {
        StartCoroutine(DeathDelay());
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Explode();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealthManager>().DamageEnemy(damage);
            Explode();
        }

        if (collision.tag == "DestroyableObject")
        {
            collision.gameObject.GetComponent<ObjectHealthManager>().DamageObject(damage);
            Explode();
        }

        if (collision.tag == "Simple Collider")
        {
            Explode();
        }
    }

    private void Explode()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
