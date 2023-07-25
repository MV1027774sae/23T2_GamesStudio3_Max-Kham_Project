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
        rb.isKinematic = true; // Ensure the arrow is kinematic as it's a projectile
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the arrow collides with the player
        if (other.CompareTag("Player"))
        {
            // Get the PlayerStatManager component from the player
            PlayerStatManager playerStatManager = other.gameObject.GetComponent<PlayerStatManager>();

            // Cause damage to the player's health
            playerStatManager.DamagePlayer(damageAmount);

            // Calculate the pushback direction from the arrow's velocity
            Vector2 pushbackDirection = rb.velocity.normalized;

            // Apply pushback force to the player's Rigidbody2D component
            Rigidbody2D playerRb = other.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.AddForce(pushbackDirection * pushbackForce, ForceMode2D.Impulse);
            }

            // Destroy the arrow
            Destroy(gameObject);
        }
        else if (other.CompareTag("Enemy"))
        {

            // Destroy the arrow when it hits an enemy
            Destroy(gameObject);
        }
    }
}



