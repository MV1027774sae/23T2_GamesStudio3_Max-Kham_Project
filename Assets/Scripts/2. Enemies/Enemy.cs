using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    public float rotateSpeed = 5f;
    private Rigidbody2D rb;

    private bool canAttack = true;
    [SerializeField] private float attackRate = 1.5f;
    [SerializeField] private int damage = 1;

    // Flashing effect variables
    [SerializeField] private float flashDuration = 0.1f;
    [SerializeField] private Color flashColor = Color.red;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    // Exploding effect variables
    public GameObject explosionPrefab;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void Update()
    {
        if (!target)
        {
            GetTarget();
        }
        else
        {
            RotateTowardsTarget();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
    }

    private void RotateTowardsTarget()
    {
        Vector2 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed);
    }

    private void GetTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && canAttack)
        {
            other.gameObject.GetComponent<PlayerStatManager>().DamagePlayer(damage);
            target = null;
            canAttack = false;

            StartCoroutine(ResetAttack());
            StartCoroutine(FlashEffect());
            Explode();
        }
        else if (other.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(FlashEffect());
        }
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(attackRate);
        canAttack = true;
    }

    IEnumerator FlashEffect()
    {
        spriteRenderer.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }

    void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

