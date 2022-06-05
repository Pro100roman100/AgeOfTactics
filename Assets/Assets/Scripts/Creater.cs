using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creater : MonoBehaviour
{

    public GameObject _GO;
    void Start()
    {
        
    }

    
    void Update()
    {
        
            if (Input.GetKeyUp(KeyCode.Space))
            {
            Instantiate(_GO, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);


        }
        
        
    }
}
