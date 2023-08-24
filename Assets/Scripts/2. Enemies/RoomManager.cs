using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemiesInRoom = new List<GameObject>();
    private bool on;

    private BoxCollider2D BoxCollider;

    void Start()
    {
        on = false;
        BoxCollider = gameObject.GetComponent<BoxCollider2D>();
        StartCoroutine(Delay());
    }

    void Update()
    {
        if (on)
        {
            if (!_enemiesInRoom.Contains(GameObject.FindGameObjectWithTag("Enemy")))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            _enemiesInRoom.Add(collision.gameObject);
        }
    }

    private IEnumerator Delay()
    {
        on = true;
        yield return new WaitForSeconds(10);
    }
} 

