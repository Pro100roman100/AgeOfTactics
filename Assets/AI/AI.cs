using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public GameObject[] unitprefab;
    public Transform spawnPos;

    public void Update()
    {
        
            if (unitprefab[0].GetComponent<Unit>().cost > MatterManager.matter)
                return;
            GameObject unit = Instantiate(unitprefab[0], spawnPos.position, Quaternion.identity);
            unit.GetComponent<Unit>().changeMask = 1 << 9;
            unit.GetComponent<Unit>().SmovementDirection = Vector2.left;
            unit.GetComponent<Unit>().OnCreate();
        
    }
}
