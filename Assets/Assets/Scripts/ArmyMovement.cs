using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyMovement : MonoBehaviour
{
    
    [SerializeField] Transform  Player_2_Base;
    // Скорость, дистанция
    private float Army_speed = 2;
   


    void FixedUpdate()
    {
        transform.Translate(Army_speed * Time.deltaTime,0,0);

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Base")
        {
            Destroy(this.gameObject);
        }
    }
}
