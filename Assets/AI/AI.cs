using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public GameObject[] unitprefab;
    public Transform spawnPos;

    public void Update()
    {
        if (unitprefab[0].GetComponent<Unit>().cost > MatterManager.player2matter)
            return;
        GameObject unit = Instantiate(unitprefab[0], spawnPos.position, Quaternion.identity);
        unit.GetComponent<Unit>().changeMask = 1 << 9;
        unit.GetComponent<Unit>().SmovementDirection = Vector2.left;
        unit.GetComponent<Unit>().OnCreate();
        unit.layer = 10;
        unit.GetComponent<Unit>().changetagConnection = GameObject.Find("Player_1_Base").tag;
        unit.tag = GameObject.Find("Player_2_Base").tag;
        MatterManager.Manager.massSpending(unit.tag, unit.GetComponent<Unit>().cost);
    }
}