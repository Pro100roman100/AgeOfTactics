using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatterManager : MonoBehaviour
{
    public static MatterManager Manager = null;
    [HideInInspector] public static float matter = 0;
    public float matterPerSecond = 100;
    [SerializeField] private float startMatter = 100;

    public void massSpending(string tag, float cost)
    {
        if (tag == "Player_1")
            matter -= cost;
        else
            matter += cost;
    }
    private void Awake()
    {
        if (Manager == null)
            Manager = this;
        matter = startMatter;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.A))
            matter += 1000;
#endif
        matter += matterPerSecond * Time.deltaTime;
        if (matter < 0) Debug.LogError("Matter < 0");
    }
}
