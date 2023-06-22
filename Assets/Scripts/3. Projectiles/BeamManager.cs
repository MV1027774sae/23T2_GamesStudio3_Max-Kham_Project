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
        }

        if (collision.tag == "DestroyableObject")
        {
            Debug.Log("Detected Destructible Object");
            collision.gameObject.GetComponent<ObjectHealthManager>().DamageObject(damage);
            canAttack = false;
            Invoke(nameof(ResetAttack), attackRate);
        }

        if (collision.tag == "Simple Collider")
        {
            return;
        }
        else return;
    }

    private void ResetAttack()
    {
        canAttack = true;
    }
}
