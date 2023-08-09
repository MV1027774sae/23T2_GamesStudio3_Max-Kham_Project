using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstRangedEnemyAttack : MonoBehaviour
{
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private float attackRate = 3f;
    [SerializeField] private float projectileVelocity = 8f;
    [SerializeField] private int projectilesPerBurst = 5;
    [SerializeField] private float burstAttackRate = 0.15f;
    [SerializeField] private GameObject projectile;


    [SerializeField] private GameObject secondaryProjectile;
    [SerializeField] private float fireballDelay = 0.66f;

    [SerializeField] private Transform shootPoint;
    [SerializeField] private float fireballVelocity = 6.5f;
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
            StartCoroutine(BurstRangedAttack());
        }

        if (gameObject.GetComponent<EnemyHealthManager>().health <= gameObject.GetComponent<EnemyHealthManager>().maxHealth / 2)
        {
            projectilesPerBurst = 10;
            attackRate = 1f;
            fireballDelay = 0.25f;
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
    }

    private void FireBallAttack()
    {
        Vector3 aimDirection = (_target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        shootPoint.eulerAngles = new Vector3(0, 0, angle);

        Rigidbody2D ball = Instantiate(secondaryProjectile, shootPoint.position, Quaternion.Euler(0, 0, angle)).GetComponent<Rigidbody2D>();
        ball.velocity = new Vector2(aimDirection.x, aimDirection.y).normalized * fireballVelocity;
    }

    private void ResetRangedAttack()
    {
        _canAttack = true;
    }

    private IEnumerator BurstRangedAttack()
    {
        for (int i = 0; i < projectilesPerBurst; i++)
        {
            RangedAttack();

            yield return new WaitForSeconds(burstAttackRate);
        }

        yield return new WaitForSeconds(fireballDelay);
        FireBallAttack();

        Invoke(nameof(ResetRangedAttack), attackRate);
    }
}
