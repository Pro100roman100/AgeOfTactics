using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyMovement : MonoBehaviour
{
    
    [SerializeField] Transform  Player_2_Base;
   
    private float Army_speed = 2;
   


    void FixedUpdate()
    {
        transform.position += Vector3.right * Army_speed * Time.deltaTime;

    }
    void OnCollisionEnter(Collision otherObj)
    {
        if (otherObj.gameObject.tag == "Base")
        {
            Destroy(gameObject);
        }
    }
}
