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
        unit.GetComponent<Unit>().OnCreate();
    }
}
