using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Unit : MonoBehaviour
{
    [SerializeField] private float range = 5f;
    [SerializeField] private float reloadSpeed = 1f;
    [SerializeField] private LayerMask mask;
    [SerializeField] private Transform bulletOrigin;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;

    [SerializeField] private float speed = 1f;

    [HideInInspector] public Transform nearestTarget;
    [HideInInspector] public bool isReloaded = true;

    public void Shoot()
    {
        Debug.Log("shoot1");
        if (!isReloaded || nearestTarget == null)
            return;

        Debug.Log("shoot2");

        GameObject bullet = Instantiate(bulletPrefab, bulletOrigin.position, Quaternion.identity);

        Vector2 diferense = (nearestTarget.position - transform.position);

        bullet.GetComponent<Rigidbody2D>().AddForce(diferense.normalized * bulletSpeed);
        Quaternion rotation = Quaternion.LookRotation
             (nearestTarget.transform.position - transform.position, transform.TransformDirection(Vector3.up));
        bullet.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);


        isReloaded = false;
        StartCoroutine(Reload());
    }
    public void Move()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.right);
    }

    private void Start()
    {
        InvokeRepeating(nameof(RefreshTargets), 0, .5f);
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
                nearestTarget = target.transform;
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
