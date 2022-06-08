using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    public Vector2 size;
    public Vector2 offset;

    public void OnBuild() { return; }

    public void OnDestroy() { return; }
}
