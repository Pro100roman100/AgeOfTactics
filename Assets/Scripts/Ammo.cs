using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [HideInInspector] public float damage;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
            collision.gameObject.GetComponent<HealthManager>().TakeDamage(damage);
        Destroy(this.gameObject);
    }
}
