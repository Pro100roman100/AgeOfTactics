using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatterManager : MonoBehaviour
{
    public static MatterManager Manager = null;
    [HideInInspector] public static int matter = 0;

    private void Awake()
    {
        if (Manager == null)
            Manager = this;
    }
}
