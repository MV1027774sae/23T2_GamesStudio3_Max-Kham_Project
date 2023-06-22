using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHealthManager : MonoBehaviour
{
    [SerializeField] private int maxHealth = 6;
    public int health = 0;

    // Flashing effect variables
    [SerializeField] private float flashDuration = 0.1f;
    [SerializeField] private Color flashColor = Color.red;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    // Exploding effect variables
    public GameObject explosionPrefab;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        health = maxHealth;
    }

    public void DamageObject(int damage)
    {
        health -= damage;
        StartCoroutine(FlashEffect());

        if(health <= 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            DestroyObject();
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
    
    private IEnumerator FlashEffect()
    {
        Debug.Log("Flash");
        spriteRenderer.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }
}
