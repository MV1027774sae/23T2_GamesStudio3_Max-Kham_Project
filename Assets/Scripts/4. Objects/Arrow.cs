using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int damageAmount = 1; // Amount of damage caused by the arrow
    public float pushbackForce = 2.0f; // Amount of force applied to the player when hit by the arrow

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the arrow collides with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the PlayerStatManager component from the player
            PlayerStatManager playerStatManager = collision.gameObject.GetComponent<PlayerStatManager>();

            // Cause damage to the player's health
            playerStatManager.DamagePlayer(damageAmount);

            // Calculate the pushback direction from the arrow's velocity
            Vector2 pushbackDirection = rb.velocity.normalized;

            // Apply pushback force to the player's Rigidbody2D component
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.AddForce(pushbackDirection * pushbackForce, ForceMode2D.Impulse);
            }
        }

        // Destroy the arrow
        Destroy(gameObject);
    }
}


