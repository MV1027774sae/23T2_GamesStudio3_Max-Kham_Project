using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAttack : MonoBehaviour
{
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private float attackRate = 2f;
    [SerializeField] private float projectileVelocity = 8f;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Transform shootPointPosition;

    private bool _canAttack = false;
    private GameObject _target;
    private Rigidbody2D rb;
    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();
        ResetRangedAttack();
    }

    void Update()
    {
        if (_target != null && _canAttack && Vector2.Distance(_target.transform.position, transform.position) <= attackRange)
        {
            RangedAttack();
        }
    }

    private void RangedAttack()
    {
        Vector3 aimDirection = (_target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        shootPoint.eulerAngles = new Vector3(0, 0, angle);

        Rigidbody2D ball = Instantiate(projectile, shootPointPosition.position, Quaternion.Euler(0, 0, angle)).GetComponent<Rigidbody2D>();
        ball.velocity = new Vector2(aimDirection.x, aimDirection.y).normalized * projectileVelocity;

        _canAttack = false;
        Invoke(nameof(ResetRangedAttack), attackRate);
    }

    private void ResetRangedAttack()
    {
        _canAttack = true;
    }
}
