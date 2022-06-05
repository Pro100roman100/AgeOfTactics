using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyMovement : MonoBehaviour
{

    [SerializeField] Transform Player_2_Base;
    public Transform ShotRange;
    public GameObject Ammo;

    // Скорость, дистанция
    private float Army_speed = 2;
    float _Distance;




    void FixedUpdate()
    {
       
        _Distance = Vector3.Distance(ShotRange.transform.position, transform.position);

        if (_Distance <= 10)
        {
            Instantiate(Ammo, transform.position, Quaternion.identity);
        }
        else if (_Distance > 10)
        {
            transform.Translate(Army_speed * Time.deltaTime, 0, 0);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Base")
        {
            Destroy(this.gameObject);
        }
    }
}
