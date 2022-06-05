using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHeath = 100;

    private float heath;

    private void Awake()
    {
        heath = maxHeath;
    }

    private void Update()
    {
//For testing, use left ctrl to kill all units
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.LeftControl))
            Kill();
#endif

    }


    public void TakeDamage(float damage)
    {
        heath -= damage;

        if (heath <= 0)
            Kill();
    }

    public void Kill()
    {
        Destroy(this.gameObject);
    }
}
