using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPickup : MonoBehaviour
{
    [SerializeField] private GameObject pickupSFX;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerController2DTopDown>().secondaryMana < 2)
        {
            collision.GetComponent<PlayerController2DTopDown>().AddMana();
            Instantiate(pickupSFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
