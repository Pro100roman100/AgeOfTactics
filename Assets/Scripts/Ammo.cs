using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private float Ammo_speed = 0.1f;
    [HideInInspector] public Transform ShotRange;

    void Update()
    {
        transform.Translate(-(transform.position - ShotRange.position).normalized * Ammo_speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Base"))
            collision.GetComponent<Health>().TakeDamage(150);
        Destroy(this.gameObject);
    }
}
