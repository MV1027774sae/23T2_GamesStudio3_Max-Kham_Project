using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform teleportDestination; // Reference to the destination transform where the object will be teleported

    // Called when the object enters the teleporter trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        TeleportObject(other.transform);
    }

    // Teleport the object to the destination position
    private void TeleportObject(Transform objectToTeleport)
    {
        if (teleportDestination != null && objectToTeleport != null)
        {
            objectToTeleport.position = teleportDestination.position;
        }
    }
}

