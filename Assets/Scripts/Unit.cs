using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Unit : MonoBehaviour
{
    [SerializeField] private float range = 5f;
    [SerializeField] private float reloadSpeed = 1f;
    [SerializeField] private LayerMask mask;

    [SerializeField] private float speed = 1f;

    private GameObject nearestTarget;
    private bool isReloaded;

    public void Shoot()
    {
        if (!isReloaded || nearestTarget == null)
            return;

        //Shoot code

        StartCoroutine(Reload());
    }
    public void Move()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void Start()
    {
        InvokeRepeating(nameof(RefreshTargets), 0, 5f);
    }

    private void RefreshTargets()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, range, mask);

        if (targets.Length == 0)
        {
            nearestTarget = null;
            return;
        }

        float sqrDistanse = Mathf.Infinity;

        foreach (Collider2D target in targets)
        {
            if (Vector3.SqrMagnitude(transform.position - target.transform.position) < sqrDistanse)
                nearestTarget = target.gameObject;
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadSpeed);
        isReloaded = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
