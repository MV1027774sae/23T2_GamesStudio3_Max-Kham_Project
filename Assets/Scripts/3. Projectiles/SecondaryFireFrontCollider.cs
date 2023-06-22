using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryFireFrontCollider : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject parentObject;

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
        if (collision.tag == "Simple Collider")
        {
            parentObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        }
    }

    private void Explode()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
