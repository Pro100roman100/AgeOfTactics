using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitButton : MonoBehaviour
{
    public GameObject[] prefab;
    public Transform spawnPos;

    public void SpawnUnit(int index)
    {
        if (prefab[index].GetComponent<Unit>().cost > MatterManager.player1matter)
            return;
        GameObject unit = Instantiate(prefab[index], spawnPos.position, Quaternion.identity);
        unit.GetComponent<Unit>().changeMask = 1 << 10;
        unit.GetComponent<Unit>().SmovementDirection = Vector2.right;
        unit.GetComponent<Unit>().OnCreate();
        unit.layer = 9;
        unit.GetComponent<Unit>().changetagConnection = GameObject.Find("Player_2_Base").tag;
        unit.tag = GameObject.Find("Player_1_Base").tag;
        MatterManager.Manager.massSpending(unit.tag, unit.GetComponent<Unit>().cost);
    }
}
