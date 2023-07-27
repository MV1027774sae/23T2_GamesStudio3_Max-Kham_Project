using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballManager : MonoBehaviour
{
    public float damage = 10;
    [SerializeField] private float damageRadius = 3f;
    [SerializeField] private float lifeTime = 2.5f;
    [SerializeField] private GameObject explosion;
    [SerializeField] private LayerMask targetLayermask;

    private PlayerController2DTopDown playerController2DTopDown;

    Vector2 hitPositionForGizmoDrawing;

    void Start()
    {
        StartCoroutine(DeathDelay());
        playerController2DTopDown = GameObject.Find("Player").GetComponent<PlayerController2DTopDown>();
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        DestroySelf();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float explosionDamage = (damage * playerController2DTopDown.secondaryDamageMultiplier);
        float explosionRadius = (damageRadius * playerController2DTopDown.secondaryDamageMultiplier);

        if (collision.tag == "Enemy" || collision.tag == "DestroyableObject" || collision.tag == "StrongObject" || collision.tag == "Simple Collider")
        {
            hitPositionForGizmoDrawing = collision.gameObject.transform.position;
            Collider2D[] targetsHit = Physics2D.OverlapCircleAll(transform.position, damageRadius, targetLayermask);
            {
                foreach(Collider2D target in targetsHit)
                {
                    if (target.gameObject.GetComponent<EnemyHealthManager>() != null)
                    {
                        target.gameObject.GetComponent<EnemyHealthManager>().DamageEnemy(explosionDamage);
                    }
                    if (target.gameObject.GetComponent<ObjectHealthManager>() != null)
                    {
                        target.gameObject.GetComponent<ObjectHealthManager>().DamageObject(explosionDamage);
                    }
                }
            }
            DestroySelf();
        }
        else return;
    }

    private void DestroySelf()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitPositionForGizmoDrawing, damageRadius);
    }

}
