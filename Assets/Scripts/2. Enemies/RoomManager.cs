using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemiesInRoom = new List<GameObject>();

    private BoxCollider2D BoxCollider;

    void Start()
    {
        BoxCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (!_enemiesInRoom.Contains(GameObject.FindGameObjectWithTag("Enemy")))
        {
            Debug.Log("No enemies remaining");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            _enemiesInRoom.Add(collision.gameObject);
        }
    }
} 

