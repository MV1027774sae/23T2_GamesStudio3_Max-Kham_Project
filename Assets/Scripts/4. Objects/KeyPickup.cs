using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerStatManager>().numOfKeys <= 99)
        {
            collision.GetComponent<PlayerStatManager>().AddKey();
            Destroy(gameObject);
        }
    }
}
