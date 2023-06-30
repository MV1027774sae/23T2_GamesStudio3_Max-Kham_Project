using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Wander,
    Follow,
    Die,
    Attack
};

public class EnemyAIBasic : MonoBehaviour
{
    private GameObject player;
    public EnemyState currentState = EnemyState.Wander;

    //enemy stats
    [SerializeField] private float sightRange = 10f;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private float attackRange = 1.2f;
    [SerializeField] private float attackRate = 1f;

    [SerializeField] private float health = 2;

    [SerializeField] private LayerMask simpleCollider;

    private bool chooseDirection, hasAttacked, dead = false;
    private Vector3 randomDirection;

    [SerializeField] private PlayerController2DTopDown playerController2DTopDown;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        switch(currentState)
        {
            case (EnemyState.Wander):
                Wander();
                break;
            case (EnemyState.Follow):
                Follow();
                break;
            case (EnemyState.Die):

                break;
            case (EnemyState.Attack):
                Attack();
                break;
        }

        if(IsPlayerInRange(sightRange) && currentState != EnemyState.Die)
        {
            currentState = EnemyState.Follow;
        }
        else if(!IsPlayerInRange(sightRange) && currentState != EnemyState.Die)
        {
            currentState = EnemyState.Wander;
        }
        if(Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            currentState = EnemyState.Attack;
        }
    }
    
    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    private IEnumerator ChooseDirection()
    {
        chooseDirection = true;
        yield return new WaitForSeconds(Random.Range(1.25f, 6f));
        randomDirection = new Vector3(0, 0, Random.Range(0, 360));
        Quaternion nextRotation = Quaternion.Euler(randomDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 4f));
        chooseDirection = false;
    }

    private void Wander()
    {
        if (!chooseDirection)
        {
            StartCoroutine(ChooseDirection());
        }

        transform.position += transform.right * moveSpeed * Time.deltaTime;
        if(IsPlayerInRange(sightRange))
        {
            currentState = EnemyState.Follow;
        }
    }

    //private void Follow()
    //{
    //    Vector3 target = player.transform.position - transform.position;
    //    float angle = Mathf.Atan2(target.x, target.y) * Mathf.Rad2Deg;
    //    transform.rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);
    //    transform.position += transform.right * moveSpeed * Time.deltaTime;
    //}

    private void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }

    public void DamageEnemy(int damage)
    {
        health -= damage;
        if(health == 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    private void Attack()
    {
        if(!hasAttacked)
        {
            playerController2DTopDown.StartCoroutine(playerController2DTopDown.FlashCo());
            //PlayerStatManager.DamagePlayer(attackDamage);
            StartCoroutine(HasAttacked());
        }
    }
    private IEnumerator HasAttacked()
    {
        hasAttacked = true;
        yield return new WaitForSeconds(attackRate);
        hasAttacked = false;
    }
}
