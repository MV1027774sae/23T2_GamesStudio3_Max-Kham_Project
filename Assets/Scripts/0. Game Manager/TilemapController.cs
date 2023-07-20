using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
{
    public Tile closedDoorTile;
    public Tile openDoorTile;
    public Tilemap tilemap;
    public Transform teleportDestination;
    public BoxCollider2D doorCollider;

    public GameObject[] enemies;

    private bool doorOpen = false;

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
        doorCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        // Check if there are any enemies in the room
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        bool hasEnemies = enemies.Length > 0;

        if (hasEnemies && doorOpen)
        {
            ChangeTile(closedDoorTile, false);
            doorOpen = false;
            Debug.Log("close the door");
        }
        else if (!hasEnemies && !doorOpen)
        {
            ChangeTile(openDoorTile, true);
            doorOpen = true;
            Debug.Log("open the door");
        }
    }

    private void ChangeTile(Tile tile, bool isTrigger)
    {
        tilemap.SwapTile(closedDoorTile, openDoorTile);
        doorCollider.isTrigger = isTrigger;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.position = teleportDestination.position;
        }
    }
}





























