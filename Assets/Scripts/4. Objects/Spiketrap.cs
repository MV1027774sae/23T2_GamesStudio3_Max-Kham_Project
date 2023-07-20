using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiketrap : MonoBehaviour
{
    public int damageAmount = 1;
    public float pushbackForce = 2.0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DamageAndPushbackPlayer(other.gameObject);
            Debug.Log("Player triggered the SpikeTrap!");
        }
    }

    private void DamageAndPushbackPlayer(GameObject player)
    {
        // Apply damage to the player
        PlayerStatManager playerStats = player.GetComponent<PlayerStatManager>();
        if (playerStats != null)
        {
            playerStats.DamagePlayer(damageAmount);
        }

        // Calculate the pushback direction from the trap to the player
        Vector2 pushbackDirection = player.transform.position - transform.position;
        pushbackDirection = pushbackDirection.normalized;

        // Apply pushback force to the player's position
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            playerRb.MovePosition(playerRb.position + pushbackDirection * pushbackForce * Time.fixedDeltaTime);
        }
    }
}

