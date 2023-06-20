using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamManager : MonoBehaviour
{
    private bool canAttack = true;
    public int damage = 2;
    [SerializeField] private float attackRate = 0.3f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && canAttack)
        {
            collision.gameObject.GetComponent<EnemyHealthManager>().DamageEnemy(damage);
            canAttack = false;
            Invoke(nameof(ResetAttack), attackRate);
            Debug.Log("Detected Enemy Collision");
        }

        if (collision.tag == "Simple Collider")
        {
            return;
        }

        if (collision.tag == "DestroyableObject" && canAttack)
        {
            collision.gameObject.GetComponent<ObjectHealthManager>().DamageObject(damage);
            canAttack = false;
            Invoke(nameof(ResetAttack), attackRate);

        }
        else return;
    }

    private void ResetAttack()
    {
        canAttack = true;
    }

    private IEnumerator DamageEnemy(Collider2D collision)
    {
        collision.gameObject.GetComponent<EnemyAIBasic>().DamageEnemy(damage);
        Debug.Log("Damaged Enemy?");
        yield return new WaitForSeconds(attackRate);
    }

    private IEnumerator DamageObject(Collider2D collision)
    {
        collision.gameObject.GetComponent<ObjectHealthManager>().DamageObject(damage);
        yield return new WaitForSeconds(attackRate);
    }
}
