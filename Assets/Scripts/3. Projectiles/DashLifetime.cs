using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashLifetime : MonoBehaviour
{
    [SerializeField] private float lifeTime = 0.3f;

    void Start()
    {
        StartCoroutine(DeathDelay());
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
