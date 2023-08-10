using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireballManager : MonoBehaviour
{
    public int damage = 2;
    [SerializeField] private float damageRadius = 3f;
    [SerializeField] private float lifeTime = 2.5f;
    [SerializeField] private GameObject explosion;
    [SerializeField] private LayerMask targetLayermask;

    Vector2 hitPositionForGizmoDrawing;

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
        if (collision.tag == "Player" || collision.tag == "DestroyableObject" || collision.tag == "StrongObject" || collision.tag == "Simple Collider")
        {
            hitPositionForGizmoDrawing = collision.gameObject.transform.position;
            Collider2D[] targetsHit = Physics2D.OverlapCircleAll(transform.position, damageRadius, targetLayermask);
            {
                foreach (Collider2D target in targetsHit)
                {
                    if (target.gameObject.GetComponent<PlayerStatManager>() != null)
                    {
                        target.gameObject.GetComponent<PlayerStatManager>().DamagePlayer(damage);
                    }
                    if (target.gameObject.GetComponent<ObjectHealthManager>() != null)
                    {
                        target.gameObject.GetComponent<ObjectHealthManager>().DamageObject(damage);
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
}
