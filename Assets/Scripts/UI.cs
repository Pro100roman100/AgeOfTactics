using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private GameObject Block;
    private GameObject Miner;
    private GameObject Turret;
    //:)
    bool ACTIVATOR; 
    private void Awake()
    {
        Block = GameObject.Find("Block");
        Miner = GameObject.Find("Miner");
        Turret = GameObject.Find("Turret");
        Block.SetActive(false);
        Miner.SetActive(false);
        Turret.SetActive(false);
        ACTIVATOR =false;
    }

    public void Click()
    {
        if (ACTIVATOR == false)
        {
            Block.SetActive(true);
            Miner.SetActive(true);
            Turret.SetActive(true);
            ACTIVATOR = true;
        }
        else
        {
            Block.SetActive(false);
            Miner.SetActive(false);
            Turret.SetActive(false);
            ACTIVATOR = false;
        }

    }
}

