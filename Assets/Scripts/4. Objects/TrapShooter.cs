using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapShooter : MonoBehaviour
{
    public GameObject arrowPrefab; // Reference to the arrow prefab
    public Transform arrowSpawnPoint; // Reference to the spawn point for arrows

    public float shootingInterval = 2f; // Time between each arrow shot
    public float shootingForce = 10f; // The force applied to the arrow when shooting
    public float rotationAngle = 45f; // Rotation angle for the arrow in degrees

    public Vector2 arrowDirection = Vector2.right; // Serialized variable for arrow direction

    private float timeSinceLastShot; // Time elapsed since the last arrow shot

    void Start()
    {
        timeSinceLastShot = shootingInterval;
    }

    void Update()
    {
        // Increment the timer
        timeSinceLastShot += Time.deltaTime;

        // Check if enough time has passed to shoot another arrow
        if (timeSinceLastShot >= shootingInterval)
        {
            ShootArrow();
            timeSinceLastShot = 0f;
        }
    }

    void ShootArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
        arrow.transform.Rotate(0f, 0f, rotationAngle); // Rotate the arrow by the specified angle

        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.AddForce(arrowDirection.normalized * shootingForce, ForceMode2D.Impulse); // Use arrowDirection as the force direction
    }
}

