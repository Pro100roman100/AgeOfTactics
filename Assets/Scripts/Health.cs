using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    [HideInInspector] public float health { get; private set; }
    
    private void Awake()
    {
        health = maxHealth;       
    }

    private void Update()
    {
//For testing, use left ctrl to kill all units
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.LeftControl))
            Kill();
#endif
    }

    public void Heal(float heal)
    {
        if (health + heal >= maxHealth)
            health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
            Kill();
    }

    public void Kill()
    {
        Destroy(this.gameObject);
    }
   
}
