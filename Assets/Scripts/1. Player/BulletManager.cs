using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private Transform tr; 

    [SerializeField] private float lifeTime;
    public int damage = 1;

    [SerializeField] private GameObject explosion;

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
        if(collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyAIBasic>().DamageEnemy(damage);
            Explode();
        }

        if(collision.tag == "Chargable Object")
        {
            collision.gameObject.GetComponent<SwitchController>().ChargeSwitch();
            Explode();
        }

        if(collision.tag == "Simple Collider")
        {
            Explode();
        }
    }

    private void Explode()
    {
        Instantiate(explosion, tr.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
