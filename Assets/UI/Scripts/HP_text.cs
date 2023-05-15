using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_text : MonoBehaviour
{
    [SerializeField] private HealthManager player1BaseHealth;
    [SerializeField] private HealthManager player2BaseHealth;
    [SerializeField] private Image p1healthBar;
    [SerializeField] private Image p2healthBar;

    private float p1oldHealth;
    private float p2oldHealth;

    private void Start()
    {
        p1oldHealth = player1BaseHealth.Health;
        p2oldHealth = player2BaseHealth.Health;
    }
    private void Update()
    {
        if (p1oldHealth != player1BaseHealth.Health)
        {
            p1oldHealth = player1BaseHealth.Health;
            p1healthBar.fillAmount = player1BaseHealth.Health / player1BaseHealth.maxHealth;
        }
        if (p2oldHealth != player2BaseHealth.Health)
        {
            p2oldHealth = player2BaseHealth.Health;
            p2healthBar.fillAmount = player2BaseHealth.Health / player2BaseHealth.maxHealth;
        }
    }
}
