using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potionSFX : MonoBehaviour
{
    [SerializeField] private AudioClip SFX;
    [SerializeField] private AudioSource audioSource;
    void Start()
    {
        audioSource.PlayOneShot(SFX);
        StartCoroutine(DeathDelay());
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
