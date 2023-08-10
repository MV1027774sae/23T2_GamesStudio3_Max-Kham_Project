using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBarManager : MonoBehaviour
{
    private EnemyHealthManager enemyHealthManager;
    [SerializeField] private GameObject healthBar;
    void Start()
    {
        enemyHealthManager = gameObject.GetComponent<EnemyHealthManager>();
    }

    void Update()
    {
        
    }
}
