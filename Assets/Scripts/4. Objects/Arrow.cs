using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int damageAmount = 1; // Amount of damage caused by the arrow

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the arrow collides with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the PlayerStatManager component from the player
            PlayerStatManager playerStatManager = collision.gameObject.GetComponent<PlayerStatManager>();

            // Cause damage to the player's health
            playerStatManager.DamagePlayer(damageAmount);
        }

        // Destroy the arrow
        Destroy(gameObject);
    }
}
