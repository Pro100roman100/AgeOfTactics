using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUnit : Unit
{
    private void FixedUpdate()
    {
        if (!inRange)
            Move();
        else
            Shoot();
    }
}
