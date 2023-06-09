using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    public float rotateSpeed = 5f;
    private Rigidbody2D rb;

    private bool canAttack = true;
    [SerializeField] private float attackRate = 0.5f;

    [SerializeField] private int damage = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(!target)
        {
            GetTarget();
        }
        else
        {
            RotateTorwardsTarget();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
    }

    private void RotateTorwardsTarget()
    {
        Vector2 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed);
    }

    private void GetTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player") && canAttack)
        {
            other.gameObject.GetComponent<PlayerStatManager>().DamagePlayer(damage);
            target = null;
            canAttack = false;

            StartCoroutine(ResetAttack());
        }
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(attackRate);
        canAttack = true;
    }
}






