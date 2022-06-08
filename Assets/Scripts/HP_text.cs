using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_text : MonoBehaviour
{
    [SerializeField] private HealthManager enemyBaseHealth;
    [SerializeField] private Image healthBar;

    private float oldHealth;

    private void Start()
    {
        oldHealth = enemyBaseHealth.Health;
    }
    private void Update()
    {
        if (oldHealth == enemyBaseHealth.Health)
            return;
        oldHealth = enemyBaseHealth.Health;
        healthBar.fillAmount = enemyBaseHealth.Health / enemyBaseHealth.maxHealth;
    }
}
