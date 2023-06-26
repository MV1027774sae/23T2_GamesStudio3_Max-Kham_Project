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
    //private /*static*/ int health = 4;
    //[SerializeField] private /*static*/ int maxHealth = 4;
    //[SerializeField] private /*static*/ float moveSpeed = 5f;
    //[SerializeField] private /*static*/ float fireRate = 0.6f;

    //public static int Health { get => health; set => health = value; }
    //public static int MaxHealth { get => maxHealth; set => maxHealth = value; }
    //public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    //public static float FireRate { get => fireRate; set => fireRate = value; }

    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;


    public TextMeshProUGUI healthTMPro;

    [SerializeField] private PlayerController2DTopDown playerController2DTopDown;

    void Awake()
    {
        //if(instance == null)
        //{
        //    instance = this;
        //}

        playerController2DTopDown = GetComponent<PlayerController2DTopDown>();
        health = numOfHearts;
    }

    void Update()
    {
        //healthTMPro.text = "Health: " + health.ToString();

        if(health > numOfHearts)
        {
            health = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
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
        health = Mathf.Min(numOfHearts, health + healAmount);
    }

    private /*static*/ void KillPlayer()
    {
        Cursor.visible = true;
        SceneManager.LoadScene(1);
    }
}
