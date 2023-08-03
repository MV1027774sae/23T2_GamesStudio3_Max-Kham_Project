using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPickupRespawner : MonoBehaviour
{
    [SerializeField] private GameObject manaPickup; 
    
    private GameObject player;
    private bool canSpawn = true;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (player.GetComponent<PlayerController2DTopDown>().secondaryMana >= 1)
        {
            canSpawn = true;
        }
            
        if (player.GetComponent<PlayerController2DTopDown>().secondaryMana == 0 && canSpawn)
        {
            canSpawn = false;
            Invoke(nameof(SpawnMana), 1.5f);
        }
    }

    private void SpawnMana()
    {
        Instantiate(manaPickup, transform.position, Quaternion.identity);
    }
}
