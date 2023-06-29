using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroy : MonoBehaviour
{
    public GameObject destructionEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(gameObject);

            if (destructionEffect != null)
            {
                Instantiate(destructionEffect, transform.position, transform.rotation);
            }
        }
    }
}

