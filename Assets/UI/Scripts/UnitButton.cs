using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitButton : MonoBehaviour
{
    public GameObject[] prefab;
    public Transform spawnPos;

    public void SpawnUnit(int index)
    {
        if (prefab[index].GetComponent<Unit>().cost > MatterManager.matter)
            return;
        GameObject unit = Instantiate(prefab[index], spawnPos.position, Quaternion.identity);
        unit.GetComponent<Unit>().changeMask = 1 << 10;
        unit.GetComponent<Unit>().SmovementDirection = Vector2.right;
        unit.GetComponent<Unit>().OnCreate();  
    }
}
