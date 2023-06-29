using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
{
    public Tile closedDoorTile;
    public Tile openDoorTile;
    public GameObject enemy;
    public Tilemap tilemap;
    public Transform teleportDestination;
    public BoxCollider2D doorCollider;

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
        doorCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (enemy == null)
        {
            // Change the tile to open door
            tilemap.SwapTile(closedDoorTile, openDoorTile);

            // Set the door collider as a trigger
            doorCollider.isTrigger = true;
        }
        else
        {
            // Set the door collider as a solid obstacle
            doorCollider.isTrigger = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Teleport the player to the destination
            other.transform.position = teleportDestination.position;
        }
    }
}
