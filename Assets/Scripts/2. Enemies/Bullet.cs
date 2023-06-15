using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] private float speed = 10f;

    [Range(1, 10)]
    [SerializeField] private float lifeTime = 10f;

    private Rigidbody2D rb;
    [SerializeField] private int damage = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerStatManager>().DamagePlayer(damage);
            Destroy(gameObject);
        }

        if (collision.tag == "Simple Collider")
        {
            Destroy(gameObject);
        }

        else return;
    }
}
