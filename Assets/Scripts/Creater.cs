using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creater : MonoBehaviour
{
    public GameObject prefab;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            GameObject unit = Instantiate(
                prefab,
                new Vector3(transform.position.x, transform.position.y, transform.position.z),
                Quaternion.identity
                );
            unit.GetComponent<Unit>().OnCreate();
        }
    }
}
