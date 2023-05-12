using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatterManager : MonoBehaviour
{
    public static MatterManager Manager = null;
    [HideInInspector] public static float player1matter = 0;
    [HideInInspector] public static float player2matter = 0;
    public float player1matterPerSecond = 100;
    public float player2matterPerSecond = 1;
    [SerializeField] private float startMatter = 100;

    public void massSpending(string tag, float cost)
    {
        if (tag == "Player_1")
            player1matter -= cost;
        else
            player2matter -= cost;
    }
    private void Awake()
    {
        if (Manager == null)
            Manager = this;
        player1matter = startMatter;
        player2matter = startMatter;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.A))
            player1matter += 1000;
#endif
        player1matter += player1matterPerSecond * Time.deltaTime;
        player2matter += player2matterPerSecond * Time.deltaTime;
    }
}
