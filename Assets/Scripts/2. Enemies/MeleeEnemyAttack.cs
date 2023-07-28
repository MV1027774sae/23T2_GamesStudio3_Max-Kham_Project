using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyAttack : MonoBehaviour
{
    private bool canAttack = true;

    [SerializeField] private float attackRate = 0.5f;
    [SerializeField] private int damage = 1;

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && canAttack)
        {
            other.gameObject.GetComponent<PlayerStatManager>().DamagePlayer(damage);
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
