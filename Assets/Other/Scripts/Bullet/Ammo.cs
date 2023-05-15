using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [HideInInspector] public float damage;
    [SerializeField] public string enemyTag;
    [SerializeField] public string thisTag;
    private void Awake()
    {
        this.gameObject.tag = thisTag;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("CCollision");
        if (other.gameObject.CompareTag(enemyTag))
        {
            other.gameObject.GetComponent<HealthManager>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
