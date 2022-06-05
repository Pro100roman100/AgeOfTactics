using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    private float Ammo_speed = 0.01f;
       
    void Update()
    {
        transform.Translate(Vector3.right * Ammo_speed);
    }
}
