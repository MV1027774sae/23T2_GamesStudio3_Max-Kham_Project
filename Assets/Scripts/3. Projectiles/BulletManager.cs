using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
<<<<<<<< HEAD:Assets/Scripts/1. Player/Projectiles/BulletManager.cs
    [SerializeField] private Transform tr; 

    [SerializeField] private float lifeTime = 1;
========
    [SerializeField] private GameObject explosion;
    [SerializeField] private Transform tr;
    
    [SerializeField] private float lifeTime;
>>>>>>>> main:Assets/Scripts/3. Projectiles/BulletManager.cs
    public int damage = 1;

    void Start()
    {
        tr = this.GetComponent<Transform>();
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

<<<<<<<< HEAD:Assets/Scripts/1. Player/Projectiles/BulletManager.cs
        if(collision.tag == "Simple Collider")
========
        if (collision.tag == "DestroyableObject")
        {
            collision.gameObject.GetComponent<ObjectHealthManager>().DamageObject(damage);
            Explode();
        }

        if (collision.tag == "Simple Collider")
>>>>>>>> main:Assets/Scripts/3. Projectiles/BulletManager.cs
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
