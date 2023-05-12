using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [HideInInspector] public float damage;
    [SerializeField] public string enemyTag;
    [SerializeField] public string thisTag;
    private void Start()
    {
        this.gameObject.tag = thisTag;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(thisTag))
            return;
        if (collision.collider.CompareTag(enemyTag))
            collision.gameObject.GetComponent<HealthManager>().TakeDamage(damage);
        Destroy(this.gameObject);
    }
}
