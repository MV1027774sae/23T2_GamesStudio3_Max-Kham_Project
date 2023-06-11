using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    public int damage = 1;

    void Start()
    {
        StartCoroutine(DeathDelay());
    }

    void Update()
    {

    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyAIBasic>().DamageEnemy(damage);
            Destroy(gameObject);
        }

        if (collision.tag == "ChargableObject")
        {
            collision.gameObject.GetComponent<SwitchController>().ChargeSwitch();
            Destroy(gameObject);
        }

        if (collision.tag == "SimpleCollider")
        {
            Destroy(gameObject);
        }
    }
}
