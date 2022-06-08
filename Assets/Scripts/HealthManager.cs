using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthManager : MonoBehaviour
{
    public float maxHealth = 100;
    [HideInInspector] public float Health { get; private set; }
    
    private void Awake()
    {
        Health = maxHealth;
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
        if (Health + heal >= maxHealth)
            Health = maxHealth;
    }
    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
            Kill();
    }
    public void Kill()
    {
        Destroy(this.gameObject);
    }
}
