using UnityEngine;

public class RoomController : MonoBehaviour
{
    public GameObject[] enemies; // Reference to the enemies in the room

    private void Start()
    {
        // Disable all enemies initially
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Activate the enemies when the player enters the collider
            foreach (GameObject enemy in enemies)
            {
                enemy.SetActive(true);
            }
        }
    }
}

