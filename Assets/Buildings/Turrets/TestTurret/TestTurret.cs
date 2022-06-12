using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTurret : Turret
{
    private void FixedUpdate()
    {
        if (nearestTarget != null)
        {
            Shoot();
        }
    }
}
