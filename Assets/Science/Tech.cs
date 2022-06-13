using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tech : MonoBehaviour
{
    [SerializeField] private Tech[] previousTech;
    [SerializeField] private bool neededAllTechBefore = false;
    [SerializeField] private float cost;
    public bool researched { get; private set; } = false;

    public bool CanTech()
    {
        if (previousTech.Length == 0)
        {
            return true;
        }
        else if (neededAllTechBefore)
        {
            bool canTech = true;
            foreach (Tech tech in previousTech)
            {
                if (!tech.researched) canTech = false;
            }
            return canTech;
        }
        else
        {
            bool canTech = false;
            foreach (Tech tech in previousTech)
            {
                if (tech.researched) canTech = true;
            }
            return canTech;
        }
    }

    public abstract void OnResearch();

    public void Research()
    {
        if (CanTech() && MatterManager.matter >= cost)
        {
            MatterManager.matter -= cost;
            OnResearch();
            researched = true;
        }
    }
}