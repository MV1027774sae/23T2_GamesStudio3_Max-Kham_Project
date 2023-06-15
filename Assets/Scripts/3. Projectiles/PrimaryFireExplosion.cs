using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryFireExplosion : MonoBehaviour
{
    private float lifeTime = 0.3f;
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
