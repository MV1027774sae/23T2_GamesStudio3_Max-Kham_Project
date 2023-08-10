using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private GameObject healthBarOwner;
    private Slider healthSlider;

    private void Start()
    {
        healthSlider = GetComponent<Slider>();
        SetMaxHealth();
    }

    private void Update()
    {
        if (healthBarOwner != null)
        {
            SetHealth();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void SetMaxHealth()
    {
        healthSlider.maxValue = healthBarOwner.GetComponent<EnemyHealthManager>().maxHealth;
    }

    public void SetHealth()
    {
        healthSlider.value = healthBarOwner.GetComponent<EnemyHealthManager>().health;
    }
}
