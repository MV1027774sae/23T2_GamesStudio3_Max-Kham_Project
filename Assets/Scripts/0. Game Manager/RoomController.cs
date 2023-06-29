using UnityEngine;

public class RoomController : MonoBehaviour
{
    public Collider room1Collider;
    public Collider room2Collider;
    public GameObject room1Objects;
    public GameObject room2Objects;

    private void Start()
    {
        // Initialize the rooms
        ActivateRoom(room1Objects);
        DeactivateRoom(room2Objects);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Check which room the player entered
            if (other.gameObject.GetComponent<Collider>().Equals(room1Collider))
            {
                // Player entered room 1
                ActivateRoom(room1Objects);
                DeactivateRoom(room2Objects);
            }
            else if (other.gameObject.GetComponent<Collider>().Equals(room2Collider))
            {
                // Player entered room 2
                ActivateRoom(room2Objects);
                DeactivateRoom(room1Objects);
            }
        }
    }

    private void ActivateRoom(GameObject roomObjects)
    {
        roomObjects.SetActive(true);
    }

    private void DeactivateRoom(GameObject roomObjects)
    {
        roomObjects.SetActive(false);
    }
}

