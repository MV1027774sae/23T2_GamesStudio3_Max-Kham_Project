using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamManager : MonoBehaviour
{
    public int damage = 1;
    [SerializeField] private float attackRate = 0.3f;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            StartCoroutine(DamageEnemy(collision));
            Debug.Log("Detected Enemy Collision");
        }

        if (collision.tag == "Simple Collider")
        {
            return;
        }
    }

    private IEnumerator DamageEnemy(Collider2D collision)
    {
        collision.gameObject.GetComponent<EnemyAIBasic>().DamageEnemy(damage);
        Debug.Log("Damaged Enemy?");
        yield return new WaitForSeconds(attackRate);
    }
}
