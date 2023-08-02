using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryFireExplosion : MonoBehaviour
{
    [SerializeField] private float lifeTime = 0.3f;
    [SerializeField] private AudioClip explosionSFX;
    private AudioSource audioSource;
   
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(DeathDelay());
    }

    IEnumerator DeathDelay()
    {
        audioSource.PlayOneShot(explosionSFX);
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}