using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerStatManager : MonoBehaviour
{
    //public /*static*/ PlayerStatManager instance;

    //public string characterName = "undefined";
    //public int currentLevel = 0;

    //player stats
    private /*static*/ int health = 4;
    [SerializeField] private /*static*/ int maxHealth = 4;
    //[SerializeField] private /*static*/ float moveSpeed = 5f;
    //[SerializeField] private /*static*/ float fireRate = 0.6f;

    //public static int Health { get => health; set => health = value; }
    //public static int MaxHealth { get => maxHealth; set => maxHealth = value; }
    //public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    //public static float FireRate { get => fireRate; set => fireRate = value; }

    public TextMeshProUGUI healthTMPro;

    [SerializeField] private PlayerController2DTopDown playerController2DTopDown;

    void Awake()
    {
        //if(instance == null)
        //{
        //    instance = this;
        //}

        health = maxHealth;
    }

    void Update()
    {
        healthTMPro.text = "Health: " + health.ToString();
    }

    public /*static*/ void DamagePlayer(int damage)
    {
        health -= damage;
        playerController2DTopDown.StartCoroutine(playerController2DTopDown.FlashCo());

        if (health <= 0)
        {
            KillPlayer();
        }
    }

    public /*static*/ void HealPlayer(int healAmount)
    {
        health = Mathf.Min(maxHealth, health + healAmount);
    }

    private /*static*/ void KillPlayer()
    {
        Cursor.visible = true;
        SceneManager.LoadScene(1);
    }
}
