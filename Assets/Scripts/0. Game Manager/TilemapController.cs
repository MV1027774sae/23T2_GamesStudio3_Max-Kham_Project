using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
{
    //public Tile closedDoorTile;
    //public Tile openDoorTile;
    //public Tilemap tilemap;
    //public Transform teleportDestination;
    //public BoxCollider2D doorCollider;

    //private bool doorOpen = false;
    //private bool enemiesPresent = false; // Added variable to track the presence of enemies

    //private void Start()
    //{
    //    tilemap = GetComponent<Tilemap>();
    //    doorCollider = GetComponent<BoxCollider2D>();
    //}

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        TeleportPlayerToDestination(other.gameObject);
    //    }
    //}

    //private void Update()
    //{
    //    // Check if there are any enemies in the room
    //    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
    //    bool hasEnemies = enemies.Length > 0;

    //    if (hasEnemies && doorOpen)
    //    {
    //        // Change tile to closedDoorTile when enemies are present
    //        tilemap.SwapTile(openDoorTile, closedDoorTile);
    //        doorOpen = false;
    //        Debug.Log("Close the door");
    //    }
    //    else if (!hasEnemies && !doorOpen)
    //    {
    //        // Change tile to openDoorTile when no enemies
    //        tilemap.SwapTile(closedDoorTile, openDoorTile);
    //        doorOpen = true;
    //        Debug.Log("Open the door");
    //    }
    //}

    //private void TeleportPlayerToDestination(GameObject player)
    //{
    //    Transform teleportDestination = transform.Find("DoorDestination");
    //    if (teleportDestination != null)
    //    {
    //        player.transform.position = teleportDestination.position;
    //    }
    //}

    //// Method to set the presence of enemies in the room
    //public void SetEnemiesPresent(bool present)
    //{
    //    if (present && doorOpen)
    //    {
    //        // Change tile to closedDoorTile when enemies are present
    //        tilemap.SwapTile(openDoorTile, closedDoorTile);
    //        doorOpen = false;
    //        Debug.Log("Close the door");
    //    }
    //    else if (!present && !doorOpen)
    //    {
    //        // Change tile to openDoorTile when no enemies
    //        tilemap.SwapTile(closedDoorTile, openDoorTile);
    //        doorOpen = true;
    //        Debug.Log("Open the door");
    //    }
    //}
}

































