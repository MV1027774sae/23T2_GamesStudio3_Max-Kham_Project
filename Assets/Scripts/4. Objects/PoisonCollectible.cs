using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonCollectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStatManager playerStatManager = collision.GetComponent<PlayerStatManager>();
            playerStatManager.CollectPoison();

            // Disable the poison object when collected
            Destroy(gameObject);
        }
    }
}
