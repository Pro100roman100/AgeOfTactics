using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_text : MonoBehaviour
{
    [SerializeField] private Health enemyBaseHealth;
    [SerializeField] private Image healthBar;

    private float oldHealth;

    private void Start()
    {
        oldHealth = enemyBaseHealth.health;
    }
    private void Update()
    {
        if (oldHealth == enemyBaseHealth.health)
            return;
        oldHealth = enemyBaseHealth.health;
        healthBar.fillAmount = enemyBaseHealth.health / enemyBaseHealth.maxHealth;
    }
}
