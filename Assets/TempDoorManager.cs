using UnityEngine;
using UnityEngine.Tilemaps;

public class TempDoorManager : MonoBehaviour
{
    public Tile closedDoorTile;
    public Tile openDoorTile;
    public Tilemap tilemap;
    public Transform teleportDestination;
    public BoxCollider2D doorCollider;

    private bool isDeactivated = false;

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
        doorCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.position = teleportDestination.position;
        }
    }
}