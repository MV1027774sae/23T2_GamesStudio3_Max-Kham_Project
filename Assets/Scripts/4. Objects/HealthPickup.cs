using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerStatManager>().health < collision.gameObject.GetComponent<PlayerStatManager>().numOfHearts)
        {
            collision.GetComponent<PlayerStatManager>().HealPlayer(1);
            Destroy(gameObject);
        }
    }
}
